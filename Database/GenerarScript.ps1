# Genera Database\CarAgency.sql (esquema + datos) desde una base existente, con SMO.
# Uso: powershell -ExecutionPolicy Bypass -File Database\GenerarScript.ps1 [-Server .] [-Database CarAgency]
#
# El script no incluye CREATE DATABASE ni USE: DbSetup crea la base y ejecuta el script conectado a ella.
# Orden: tipos y tablas sin claves foraneas -> datos -> claves foraneas -> procedimientos,
# asi los INSERT no dependen del orden entre tablas.
param(
    [string]$Server = ".",
    [string]$Database = "CarAgency",
    [string]$Output = (Join-Path $PSScriptRoot "CarAgency.sql")
)

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
foreach ($table in $tables) { Add-Script $table.Script($schema) }

# 2. Datos. Se copian los valores tal cual estan, incluidos DVH y DVV.
$data = New-Options
$data.ScriptSchema = $false
$data.ScriptData = $true
$scripter = New-Object Microsoft.SqlServer.Management.Smo.Scripter($smo)
$scripter.Options = $data
foreach ($table in $tables) {
    if ($table.RowCount -eq 0) { continue }
    $inserts = @($scripter.EnumScript([Microsoft.SqlServer.Management.Smo.SqlSmoObject[]]@($table)))
    foreach ($insert in $inserts) { Add-Line $insert }
    Add-Line "GO"
}

# 3. Claves foraneas.
$fk = New-Options
foreach ($table in $tables) {
    foreach ($key in $table.ForeignKeys) { Add-Script $key.Script($fk) }
}

# 4. Procedimientos.
$code = New-Options
foreach ($procedure in (Sort-ByDependency $procedures)) { Add-Script $procedure.Script($code) }

$utf8 = New-Object System.Text.UTF8Encoding($true)
[System.IO.File]::WriteAllText($Output, $sqlText.ToString(), $utf8)
Write-Host ("Generado {0}: {1} tablas, {2} tipos, {3} procedimientos." -f $Output, $tables.Count, $types.Count, $procedures.Count)
