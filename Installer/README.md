# Instalador de CarAgency

## Qué hace

- **Primera instalación:** copia el programa, crea la base `CarAgency` en la instancia de SQL Server elegida y apunta `UI.exe.config` a esa instancia.
- **Reinstalación (reparar):** detecta la instalación anterior, vuelve a copiar los archivos y conserva la base.
- **Reemplazar la base:** si la base ya existe, el asistente muestra la opción *Reemplazar la base de datos existente*. Antes de borrarla se guarda una copia en la carpeta Backup de la instancia (`CarAgency.antes-de-reemplazar-<fecha>.bak`).

La lógica de la base está en `DbSetup/` (`DbSetup.exe check|install`). El instalador solo la invoca.

## Cómo generar el instalador

1. Dejar la base local `CarAgency` como se va a entregar, con los dígitos verificadores recalculados desde la aplicación.
2. Regenerar el script (esquema + datos, compatible con SQL Server 2019 o superior):

   ```
   powershell -ExecutionPolicy Bypass -File Database\GenerarScript.ps1
   ```

   Deja `Database\CarAgency.sql` sin `CREATE DATABASE` ni `USE`. Git no le convierte los fines de línea (`.gitattributes`), porque cambiarlos alteraría datos protegidos por DVH.
3. Compilar la solución en **Release**.
4. Compilar `Installer\CarAgency.iss` con Inno Setup 6.3 o superior. El instalador queda en `Installer\Output\`:

   ```
   "%LOCALAPPDATA%\Programs\Inno Setup 6\ISCC.exe" Installer\CarAgency.iss
   ```

## Requisitos de la máquina destino

- Windows de 64 bits con .NET Framework 4.8.
- SQL Server ya instalado y en ejecución.
- El usuario que instala y el que usa la aplicación necesitan acceso a SQL Server con autenticación de Windows.
- La clave `CARAGENCY_DV_KEY` de `UI.exe.config` tiene que ser la misma con la que se calcularon los dígitos verificadores del script.
