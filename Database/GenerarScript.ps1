# Genera Database\CarAgency.sql (esquema + datos iniciales) desde una base existente, con SMO.
# Uso: powershell -ExecutionPolicy Bypass -File Database\GenerarScript.ps1 [-Server .] [-Database CarAgency]
#
# Datos: solo los de configuracion ($dataFilters). Las operaciones (personas, vehiculos, compras, bitacoras, etc.)
# quedan vacias. El DVV de cada tabla se recalcula sobre las filas que viajan, con Security.dll de la aplicacion
# compilada ($Bin) y la clave de UI\App.config: compilar la solucion antes de correr este script.
#
# El script no incluye CREATE DATABASE ni USE: DbSetup crea la base y ejecuta el script conectado a ella.
# Orden: tipos, secuencias y tablas sin claves foraneas -> datos -> claves foraneas -> procedimientos,
# asi los INSERT no dependen del orden entre tablas.
param(
    [string]$Server = ".",
    [string]$Database = "CarAgency",
    [string]$Output = (Join-Path $PSScriptRoot "CarAgency.sql"),
    [string]$Bin = (Join-Path $PSScriptRoot "..\UI\bin\Release"),
    [string]$AppConfig = (Join-Path $PSScriptRoot "..\UI\App.config")
)

# Roles, permisos y patentes; marcas, modelos y versiones; los catalogos que usan las pantallas y la bitacora;
# y el usuario Admin para el primer ingreso. Valor: filtro de filas (vacio = todas).
$dataFilters = @{
    Permiso = ""; Permiso_Permiso = ""
    Marca = ""; Modelo = ""; Version = ""
    TipoEvento = ""; Color = ""; TipoPago = ""; TipoDocumentoCompra = ""; ItemInspeccion = ""
    Usuario = "NombreUsuario = 'Admin'"
}

$ErrorActionPreference = "Stop"
Add-Type -AssemblyName "Microsoft.SqlServer.Smo, Version=17.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
Add-Type -AssemblyName "Microsoft.SqlServer.ConnectionInfo, Version=17.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"

$connection = New-Object Microsoft.SqlServer.Management.Common.ServerConnection($Server)
$connection.TrustServerCertificate = $true
$smo = New-Object Microsoft.SqlServer.Management.Smo.Server($connection)
$db = $smo.Databases[$Database]
if ($db -eq $null) { throw "No existe la base $Database en $Server." }

function New-Options {
    $o = New-Object Microsoft.SqlServer.Management.Smo.ScriptingOptions
    $o.TargetServerVersion = [Microsoft.SqlServer.Management.Smo.SqlServerVersion]::Version150
    $o.IncludeDatabaseContext = $false
    $o.IncludeHeaders = $false
    $o.ScriptBatchTerminator = $true
    $o.AnsiPadding = $true
    $o.ExtendedProperties = $true
    $o.Permissions = $false
    return $o
}

$sqlText = New-Object System.Text.StringBuilder
# El lote incluye el fin de linea previo al GO, asi que cada sentencia se escribe con el suyo propio
# (LF o CRLF): el texto de los procedimientos queda igual al de la base de origen.
function Add-Line([string]$text) {
    [void]$sqlText.Append($text)
    if (-not $text.EndsWith("`n")) { [void]$sqlText.Append("`r`n") }
}
function Add-Script($script) {
    foreach ($statement in $script) {
        Add-Line $statement
        Add-Line "GO"
    }
}

# Los procedimientos que otros invocan van primero, asi no aparecen avisos de objetos faltantes.
function Sort-ByDependency($procedures) {
    $references = @{}
    $rows = $db.ExecuteWithResults(
        "SELECT OBJECT_NAME(d.referencing_id) AS Referencing, d.referenced_entity_name AS Referenced " +
        "FROM sys.sql_expression_dependencies d JOIN sys.procedures p ON p.name = d.referenced_entity_name").Tables[0].Rows
    foreach ($row in $rows) {
        if (-not $references.ContainsKey($row.Referencing)) { $references[$row.Referencing] = @() }
        $references[$row.Referencing] += $row.Referenced
    }

    $placed = @{}
    $ordered = New-Object System.Collections.Generic.List[object]
    $pending = @($procedures)
    while ($pending.Count -gt 0) {
        $ready = @($pending | Where-Object {
            $name = $_.Name
            -not $references.ContainsKey($name) -or
            -not ($references[$name] | Where-Object { $_ -ne $name -and -not $placed.ContainsKey($_) })
        })
        # Dependencias circulares: se agregan igual, SQL Server resuelve los nombres al ejecutar.
        if ($ready.Count -eq 0) { $ready = @($pending[0]) }
        foreach ($procedure in $ready) { $ordered.Add($procedure); $placed[$procedure.Name] = $true }
        $pending = @($pending | Where-Object { -not $placed.ContainsKey($_.Name) })
    }
    return $ordered
}

$tables = @($db.Tables | Where-Object { -not $_.IsSystemObject } | Sort-Object Name)
$types = @($db.UserDefinedTableTypes) + @($db.UserDefinedDataTypes)
$procedures = @($db.StoredProcedures | Where-Object { -not $_.IsSystemObject } | Sort-Object Name)

# 1. Tipos y tablas, con PK, UNIQUE, CHECK, DEFAULT e indices, sin FK.
$schema = New-Options
$schema.DriPrimaryKey = $true
$schema.DriUniqueKeys = $true
$schema.DriChecks = $true
$schema.DriDefaults = $true
$schema.Indexes = $true
$schema.DriForeignKeys = $false
foreach ($type in $types) { Add-Script $type.Script($schema) }

# Las SEQUENCE numeran compras y comprobantes, que no viajan: arrancan en 1. No se usa start_value
# porque una base creada con un script anterior lo trae corrido.
$sequences = $db.ExecuteWithResults(
    "SELECT s.name AS Name, TYPE_NAME(s.user_type_id) AS TypeName, CAST(s.increment AS bigint) AS Increment " +
    "FROM sys.sequences s WHERE SCHEMA_NAME(s.schema_id) = 'dbo' ORDER BY s.name").Tables[0].Rows
foreach ($sequence in $sequences) {
    Add-Line ("CREATE SEQUENCE [dbo].[{0}] AS {1} START WITH 1 INCREMENT BY {2}" -f $sequence.Name, $sequence.TypeName, $sequence.Increment)
    Add-Line "GO"
}

# SMO vuelve ANSI_PADDING a OFF despues de cada tabla, y los indices filtrados (UX_Acquisitions_Vehicle)
# solo se crean con ON: se deja ON para todo el script.
foreach ($table in $tables) { Add-Script @($table.Script($schema) | Where-Object { $_ -notmatch '^\s*SET ANSI_PADDING OFF\s*$' }) }

# 2. Datos de configuracion. Se copian los valores tal cual estan, incluido el DVH.
$data = New-Options
$data.ScriptSchema = $false
$data.ScriptData = $true
$scripter = New-Object Microsoft.SqlServer.Management.Smo.Scripter($smo)
$scripter.Options = $data
foreach ($table in $tables) {
    if (-not $dataFilters.ContainsKey($table.Name) -or $table.RowCount -eq 0) { continue }
    $inserts = @($scripter.EnumScript([Microsoft.SqlServer.Management.Smo.SqlSmoObject[]]@($table)))
    $filter = $dataFilters[$table.Name]
    if ($filter) {
        # SMO no filtra filas: quedan los INSERT cuyo primer valor (la clave) es de una fila del filtro.
        $key = $table.Columns[0].Name
        $ids = @($db.ExecuteWithResults("SELECT CAST([$key] AS nvarchar(36)) AS Id FROM [dbo].[$($table.Name)] WHERE $filter").Tables[0].Rows | ForEach-Object { $_.Id })
        $inserts = @($inserts | Where-Object { $line = $_; @($ids | Where-Object { $line.IndexOf("VALUES (N'$_'", [StringComparison]::OrdinalIgnoreCase) -ge 0 }).Count -gt 0 })
        if ($inserts.Count -ne $ids.Count) { throw "dbo.$($table.Name): se esperaban $($ids.Count) filas y quedaron $($inserts.Count)." }
    }
    foreach ($insert in $inserts) { Add-Line $insert }
    Add-Line "GO"
}

# DV: una fila por tabla protegida, con el DVV de las filas que viajan (las tablas vacias tambien lo necesitan).
[void][Reflection.Assembly]::LoadFrom((Resolve-Path (Join-Path $Bin "BE.dll")).Path)
[void][Reflection.Assembly]::LoadFrom((Resolve-Path (Join-Path $Bin "Security.dll")).Path)
$dvKey = ([xml](Get-Content $AppConfig -Raw)).configuration.appSettings.add | Where-Object { $_.key -eq "CARAGENCY_DV_KEY" }
$calculator = [CarAgency.Security.Integrity.DigitVerifierCalculator]::new([Convert]::FromBase64String($dvKey.value))
$digits = $db.ExecuteWithResults("SELECT NombreEsquema, NombreTabla, VersionAlgoritmo FROM dbo.DV ORDER BY NombreTabla").Tables[0].Rows
foreach ($digit in $digits) {
    $name = $digit.NombreTabla
    $digests = @()
    if ($dataFilters.ContainsKey($name)) {
        $where = if ($dataFilters[$name]) { " WHERE " + $dataFilters[$name] } else { "" }
        $digests = @($db.ExecuteWithResults("SELECT DVH FROM [dbo].[$name]$where").Tables[0].Rows | ForEach-Object { $_.DVH })
    }
    Add-Line ("INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'{0}', N'{1}', N'{2}', {3})" -f
        $digit.NombreEsquema, $name, $calculator.CalculateDvv($name, [string[]]$digests), $digit.VersionAlgoritmo)
}
Add-Line "GO"

# 3. Claves foraneas.
$fk = New-Options
foreach ($table in $tables) {
    foreach ($key in $table.ForeignKeys) { Add-Script $key.Script($fk) }
}

# 4. Procedimientos.
$code = New-Options
foreach ($procedure in (Sort-ByDependency $procedures)) { Add-Script $procedure.Script($code) }

# 5. Triggers. Van despues de los datos: si existieran durante los INSERT, la carga del script
# agregaria filas a BitacoraCambios por cada vehiculo, ademas de las que ya trae el historial.
$triggerCount = 0
foreach ($table in $tables) {
    foreach ($trigger in $table.Triggers) { Add-Script $trigger.Script($code); $triggerCount++ }
}

$utf8 = New-Object System.Text.UTF8Encoding($true)
[System.IO.File]::WriteAllText($Output, $sqlText.ToString(), $utf8)
Write-Host ("Generado {0}: {1} tablas, {2} tipos, {3} secuencias, {4} procedimientos, {5} triggers." -f $Output, $tables.Count, $types.Count, @($sequences).Count, $procedures.Count, $triggerCount)
