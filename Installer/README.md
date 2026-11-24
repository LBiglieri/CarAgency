# Instalador de CarAgency

## Qué hace

- **Primera instalación:** copia el programa, crea la base `CarAgency` en la instancia de SQL Server elegida y apunta `UI.exe.config` a esa instancia.
- **Instalación existente:** detecta la instalación anterior y deja elegir:
  - **Reparar:** vuelve a copiar los archivos y conserva la base.
  - **Desinstalar:** borra la base `CarAgency` (sin copia de seguridad) y después ejecuta el desinstalador en modo silencioso.
- **Reemplazar la base:** si la base ya existe, el asistente muestra la opción *Reemplazar la base de datos existente*. Antes de borrarla se guarda una copia en la carpeta Backup de la instancia (`CarAgency.antes-de-reemplazar-<fecha>.bak`).

La lógica de la base está en `DbSetup/` (`DbSetup.exe check|install|config|drop`). El instalador solo la invoca.

La base nueva trae solo datos de configuración: roles, permisos y patentes; marcas, modelos y versiones; los catálogos (tipos de evento, colores, tipos de pago, tipos de documento, ítems de inspección) y el usuario `Admin`. El resto de las tablas queda vacío.

`check`, `install` y `drop` corren como el usuario que abrió el instalador, no como la cuenta con la que se autorizó UAC (que puede no tener login en SQL Server). `config` corre elevado porque escribe `UI.exe.config` en Program Files. El log queda en `C:\ProgramData\CarAgency\DbSetup.log`.

Desinstalar desde *Configuración > Aplicaciones* quita el programa pero **conserva la base**: Inno Setup no permite ejecutar como el usuario original durante la desinstalación, y la cuenta elevada puede no tener acceso a SQL Server. Por eso el borrado de la base solo está en la opción *Desinstalar* del instalador.

## Cómo generar el instalador

1. Dejar la base local `CarAgency` con los datos de configuración como se van a entregar y los dígitos verificadores recalculados desde la aplicación.
2. Compilar la solución en **Release** (el paso siguiente usa `UI\bin\Release\Security.dll` para recalcular los DVV).
3. Regenerar el script (esquema + datos de configuración, compatible con SQL Server 2019 o superior):

   ```
   powershell -ExecutionPolicy Bypass -File Database\GenerarScript.ps1
   ```

   Deja `Database\CarAgency.sql` sin `CREATE DATABASE` ni `USE`, con los DVV recalculados sobre las filas que viajan. Las tablas que se cargan están en `$dataFilters`. Git no le convierte los fines de línea (`.gitattributes`), porque cambiarlos alteraría datos protegidos por DVH.
4. Compilar `Installer\CarAgency.iss` con Inno Setup 6.3 o superior. El instalador queda en `Installer\Output\`:

   ```
   "%LOCALAPPDATA%\Programs\Inno Setup 6\ISCC.exe" Installer\CarAgency.iss
   ```

## Requisitos de la máquina destino

- Windows de 64 bits con .NET Framework 4.8.
- SQL Server ya instalado y en ejecución.
- El usuario que va a usar la aplicación abre el instalador con doble clic (no con "Ejecutar como administrador") y necesita permiso para crear bases en SQL Server (rol `dbcreator` o `sysadmin`) con autenticación de Windows. La cuenta de administrador que autoriza UAC no necesita acceso a SQL Server.
- La clave `CARAGENCY_DV_KEY` de `UI.exe.config` tiene que ser la misma con la que se calcularon los dígitos verificadores del script.
