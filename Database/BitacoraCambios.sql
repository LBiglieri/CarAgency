-- Bitacora de cambios de vehiculos: historial de altas, modificaciones y ventas, cargado por triggers.
-- No tiene digitos verificadores: las filas las inserta SQL Server y el DVH solo se calcula en la aplicacion.
-- El usuario sale de SESSION_CONTEXT(N'Usuario_Id'), que DataAccessBase carga en cada conexion.
-- Debe coincidir con BE/Vehiculos/BitacoraCambio.cs y BE/Permisos/PermisoType.cs.
-- Permiso y Permiso_Permiso tienen DVH: la patente entra con el digito en cero y hay que sellar
-- esas filas y rehacer el DVV de las dos tablas despues de ejecutar este script.
-- Ejecutar con sqlcmd -I (QUOTED_IDENTIFIER ON).
USE [CarAgency]
GO
SET XACT_ABORT ON
GO

IF OBJECT_ID(N'dbo.BitacoraCambios', N'U') IS NULL
BEGIN
    -- Sin FK a Vehiculo ni a Usuario: el historial sobrevive a la baja del vehiculo o del usuario.
    -- La patente se copia para que el historial se entienda aunque el vehiculo ya no exista.
    CREATE TABLE dbo.BitacoraCambios(
        BitacoraCambios_Id uniqueidentifier NOT NULL CONSTRAINT DF_BitacoraCambios_Id DEFAULT (newid()),
        Vehiculo_Id uniqueidentifier NOT NULL,
        Patente varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
        Fecha datetime NOT NULL CONSTRAINT DF_BitacoraCambios_Fecha DEFAULT (getdate()),
        Operacion int NOT NULL,
        Campo varchar(32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        ValorAnterior nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        ValorNuevo nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        Usuario_Id uniqueidentifier NULL,
        CONSTRAINT PK_BitacoraCambios PRIMARY KEY CLUSTERED (BitacoraCambios_Id),
        -- 1 Alta, 2 Modificacion, 3 Venta. Una modificacion guarda una fila por cada campo que cambio.
        CONSTRAINT CK_BitacoraCambios_Operacion CHECK (Operacion IN (1, 2, 3)),
        CONSTRAINT CK_BitacoraCambios_Campo CHECK ((Operacion = 2 AND Campo IS NOT NULL) OR (Operacion <> 2 AND Campo IS NULL))
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

    CREATE NONCLUSTERED INDEX IX_BitacoraCambios_Vehiculo_Fecha ON dbo.BitacoraCambios (Vehiculo_Id, Fecha);
    CREATE NONCLUSTERED INDEX IX_BitacoraCambios_Fecha ON dbo.BitacoraCambios (Fecha);
END
GO

-- Alta: cualquier insert en Vehiculo (Compra_Finish de CUF10 o el ABM de vehiculos).
CREATE OR ALTER TRIGGER dbo.TR_Vehiculo_Alta ON dbo.Vehiculo AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Usuario_Id uniqueidentifier = TRY_CAST(SESSION_CONTEXT(N'Usuario_Id') AS uniqueidentifier);

    INSERT INTO dbo.BitacoraCambios (Vehiculo_Id, Patente, Operacion, Usuario_Id)
    SELECT i.Vehiculo_Id, i.Patente, 1, @Usuario_Id
    FROM inserted i;
END
GO

-- Modificacion: una fila por cada campo del vehiculo que cambio de valor.
-- Un update que solo reescribe el DVH (recalculo o reparacion de integridad) no deja filas.
-- Marca, modelo, version y color se guardan con su descripcion, no con el Id.
-- La comparacion es binaria para que un cambio de mayusculas tambien quede registrado.
CREATE OR ALTER TRIGGER dbo.TR_Vehiculo_Modificacion ON dbo.Vehiculo AFTER UPDATE AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Usuario_Id uniqueidentifier = TRY_CAST(SESSION_CONTEXT(N'Usuario_Id') AS uniqueidentifier);

    INSERT INTO dbo.BitacoraCambios (Vehiculo_Id, Patente, Operacion, Campo, ValorAnterior, ValorNuevo, Usuario_Id)
    SELECT i.Vehiculo_Id, i.Patente, 2, x.Campo, x.ValorAnterior, x.ValorNuevo, @Usuario_Id
    FROM inserted i
    JOIN deleted d ON d.Vehiculo_Id = i.Vehiculo_Id
    CROSS APPLY (VALUES
        ('Patente', CAST(d.Patente AS nvarchar(max)), CAST(i.Patente AS nvarchar(max))),
        ('Marca', (SELECT CAST(m.Descripcion AS nvarchar(max)) FROM dbo.Marca m WHERE m.Marca_Id = d.Marca_Id),
                  (SELECT CAST(m.Descripcion AS nvarchar(max)) FROM dbo.Marca m WHERE m.Marca_Id = i.Marca_Id)),
        ('Modelo', (SELECT CAST(m.Descripcion AS nvarchar(max)) FROM dbo.Modelo m WHERE m.Modelo_Id = d.Modelo_Id),
                   (SELECT CAST(m.Descripcion AS nvarchar(max)) FROM dbo.Modelo m WHERE m.Modelo_Id = i.Modelo_Id)),
        ('Version', (SELECT CAST(v.Descripcion AS nvarchar(max)) FROM dbo.Version v WHERE v.Version_Id = d.Version_Id),
                    (SELECT CAST(v.Descripcion AS nvarchar(max)) FROM dbo.Version v WHERE v.Version_Id = i.Version_Id)),
        ('Color', (SELECT CAST(col.Descripcion AS nvarchar(max)) FROM dbo.Color col WHERE col.Color_Id = d.Color_Id),
                  (SELECT CAST(col.Descripcion AS nvarchar(max)) FROM dbo.Color col WHERE col.Color_Id = i.Color_Id)),
        ('Precio', CAST(CAST(d.Precio AS decimal(18, 2)) AS nvarchar(max)), CAST(CAST(i.Precio AS decimal(18, 2)) AS nvarchar(max))),
        ('Opcionales', CAST(d.Opcionales AS nvarchar(max)), CAST(i.Opcionales AS nvarchar(max))),
        ('Observaciones', CAST(d.Observaciones AS nvarchar(max)), CAST(i.Observaciones AS nvarchar(max))),
        ('Puertas', CAST(d.Puertas AS nvarchar(max)), CAST(i.Puertas AS nvarchar(max))),
        ('Anio', CAST(d.Anio AS nvarchar(max)), CAST(i.Anio AS nvarchar(max))),
        ('Kilometraje', CAST(d.Kilometraje AS nvarchar(max)), CAST(i.Kilometraje AS nvarchar(max))),
        ('LinkImagen', d.LinkImagen, i.LinkImagen)
    ) x (Campo, ValorAnterior, ValorNuevo)
    -- NULL y '' cuentan como el mismo valor: la pantalla guarda un campo vacio como '', aunque antes fuera NULL.
    WHERE EXISTS (SELECT NULLIF(x.ValorAnterior, N'') COLLATE Latin1_General_100_BIN2
                  EXCEPT
                  SELECT NULLIF(x.ValorNuevo, N'') COLLATE Latin1_General_100_BIN2);
END
GO

-- Venta: la venta se confirma cuando el tramite pasa a finalizado (CUF05).
CREATE OR ALTER TRIGGER dbo.TR_Tramite_Venta ON dbo.Tramite AFTER INSERT, UPDATE AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Usuario_Id uniqueidentifier = TRY_CAST(SESSION_CONTEXT(N'Usuario_Id') AS uniqueidentifier);

    INSERT INTO dbo.BitacoraCambios (Vehiculo_Id, Patente, Operacion, Usuario_Id)
    SELECT i.Vehiculo_Id, v.Patente, 3, @Usuario_Id
    FROM inserted i
    LEFT JOIN deleted d ON d.Tramite_Id = i.Tramite_Id
    JOIN dbo.Vehiculo v ON v.Vehiculo_Id = i.Vehiculo_Id
    WHERE i.Finalizado = 1 AND ISNULL(d.Finalizado, 0) = 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.BitacoraCambios_Query
    @Vehiculo_Id uniqueidentifier = NULL, @From datetime, @Until datetime, @Operacion int = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- El cambio se relaciona con el usuario por Id. El login se resuelve en la consulta.
    SELECT b.BitacoraCambios_Id, b.Vehiculo_Id, b.Patente, b.Fecha, b.Operacion, b.Campo,
        b.ValorAnterior, b.ValorNuevo, b.Usuario_Id,
        ISNULL(u.NombreUsuario, '') AS Login
    FROM dbo.BitacoraCambios b
    LEFT JOIN dbo.Usuario u ON u.Usuario_Id = b.Usuario_Id
    WHERE b.Fecha >= @From AND b.Fecha < @Until
      AND (@Vehiculo_Id IS NULL OR b.Vehiculo_Id = @Vehiculo_Id)
      AND (@Operacion IS NULL OR b.Operacion = @Operacion)
    ORDER BY b.Fecha DESC, b.Patente, b.Campo;
END
GO

BEGIN TRAN

-- Carga inicial: los vehiculos que ya estaban en el inventario entran con su alta.
-- Los que llegaron por una compra toman la fecha de cierre; el resto, la fecha de la carga.
IF NOT EXISTS (SELECT * FROM dbo.BitacoraCambios)
BEGIN
    INSERT INTO dbo.BitacoraCambios (Vehiculo_Id, Patente, Fecha, Operacion)
    SELECT v.Vehiculo_Id, v.Patente, ISNULL(c.FechaCierre, getdate()), 1
    FROM dbo.Vehiculo v
    LEFT JOIN dbo.Compra c ON c.Vehiculo_Id = v.Vehiculo_Id;

    INSERT INTO dbo.BitacoraCambios (Vehiculo_Id, Patente, Fecha, Operacion)
    SELECT t.Vehiculo_Id, v.Patente, ISNULL(t.FechaTransferencia, getdate()), 3
    FROM dbo.Tramite t
    JOIN dbo.Vehiculo v ON v.Vehiculo_Id = t.Vehiculo_Id
    WHERE t.Finalizado = 1;
END

DECLARE @PendingDvh char(64) = REPLICATE('0', 64)

-- Patente de la pantalla; Tipo es el nombre del formulario. La recibe Admin, igual que la bitacora de eventos.
IF NOT EXISTS (SELECT * FROM dbo.Permiso WHERE Permiso_Id = '32d2ede6-fe7c-47f8-94d2-a161dad6d274')
    INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES
        ('32d2ede6-fe7c-47f8-94d2-a161dad6d274', 'Bitacora de cambios', 'BitacoraCambiosForm', @PendingDvh)

IF NOT EXISTS (SELECT * FROM dbo.Permiso_Permiso WHERE Padre_Id = '890db080-56b1-42fa-9f6d-2c3dd685ac3e'
                                                  AND Hijo_Id = '32d2ede6-fe7c-47f8-94d2-a161dad6d274')
    INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES
        ('890db080-56b1-42fa-9f6d-2c3dd685ac3e', '32d2ede6-fe7c-47f8-94d2-a161dad6d274', @PendingDvh) -- Admin

COMMIT
GO
