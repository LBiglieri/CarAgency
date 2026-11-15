-- Agrega las dos claves foraneas que faltaban:
--   Usuario.Rol_Id     -> Permiso.Permiso_Id  (el rol del usuario es una familia)
--   Tramite.Persona_Id -> Persona.Persona_Id  (el cliente del tramite)
-- Las dos columnas ya eran NOT NULL y no hay filas huerfanas. Permiso_DeleteCompleteFamily pasa los usuarios
-- a "Base User" antes de borrar la familia, asi que la FK no lo afecta. Una restriccion no es un dato:
-- no cambia ningun DVH ni DVV.
-- Ejecutar con sqlcmd -I. Se puede correr mas de una vez.

IF OBJECT_ID('dbo.FK_Usuario_Permiso', 'F') IS NULL
    ALTER TABLE dbo.Usuario WITH CHECK ADD CONSTRAINT FK_Usuario_Permiso
        FOREIGN KEY (Rol_Id) REFERENCES dbo.Permiso (Permiso_Id);
GO

IF OBJECT_ID('dbo.FK_Tramite_Persona', 'F') IS NULL
    ALTER TABLE dbo.Tramite WITH CHECK ADD CONSTRAINT FK_Tramite_Persona
        FOREIGN KEY (Persona_Id) REFERENCES dbo.Persona (Persona_Id);
GO
