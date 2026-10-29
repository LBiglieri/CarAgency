; Instalador de CarAgency (Inno Setup 6.3 o superior).
; Antes de compilar: generar UI y DbSetup en Release y dejar el script de la base en Database\CarAgency.sql.

#define AppName "CarAgency"
#define AppVersion "1.0.0"
#define AppPublisher "Lautaro Biglieri"
#define AppExe "UI.exe"
; Identifica la instalacion entre versiones: no cambiarlo, o la reinstalacion deja de detectarse.
#define AppGuid "7984CD30-A88C-416D-9C1C-C18F24025C4F"
#define UIBin "..\UI\bin\Release"
#define DbSetupBin "..\DbSetup\bin\Release"

[Setup]
AppId={{{#AppGuid}}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#AppPublisher}
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
UsePreviousAppDir=yes
DisableDirPage=auto
DisableProgramGroupPage=yes
DisableWelcomePage=no
PrivilegesRequired=admin
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputDir=Output
OutputBaseFilename={#AppName}-Setup-{#AppVersion}
SetupIconFile=..\UI\logo.ico
UninstallDisplayIcon={app}\{#AppExe}
Compression=lzma2
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "es"; MessagesFile: "compiler:Languages\Spanish.isl"

[Files]
Source: "{#UIBin}\*"; DestDir: "{app}"; Excludes: "*.pdb,*.vshost.*"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#DbSetupBin}\DbSetup.exe"; DestDir: "{app}\Database"; Flags: ignoreversion
Source: "{#DbSetupBin}\DbSetup.exe.config"; DestDir: "{app}\Database"; Flags: ignoreversion
Source: "..\Database\CarAgency.sql"; DestDir: "{app}\Database"; Flags: ignoreversion
; Copia que solo se extrae a {tmp} para consultar la base mientras el asistente esta abierto.
Source: "{#DbSetupBin}\DbSetup.exe"; Flags: dontcopy

[Icons]
Name: "{autoprograms}\{#AppName}"; Filename: "{app}\{#AppExe}"
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\{#AppExe}"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Run]
Filename: "{app}\{#AppExe}"; Description: "{cm:LaunchProgram,{#AppName}}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: files; Name: "{app}\Database\DbSetup.log"

[Code]
const
  NetFx48Release = 528040;
  CheckMissing = 0;
  CheckExists = 1;

var
  ServerPage: TInputQueryWizardPage;
  ReplacePage: TInputOptionWizardPage;
  PreviousInstall: Boolean;
  DatabaseFound: Boolean;

function IsNetFx48Installed: Boolean;
var
  Release: Cardinal;
begin
  Result := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Release)
    and (Release >= NetFx48Release);
end;

function IsPreviouslyInstalled: Boolean;
var
  Key: String;
begin
  Key := 'Software\Microsoft\Windows\CurrentVersion\Uninstall\{' + '{#AppGuid}' + '}_is1';
  Result := RegKeyExists(HKLM64, Key) or RegKeyExists(HKLM32, Key);
end;

// Propone la instancia local: la predeterminada si existe, si no la primera con nombre (por ejemplo SQLEXPRESS).
function DetectServer: String;
var
  Names: TArrayOfString;
  I: Integer;
begin
  Result := '';
  if not RegGetValueNames(HKLM64, 'SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL', Names) then
    Exit;
  for I := 0 to GetArrayLength(Names) - 1 do
    if CompareText(Names[I], 'MSSQLSERVER') = 0 then
    begin
      Result := '.';
      Exit;
    end;
  if GetArrayLength(Names) > 0 then
    Result := '.\' + Names[0];
end;

function Server: String;
begin
  Result := Trim(ServerPage.Values[0]);
end;

function ReplaceDatabase: Boolean;
begin
  Result := DatabaseFound and ReplacePage.Values[0];
end;

function InitializeSetup: Boolean;
begin
  Result := IsNetFx48Installed;
  if not Result then
    MsgBox('CarAgency requiere .NET Framework 4.8. Instálelo y vuelva a ejecutar el instalador.', mbCriticalError, MB_OK);
end;

procedure InitializeWizard;
begin
  PreviousInstall := IsPreviouslyInstalled;
  if PreviousInstall then
    WizardForm.WelcomeLabel2.Caption :=
      'Se detectó que CarAgency ya está instalado en este equipo.' + #13#10#13#10 +
      'Si continúa, se reparará la instalación: se vuelven a copiar los archivos del programa ' +
      'y la base de datos se conserva, salvo que elija reemplazarla más adelante.';

  ServerPage := CreateInputQueryPage(wpSelectDir,
    'Servidor de base de datos',
    '¿En qué instancia de SQL Server se instala la base?',
    'Escriba la instancia de SQL Server de este equipo, por ejemplo "." para la instancia predeterminada ' +
    'o ".\SQLEXPRESS". La conexión usa autenticación de Windows.');
  ServerPage.Add('Instancia:', False);
  ServerPage.Values[0] := GetPreviousData('Server', DetectServer);

  ReplacePage := CreateInputOptionPage(ServerPage.ID,
    'Base de datos existente',
    'La base CarAgency ya existe en esta instancia.',
    'Si no marca la opción, se conserva la base actual con todos sus datos. ' +
    'Si la marca, se guarda una copia de seguridad en la carpeta Backup de SQL Server ' +
    'y la base se crea de nuevo con los datos iniciales.',
    False, False);
  ReplacePage.Add('Reemplazar la base de datos existente');
  ReplacePage.Values[0] := False;
end;

procedure RegisterPreviousData(PreviousDataKey: Integer);
begin
  SetPreviousData(PreviousDataKey, 'Server', Server);
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := (PageID = ReplacePage.ID) and not DatabaseFound;
end;

function CheckDatabase: Boolean;
var
  Code: Integer;
begin
  Result := False;
  ExtractTemporaryFile('DbSetup.exe');
  if not Exec(ExpandConstant('{tmp}\DbSetup.exe'), 'check --server ' + AddQuotes(Server), '',
    SW_HIDE, ewWaitUntilTerminated, Code) then
    Code := -1;

  case Code of
    CheckMissing: DatabaseFound := False;
    CheckExists: DatabaseFound := True;
  else
    begin
      MsgBox('No se pudo conectar con la instancia "' + Server + '".' + #13#10#13#10 +
        'Verifique que SQL Server esté instalado y en ejecución, y que el usuario actual tenga acceso.',
        mbError, MB_OK);
      Exit;
    end;
  end;
  Result := True;
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  Result := True;
  if CurPageID = ServerPage.ID then
  begin
    if Server = '' then
    begin
      MsgBox('Indique la instancia de SQL Server.', mbError, MB_OK);
      Result := False;
    end
    else
      Result := CheckDatabase;
  end
  else if (CurPageID = ReplacePage.ID) and ReplacePage.Values[0] then
    Result := MsgBox('Se van a reemplazar todos los datos de la base CarAgency. ¿Desea continuar?',
      mbConfirmation, MB_YESNO or MB_DEFBUTTON2) = IDYES;
end;

function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo,
  MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
  Action: String;
begin
  if not DatabaseFound then
    Action := 'Crear la base CarAgency'
  else if ReplaceDatabase then
    Action := 'Reemplazar la base CarAgency (con copia de seguridad previa)'
  else
    Action := 'Conservar la base CarAgency existente';

  Result := MemoDirInfo + NewLine + NewLine +
    'Base de datos:' + NewLine + Space + 'Instancia: ' + Server + NewLine + Space + Action;
  if MemoTasksInfo <> '' then
    Result := Result + NewLine + NewLine + MemoTasksInfo;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  Params, LogFile: String;
  Code: Integer;
begin
  if CurStep <> ssPostInstall then
    Exit;

  LogFile := ExpandConstant('{app}\Database\DbSetup.log');
  Params := 'install --server ' + AddQuotes(Server) +
    ' --script ' + AddQuotes(ExpandConstant('{app}\Database\CarAgency.sql')) +
    ' --config ' + AddQuotes(ExpandConstant('{app}\{#AppExe}.config')) +
    ' --log ' + AddQuotes(LogFile);
  if ReplaceDatabase then
    Params := Params + ' --replace';

  WizardForm.StatusLabel.Caption := 'Preparando la base de datos...';
  if not Exec(ExpandConstant('{app}\Database\DbSetup.exe'), Params, '', SW_HIDE, ewWaitUntilTerminated, Code)
    or (Code <> 0) then
    MsgBox('No se pudo preparar la base de datos. El detalle está en:' + #13#10 + LogFile, mbError, MB_OK);
end;
