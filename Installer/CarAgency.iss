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
Type: filesandordirs; Name: "{commonappdata}\{#AppName}"

[Code]
const
  NetFx48Release = 528040;
  CheckMissing = 0;
  CheckExists = 1;

var
  ActionPage: TInputOptionWizardPage;
  ServerPage: TInputQueryWizardPage;
  ReplacePage: TInputOptionWizardPage;
  PreviousInstall: Boolean;
  DatabaseFound: Boolean;
  Uninstalled: Boolean;

function IsNetFx48Installed: Boolean;
var
  Release: Cardinal;
begin
  Result := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Release)
    and (Release >= NetFx48Release);
end;

function UninstallKey: String;
begin
  Result := 'Software\Microsoft\Windows\CurrentVersion\Uninstall\{' + '{#AppGuid}' + '}_is1';
end;

function IsPreviouslyInstalled: Boolean;
begin
  Result := RegKeyExists(HKLM64, UninstallKey) or RegKeyExists(HKLM32, UninstallKey);
end;

function UninstallerPath: String;
begin
  Result := '';
  if not RegQueryStringValue(HKLM64, UninstallKey, 'UninstallString', Result) then
    RegQueryStringValue(HKLM32, UninstallKey, 'UninstallString', Result);
  Result := RemoveQuotes(Result);
end;

function Uninstalling: Boolean;
begin
  Result := PreviousInstall and ActionPage.Values[1];
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
      'En el paso siguiente puede elegir repararlo o desinstalarlo.';

  ActionPage := CreateInputOptionPage(wpWelcome,
    'Instalación existente',
    '¿Qué desea hacer?',
    'CarAgency ya está instalado en este equipo.',
    True, False);
  ActionPage.Add('Reparar: volver a copiar el programa y conservar la base de datos');
  ActionPage.Add('Desinstalar: quitar el programa y borrar la base de datos CarAgency');
  ActionPage.Values[0] := True;

  ServerPage := CreateInputQueryPage(wpSelectDir,
    'Servidor de base de datos',
    '¿En qué instancia de SQL Server está la base?',
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
  Result := ((PageID = ActionPage.ID) and not PreviousInstall) or
    ((PageID = ReplacePage.ID) and not DatabaseFound);
end;

procedure CurPageChanged(CurPageID: Integer);
begin
  if (CurPageID = ServerPage.ID) and Uninstalling then
    WizardForm.NextButton.Caption := 'Desinstalar';
end;

// Tras desinstalar, el asistente se cierra sin preguntar si se quiere salir.
procedure CancelButtonClick(CurPageID: Integer; var Cancel, Confirm: Boolean);
begin
  Confirm := not Uninstalled;
end;

// UAC puede elevar el instalador con otra cuenta (en el laboratorio, "Tecnico") que no tiene login en SQL Server.
// Por eso lo que toca SQL corre como el usuario que abrio el instalador, el mismo que despues usa la aplicacion.
// Ese usuario no puede escribir en Program Files ni leer el {tmp} de la cuenta elevada: trabaja en esta carpeta.
// Desde aca nunca se ejecuta nada elevado: cualquier usuario puede modificarla.
function WorkDir: String;
begin
  Result := ExpandConstant('{commonappdata}\{#AppName}');
end;

function DbSetupLog: String;
begin
  Result := WorkDir + '\DbSetup.log';
end;

procedure PrepareWorkDir;
var
  Code: Integer;
begin
  ForceDirectories(WorkDir);
  // S-1-5-11 = Usuarios autentificados (el SID evita depender del idioma de Windows).
  Exec(ExpandConstant('{sys}\icacls.exe'), AddQuotes(WorkDir) + ' /grant *S-1-5-11:(OI)(CI)M', '',
    SW_HIDE, ewWaitUntilTerminated, Code);
end;

// Devuelve el codigo de salida de DbSetup, o -1 si no se pudo ejecutar (el motivo queda en el log).
function ExecDbSetupAsUser(const Exe, Params: String): Integer;
var
  Code: Integer;
begin
  DeleteFile(DbSetupLog);
  if ExecAsOriginalUser(Exe, Params + ' --log ' + AddQuotes(DbSetupLog), '', SW_HIDE, ewWaitUntilTerminated, Code) then
    Result := Code
  else
  begin
    SaveStringToFile(DbSetupLog, 'No se pudo ejecutar ' + Exe + ': ' + SysErrorMessage(Code), False);
    Result := -1;
  end;
end;

// La copia de DbSetup de este instalador (no la ya instalada, que puede ser de otra version),
// en la carpeta que el usuario original puede leer.
function WorkDbSetup: String;
begin
  PrepareWorkDir;
  ExtractTemporaryFile('DbSetup.exe');
  Result := WorkDir + '\DbSetup.exe';
  FileCopy(ExpandConstant('{tmp}\DbSetup.exe'), Result, False);
end;

function CheckDatabase: Boolean;
var
  Code: Integer;
  Detail: AnsiString;
begin
  Result := False;
  Code := ExecDbSetupAsUser(WorkDbSetup, 'check --server ' + AddQuotes(Server));

  case Code of
    CheckMissing: DatabaseFound := False;
    CheckExists: DatabaseFound := True;
  else
    begin
      Detail := '';
      LoadStringFromFile(DbSetupLog, Detail);
      MsgBox('No se pudo conectar con la instancia "' + Server + '".' + #13#10#13#10 +
        'Verifique que SQL Server esté instalado y en ejecución, y que el usuario actual tenga acceso.' + #13#10#13#10 +
        'Detalle: ' + String(Detail),
        mbError, MB_OK);
      Exit;
    end;
  end;
  Result := True;
end;

// El desinstalador no puede borrar la base: corre con la cuenta elevada, que puede no tener login en SQL Server,
// y Inno no permite ExecAsOriginalUser al desinstalar. Por eso la base se borra desde aca y despues se lo invoca.
procedure UninstallAll;
var
  Code: Integer;
begin
  if MsgBox('Se va a desinstalar CarAgency y borrar la base CarAgency de "' + Server + '" con todos sus datos.' + #13#10#13#10 +
    '¿Desea continuar?', mbConfirmation, MB_YESNO or MB_DEFBUTTON2) <> IDYES then
    Exit;

  WizardForm.NextButton.Enabled := False;
  if ExecDbSetupAsUser(WorkDbSetup, 'drop --server ' + AddQuotes(Server)) <> 0 then
  begin
    WizardForm.NextButton.Enabled := True;
    MsgBox('No se pudo borrar la base de datos, así que el programa no se desinstaló. El detalle está en:' + #13#10 + DbSetupLog, mbError, MB_OK);
    Exit;
  end;

  // Devuelve el control cuando la desinstalacion termino (ver "Uninstaller Exit Codes" en la ayuda de Inno).
  if not Exec(UninstallerPath, '/VERYSILENT /SUPPRESSMSGBOXES /NORESTART', '', SW_HIDE, ewWaitUntilTerminated, Code) or (Code <> 0) then
    MsgBox('La base de datos se borró, pero no se pudo quitar el programa. Desinstálelo desde Configuración > Aplicaciones.', mbError, MB_OK)
  else
    MsgBox('CarAgency se desinstaló y la base de datos se borró.', mbInformation, MB_OK);

  Uninstalled := True;
  WizardForm.Close;
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
    else if Uninstalling then
    begin
      UninstallAll;
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
  Exe, Params: String;
  Code: Integer;
begin
  if CurStep <> ssPostInstall then
    Exit;

  Exe := ExpandConstant('{app}\Database\DbSetup.exe');
  Params := 'install --server ' + AddQuotes(Server) +
    ' --script ' + AddQuotes(ExpandConstant('{app}\Database\CarAgency.sql'));
  if ReplaceDatabase then
    Params := Params + ' --replace';

  WizardForm.StatusLabel.Caption := 'Preparando la base de datos...';
  if ExecDbSetupAsUser(Exe, Params) <> 0 then
  begin
    MsgBox('No se pudo preparar la base de datos. El detalle está en:' + #13#10 + DbSetupLog, mbError, MB_OK);
    Exit;
  end;

  // Elevado: escribe en Program Files y no se conecta a SQL Server.
  if not Exec(Exe, 'config --server ' + AddQuotes(Server) +
    ' --config ' + AddQuotes(ExpandConstant('{app}\{#AppExe}.config')) +
    ' --log ' + AddQuotes(DbSetupLog), '', SW_HIDE, ewWaitUntilTerminated, Code) or (Code <> 0) then
    MsgBox('No se pudo configurar la conexión de la aplicación. El detalle está en:' + #13#10 + DbSetupLog, mbError, MB_OK);
end;

// Desinstalando desde Configuracion > Aplicaciones la base queda (ver UninstallAll). Silencioso = lo invoco el asistente.
function InitializeUninstall: Boolean;
begin
  Result := True;
  if not UninstallSilent then
    MsgBox('Esta desinstalación quita el programa pero conserva la base de datos CarAgency.' + #13#10#13#10 +
      'Para borrar también la base, cancele y ejecute el instalador de CarAgency con la opción "Desinstalar".',
      mbInformation, MB_OK);
end;
