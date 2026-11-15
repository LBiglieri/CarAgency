-- Renombre de los Id: la clave primaria de cada tabla deja de llamarse Id y pasa a <Tabla>_Id, el mismo formato
-- de las claves foraneas que la referencian (Compra.Persona_Id = Persona.Persona_Id).
-- Solo cambian nombres: la cantidad, el tipo y el orden de las columnas, los datos y la logica de los procedimientos
-- quedan iguales. El DVH y el DVV incluyen el nombre de cada columna, asi que despues hay que migrarlos.
-- Correr completo con: sqlcmd -S . -E -d CarAgency -I -b -i Renombre_Ids.sql
-- Va en una sola transaccion: si algo falla, sqlcmd corta y la base queda como estaba.
SET XACT_ABORT ON
GO
BEGIN TRANSACTION
GO

-- 1. Columnas
EXEC sp_rename N'dbo.ArchivoTramite.Id', N'ArchivoTramite_Id', 'COLUMN'
EXEC sp_rename N'dbo.BitacoraEventos.Id', N'BitacoraEventos_Id', 'COLUMN'
EXEC sp_rename N'dbo.Color.Id', N'Color_Id', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Id', N'Compra_Id', 'COLUMN'
EXEC sp_rename N'dbo.ComprobanteCompra.Id', N'ComprobanteCompra_Id', 'COLUMN'
EXEC sp_rename N'dbo.Cotizacion.Id', N'Cotizacion_Id', 'COLUMN'
EXEC sp_rename N'dbo.DocumentoCompra.Id', N'DocumentoCompra_Id', 'COLUMN'
EXEC sp_rename N'dbo.Factura.Id', N'Factura_Id', 'COLUMN'
EXEC sp_rename N'dbo.FotografiaInspeccion.Id', N'FotografiaInspeccion_Id', 'COLUMN'
EXEC sp_rename N'dbo.Inspeccion.Id', N'Inspeccion_Id', 'COLUMN'
EXEC sp_rename N'dbo.ItemInspeccion.Id', N'ItemInspeccion_Id', 'COLUMN'
EXEC sp_rename N'dbo.Marca.Id', N'Marca_Id', 'COLUMN'
EXEC sp_rename N'dbo.Modelo.Id', N'Modelo_Id', 'COLUMN'
EXEC sp_rename N'dbo.Pago.Id', N'Pago_Id', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.Id', N'PagoCompra_Id', 'COLUMN'
EXEC sp_rename N'dbo.Permiso.Id', N'Permiso_Id', 'COLUMN'
EXEC sp_rename N'dbo.Persona.Id', N'Persona_Id', 'COLUMN'
EXEC sp_rename N'dbo.Reserva.Id', N'Reserva_Id', 'COLUMN'
EXEC sp_rename N'dbo.Tasacion.Id', N'Tasacion_Id', 'COLUMN'
EXEC sp_rename N'dbo.TipoDocumentoCompra.Id', N'TipoDocumentoCompra_Id', 'COLUMN'
EXEC sp_rename N'dbo.TipoPago.Id', N'TipoPago_Id', 'COLUMN'
EXEC sp_rename N'dbo.Tramite.Id', N'Tramite_Id', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Id', N'Usuario_Id', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.Id', N'Vehiculo_Id', 'COLUMN'
EXEC sp_rename N'dbo.VehiculoProspecto.Id', N'VehiculoProspecto_Id', 'COLUMN'
EXEC sp_rename N'dbo.Version.Id', N'Version_Id', 'COLUMN'

-- 2. Restricciones e indices con la columna en el nombre
EXEC sp_rename N'dbo.DF_ArchivoTramite_Id', N'DF_ArchivoTramite_ArchivoTramite_Id', 'OBJECT'
EXEC sp_rename N'dbo.DF_FotografiaInspeccion_Id', N'DF_FotografiaInspeccion_FotografiaInspeccion_Id', 'OBJECT'
GO

-- 3. Procedimientos: se borran para poder recrear los tipos de tabla que usan
DROP PROCEDURE dbo.ArchivoDocumentoCompra_Add, dbo.ArchivoDocumentoCompra_GetByDocument, dbo.ArchivoTramite_Add, dbo.ArchivoTramite_Delete, dbo.ArchivoTramite_GetByPaperWork, dbo.BitacoraEventos_ActorExists, dbo.BitacoraEventos_GetLogins, dbo.BitacoraEventos_Insert
DROP PROCEDURE dbo.BitacoraEventos_Query, dbo.Color_GetAll, dbo.Compra_Add, dbo.Compra_Finish, dbo.Compra_GetById, dbo.Compra_GetDuplicates, dbo.Compra_GetNextNumber, dbo.Compra_GetPendingAppraisal
DROP PROCEDURE dbo.Compra_GetPendingManagement, dbo.Compra_GetPendingPayment, dbo.Compra_GetSummary, dbo.Compra_Pay, dbo.Compra_Update, dbo.ComprobanteCompra_GetByAcquisition, dbo.ComprobanteCompra_GetNextNumber, dbo.Cotizacion_Add
DROP PROCEDURE dbo.Cotizacion_GetAll, dbo.Cotizacion_GetAllActiveByPerson, dbo.Cotizacion_GetById, dbo.DV_GetBaseRoles, dbo.DV_GetByTable, dbo.DV_GetColumns, dbo.DV_GetHorizontal, dbo.DV_GetUsersByRole
DROP PROCEDURE dbo.DV_Save, dbo.DV_ValidateTable, dbo.DocumentoCompra_Add, dbo.DocumentoCompra_Delete, dbo.DocumentoCompra_GetByAcquisition, dbo.DocumentoCompra_Observe, dbo.Factura_Add, dbo.Factura_Delete
DROP PROCEDURE dbo.Factura_GetAllPendingOfPaperworkByPerson, dbo.Factura_GetById, dbo.Factura_Update, dbo.FormaPagoCompra_Add, dbo.FormaPagoCompra_DeleteByAcquisition, dbo.FormaPagoCompra_GetByAcquisition, dbo.FotografiaInspeccion_Add, dbo.FotografiaInspeccion_Delete
DROP PROCEDURE dbo.FotografiaInspeccion_GetByInspection, dbo.Inspeccion_Add, dbo.Inspeccion_GetById, dbo.Inspeccion_GetDetail, dbo.Inspeccion_GetMechanics, dbo.Inspeccion_GetNextFreeSlot, dbo.Inspeccion_GetScheduledByMechanic, dbo.Inspeccion_IsMechanicBusy
DROP PROCEDURE dbo.Inspeccion_Record, dbo.ItemInspeccion_GetAll, dbo.Marca_Delete, dbo.Marca_GetAll, dbo.Marca_Insert, dbo.Modelo_Delete, dbo.Modelo_GetAllByMake, dbo.Modelo_Insert
DROP PROCEDURE dbo.PagoCompra_Add, dbo.PagoCompra_GetByAcquisition, dbo.Pago_Add, dbo.Pago_Delete, dbo.Pago_GetAllByInvoice, dbo.Permiso_DeleteCompleteFamily, dbo.Permiso_DeleteFamily, dbo.Permiso_DeletePatent
DROP PROCEDURE dbo.Permiso_GetAll, dbo.Permiso_GetAllByUser, dbo.Permiso_GetFamilies, dbo.Permiso_GetPatents, dbo.Permiso_InsertComponent, dbo.Permiso_Insert_Permission_Permission, dbo.Persona_Add, dbo.Persona_GetByDni
DROP PROCEDURE dbo.Persona_GetById, dbo.PuntajeInspeccion_GetByInspection, dbo.Reserva_Add, dbo.Reserva_GetAll, dbo.Reserva_GetAllActiveByPerson, dbo.Reserva_GetById, dbo.Tasacion_Add, dbo.Tasacion_GetLastByAcquisition
DROP PROCEDURE dbo.TipoDocumentoCompra_GetByStage, dbo.TipoPago_GetAll, dbo.Tramite_Add, dbo.Tramite_Delete, dbo.Tramite_GetAllActiveByPerson, dbo.Tramite_GetAllByPerson, dbo.Tramite_GetById, dbo.Tramite_Update
DROP PROCEDURE dbo.Usuario_Add, dbo.Usuario_Delete, dbo.Usuario_DeleteAllPermissions, dbo.Usuario_GetAll, dbo.Usuario_GetAllByState, dbo.Usuario_GetUserByDni, dbo.Usuario_GetUserById, dbo.Usuario_GetUserByUsername
DROP PROCEDURE dbo.Usuario_InsertPermission, dbo.Usuario_Update, dbo.VehiculoProspecto_Add, dbo.Vehiculo_Add, dbo.Vehiculo_Delete, dbo.Vehiculo_GetActiveVehiclesByFilters, dbo.Vehiculo_GetAll, dbo.Vehiculo_GetById
DROP PROCEDURE dbo.Vehiculo_Update, dbo.Version_Delete, dbo.Version_GetAllByMakeModel, dbo.Version_Insert
GO

-- 4. Tipos de tabla que cambian (no se pueden renombrar por columna: se borran y se crean de nuevo)
DROP TYPE dbo.UsuarioDvhUpdates
GO
CREATE TYPE [dbo].[UsuarioDvhUpdates] AS TABLE(
	[Usuario_Id] [uniqueidentifier] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Usuario_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF),
	CHECK ((len([DVH])=(64) AND NOT [DVH] like '%[^0-9A-F]%'))
)
GO

-- 5. Procedimientos con los nombres nuevos, con el mismo cuerpo
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
-- Alcance de digitos verificadores. Tiene que coincidir con BE/Integrity/DVTables.cs.
CREATE   PROCEDURE dbo.DV_ValidateTable @NombreTabla nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    IF @NombreTabla IS NULL OR @NombreTabla COLLATE Latin1_General_100_BIN2 NOT IN
        ('DocumentoCompra','TipoDocumentoCompra','PagoCompra','FormaPagoCompra','Compra',
         'Tasacion','Persona','Color','BitacoraEventos','ItemInspeccion','Inspeccion','PuntajeInspeccion','Factura','Marca',
         'Modelo','Tramite','Pago','TipoPago','Permiso_Permiso','Permiso','VehiculoProspecto',
         'ComprobanteCompra','Cotizacion','Reserva','Usuario','Vehiculo','Version')
        THROW 51003, 'Tabla fuera del alcance de digitos verificadores.', 1;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoDocumentoCompra_Add @DocumentoCompra_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max) as

insert into ArchivoDocumentoCompra(DocumentoCompra_Id, NombreArchivo, Contenido)
values (@DocumentoCompra_Id, @NombreArchivo, @Contenido)

select 'success' as SQLResultType,'Acquisition document file added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoDocumentoCompra_GetByDocument @DocumentoCompra_Id uniqueidentifier as

select	a.DocumentoCompra_Id,
        a.NombreArchivo,
        a.Contenido
from	ArchivoDocumentoCompra a
where	a.DocumentoCompra_Id = @DocumentoCompra_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoTramite_Add	@ArchivoTramite_Id uniqueidentifier, @Tramite_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max), @FechaCarga datetime as

insert into ArchivoTramite(ArchivoTramite_Id, Tramite_Id, NombreArchivo, Contenido, FechaCarga) 
values (@ArchivoTramite_Id, @Tramite_Id, @NombreArchivo, @Contenido, @FechaCarga)

select 'success' as SQLResultType,'Paperwork file added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoTramite_Delete	@ArchivoTramite_Id uniqueidentifier as

delete ArchivoTramite
where ArchivoTramite_Id = @ArchivoTramite_Id

select 'success' as SQLResultType,'Paperwork file deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoTramite_GetByPaperWork @Tramite_Id uniqueidentifier as

select	a.ArchivoTramite_Id,
        a.Tramite_Id, 
        a.NombreArchivo, 
        a.Contenido,
        a.FechaCarga
from	ArchivoTramite a
where	a.Tramite_Id = @Tramite_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE dbo.BitacoraEventos_ActorExists @Usuario_Id uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    -- Una restauracion puede dejar sin fila al actor de un evento posterior. La aplicacion
    -- consulta antes de armar la fila para no guardar una referencia que la FK rechazaria.
    SELECT CONVERT(bit,CASE WHEN EXISTS(SELECT 1 FROM dbo.Usuario WHERE Usuario_Id=@Usuario_Id) THEN 1 ELSE 0 END) AS ActorExists;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE dbo.BitacoraEventos_GetLogins
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT COALESCE(u.NombreUsuario,e.LoginIntentado,'') AS Login
    FROM dbo.BitacoraEventos e
    LEFT JOIN dbo.Usuario u ON u.Usuario_Id=e.Usuario_Id
    WHERE COALESCE(u.NombreUsuario,e.LoginIntentado,'')<>''
    ORDER BY Login;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE dbo.BitacoraEventos_Insert
    @BitacoraEventos_Id uniqueidentifier, @Usuario_Id uniqueidentifier, @LoginIntentado nvarchar(256),
    @Fecha datetime, @Modulo varchar(32), @TipoEvento varchar(64),
    @Criticidad int, @Objetivo_Id uniqueidentifier, @DVH char(64)
AS
BEGIN
    SET NOCOUNT ON;
    -- El actor se resuelve en la aplicacion antes de calcular el DVH. Este procedimiento
    -- no puede alterar ningun valor: cualquier cambio invalidaria el digito horizontal.
    INSERT dbo.BitacoraEventos(BitacoraEventos_Id,Usuario_Id,LoginIntentado,Fecha,Modulo,TipoEvento,Criticidad,Objetivo_Id,DVH)
    VALUES(@BitacoraEventos_Id,@Usuario_Id,@LoginIntentado,@Fecha,@Modulo,@TipoEvento,@Criticidad,@Objetivo_Id,@DVH);
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE dbo.BitacoraEventos_Query
    @Login nvarchar(256)=NULL, @From datetime, @Until datetime,
    @Modulo varchar(32)=NULL, @TipoEvento varchar(64)=NULL, @Criticidad int=NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- El evento se relaciona con el usuario por Id. El login se resuelve en la consulta;
    -- AttemptedLogin solo tiene valor cuando el intento no correspondio a ningun usuario.
    SELECT e.BitacoraEventos_Id, e.Usuario_Id,
        COALESCE(u.NombreUsuario,e.LoginIntentado,'') AS Login,
        ISNULL(u.Nombre,'') AS Nombre,
        ISNULL(u.Apellido,'') AS Apellido,
        e.Fecha, e.Modulo, e.TipoEvento, e.Criticidad, e.Objetivo_Id
    FROM dbo.BitacoraEventos e
    LEFT JOIN dbo.Usuario u ON u.Usuario_Id=e.Usuario_Id
    WHERE e.Fecha>=@From AND e.Fecha<@Until
      AND (@Login IS NULL OR COALESCE(u.NombreUsuario,e.LoginIntentado,'')=@Login)
      AND (@Modulo IS NULL OR e.Modulo=@Modulo)
      AND (@TipoEvento IS NULL OR e.TipoEvento=@TipoEvento)
      AND (@Criticidad IS NULL OR e.Criticidad=@Criticidad)
    ORDER BY e.Fecha DESC,e.BitacoraEventos_Id DESC;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Color_GetAll as

select Color_Id, Descripcion 
from Color 
order by Descripcion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_Add	@Compra_Id uniqueidentifier, @Numero int, @Persona_Id uniqueidentifier, @VehiculoProspecto_Id uniqueidentifier, @Vendedor_Id uniqueidentifier,
									@Estado int, @MotivoRechazo int, @Fecha datetime, @TasacionAceptada_Id uniqueidentifier, @PrecioAcordado float,
									@CuilCuitTitular varchar(13), @FechaFirmaContrato datetime, @CodigoPrecarga08 varchar(50), @FechaCitaTransferencia datetime,
									@Vehiculo_Id uniqueidentifier, @FechaCierre datetime, @DVH char(64) as

insert into Compra(Compra_Id, Numero, Persona_Id, VehiculoProspecto_Id, Vendedor_Id, Estado, MotivoRechazo, Fecha, TasacionAceptada_Id, PrecioAcordado,
						 CuilCuitTitular, FechaFirmaContrato, CodigoPrecarga08, FechaCitaTransferencia, Vehiculo_Id, FechaCierre, DVH)
values (@Compra_Id, @Numero, @Persona_Id, @VehiculoProspecto_Id, @Vendedor_Id, @Estado, @MotivoRechazo, @Fecha, @TasacionAceptada_Id, @PrecioAcordado,
		@CuilCuitTitular, @FechaFirmaContrato, @CodigoPrecarga08, @FechaCitaTransferencia, @Vehiculo_Id, @FechaCierre, @DVH)

select 'success' as SQLResultType,'Acquisition added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_Finish	@Vehiculo_Id uniqueidentifier, @Patente varchar(255), @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier,
										@Version_Id uniqueidentifier, @Color_Id uniqueidentifier, @Precio float, @Opcionales varchar(255),
										@Observaciones varchar(max), @Puertas int, @Anio int, @Kilometraje int, @LinkImagen nvarchar(max), @DVH char(64),
										@Compra_Id uniqueidentifier, @Estado int, @FechaCierre datetime, @Acquisition_DVH char(64) as

-- CUF10 pasos 23 y 24: el vehiculo entra al inventario y la compra se finaliza juntos o no se graba nada.
-- Si quedara solo el vehiculo, apareceria disponible en CUF01 con la compra sin cerrar.
-- .NET calcula cada DVH con la fila completa; en Acquisitions solo se escriben las columnas que cambian.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Compra where Compra_Id = @Compra_Id and Estado = 7)
		throw 51011, 'La adquisicion ya no esta en estado Transferida.', 1;

	insert into Vehiculo(Vehiculo_Id,Patente,Marca_Id,Modelo_Id,Version_Id,Color_Id,Precio,Opcionales,Observaciones,Puertas,Anio,Kilometraje,LinkImagen,DVH)
	values (@Vehiculo_Id,@Patente,@Marca_Id,@Modelo_Id,@Version_Id,@Color_Id,@Precio,@Opcionales,@Observaciones,@Puertas,@Anio,@Kilometraje,@LinkImagen,@DVH)

	update Compra
	set DVH = @Acquisition_DVH,
		Estado = @Estado,
		Vehiculo_Id = @Vehiculo_Id,
		FechaCierre = @FechaCierre
	where Compra_Id = @Compra_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Acquisition finished succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetById @Compra_Id uniqueidentifier as

-- Fila completa: cualquier cambio de estado recalcula el DVH con todas las columnas.
select	a.Compra_Id,
        a.Numero,
        a.Persona_Id,
        a.VehiculoProspecto_Id,
        a.Vendedor_Id,
        a.Estado,
        a.MotivoRechazo,
        a.Fecha,
        a.TasacionAceptada_Id,
        a.PrecioAcordado,
        a.CuilCuitTitular,
        a.FechaFirmaContrato,
        a.CodigoPrecarga08,
        a.FechaCitaTransferencia,
        a.Vehiculo_Id,
        a.FechaCierre
from	Compra a
where	a.Compra_Id = @Compra_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetDuplicates @Patente varchar(10), @NumeroChasis varchar(17), @NumeroMotor varchar(20) as

-- Inventario (Number = 0): vehiculos sin factura pagada. Chasis y motor solo se conocen
-- de los que entraron por una adquisicion; el resto se compara por patente.
select	0 as Numero,
        a.Patente,
        isnull(g.NumeroChasis, '') as NumeroChasis,
        isnull(g.NumeroMotor, '') as NumeroMotor,
        (cast(a.Anio as varchar) + ' ' + e.Descripcion + ' ' + b.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + a.Patente) as Vehiculo_Descripcion
from	Vehiculo a
        join Marca b on (b.Marca_Id = a.Marca_Id)
		join Modelo c on (c.Modelo_Id = a.Modelo_Id)
		join Version d on (d.Version_Id = a.Version_Id)
		join Color e on (e.Color_Id = a.Color_Id)
        left join Compra f on (f.Vehiculo_Id = a.Vehiculo_Id)
        left join VehiculoProspecto g on (g.VehiculoProspecto_Id = f.VehiculoProspecto_Id)
where	(a.Patente = @Patente or g.NumeroChasis = @NumeroChasis or g.NumeroMotor = @NumeroMotor)
        and not exists (select * from Factura i where i.Vehiculo_Id = a.Vehiculo_Id and i.EstadoPago = 1)

union all

-- Adquisiciones abiertas: de Inspeccion programada (1) a Transferida (7).
select	a.Numero,
        g.Patente,
        g.NumeroChasis,
        g.NumeroMotor,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + b.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + g.Patente) as Vehiculo_Descripcion
from	Compra a
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        join Marca b on (b.Marca_Id = g.Marca_Id)
		join Modelo c on (c.Modelo_Id = g.Modelo_Id)
		join Version d on (d.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
where	a.Estado between 1 and 7
        and (g.Patente = @Patente or g.NumeroChasis = @NumeroChasis or g.NumeroMotor = @NumeroMotor)
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetNextNumber as

-- El numero entra en el DVH: se pide antes del insert y no puede salir de un IDENTITY.
select NEXT VALUE FOR dbo.CompraNumbers as Numero
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetPendingAppraisal @Filter varchar(20), @Now datetime as

-- Adquisiciones a tasar: Inspeccionado (2), o Tasada (3) con la ultima tasacion vencida.
-- El filtro busca por DNI exacto o por el comienzo de la matricula; vacio trae todas.
select	a.Compra_Id,
        a.Numero,
        b.Inspeccion_Id as Inspeccion_Id,
        a.Persona_Id,
        h.DNI,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        b.FechaRealizada,
        (select count(*) from Tasacion x where x.Compra_Id = a.Compra_Id) as TasacionesPrevias
from	Compra a
        join Inspeccion b on (b.Compra_Id = a.Compra_Id)
        join Persona h on (h.Persona_Id = a.Persona_Id)
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
where	(a.Estado = 2
            or (a.Estado = 3 and (select top 1 x.Vigencia from Tasacion x where x.Compra_Id = a.Compra_Id order by x.Fecha desc) < @Now))
        and (isnull(@Filter, '') = ''
            or h.DNI = try_cast(@Filter as int)
            or g.Patente like @Filter + '%')
order by b.FechaRealizada asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetPendingManagement @Filter varchar(20) as

-- Compras en gestion (CUF10): Tasada (3), Compra formalizada (4), Documentacion completa (5), Pagada (6) y Transferida (7).
-- El filtro busca por DNI exacto o por el comienzo de la matricula; vacio trae todas.
select	a.Compra_Id,
        a.Numero,
        b.Inspeccion_Id as Inspeccion_Id,
        a.Persona_Id,
        h.DNI,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        a.Estado
from	Compra a
        join Inspeccion b on (b.Compra_Id = a.Compra_Id)
        join Persona h on (h.Persona_Id = a.Persona_Id)
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
where	a.Estado between 3 and 7
        and (isnull(@Filter, '') = ''
            or h.DNI = try_cast(@Filter as int)
            or g.Patente like @Filter + '%')
order by a.Numero asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetPendingPayment @Persona_Id uniqueidentifier as

-- Compras de la persona para el cajero (CUF11 paso 2): las de Documentacion completa (5), con saldo o con el
-- saldo cancelado y el comprobante sin emitir, y las que ya tienen comprobante para volver a imprimirlo.
select	a.Compra_Id,
        a.Numero,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        a.PrecioAcordado,
        isnull((select sum(y.Monto) from PagoCompra y where y.Compra_Id = a.Compra_Id), 0) as MontoPagado,
        r.Numero as ComprobanteCompra_Numero,
        a.Estado
from	Compra a
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
        left join ComprobanteCompra r on (r.Compra_Id = a.Compra_Id)
where	a.Persona_Id = @Persona_Id
        and (a.Estado = 5 or r.ComprobanteCompra_Id is not null)
order by a.Numero asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetSummary @Compra_Id uniqueidentifier as

-- Resumen completo de la compra (CUF10 paso 20): vehiculo, inspeccion, tasacion aceptada, acuerdo,
-- lo pagado y el comprobante de CUF11 y la transferencia. Los datos de la persona se leen aparte porque estan encriptados.
select	a.Compra_Id,
        a.Numero,
        a.Persona_Id,
        h.DNI,
        g.Patente,
        g.NumeroChasis,
        g.NumeroMotor,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        g.Marca_Id,
        g.Modelo_Id,
        g.Version_Id,
        g.Color_Id,
        g.Anio,
        g.Puertas,
        g.Kilometraje,
        b.Inspeccion_Id as Inspeccion_Id,
        b.FechaRealizada,
        b.KilometrajeVerificado,
        x.Valor as Tasacion_Valor,
        x.Fecha as Tasacion_Fecha,
        a.PrecioAcordado,
        a.CuilCuitTitular,
        a.FechaFirmaContrato,
        isnull((select sum(y.Monto) from PagoCompra y where y.Compra_Id = a.Compra_Id), 0) as MontoPagado,
        r.Numero as ComprobanteCompra_Numero,
        r.Fecha as ComprobanteCompra_Fecha,
        a.CodigoPrecarga08,
        a.FechaCitaTransferencia,
        a.FechaCierre
from	Compra a
        join Inspeccion b on (b.Compra_Id = a.Compra_Id)
        join Persona h on (h.Persona_Id = a.Persona_Id)
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
        left join Tasacion x on (x.Tasacion_Id = a.TasacionAceptada_Id)
        left join ComprobanteCompra r on (r.Compra_Id = a.Compra_Id)
where	a.Compra_Id = @Compra_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_Pay	@ComprobanteCompra_Id uniqueidentifier, @Numero int, @Compra_Id uniqueidentifier, @Fecha datetime, @NombrePersona varchar(255),
									@CuilCuit varchar(13), @Detalle varchar(max), @MontoNeto float, @MontoImpuesto float, @MontoTotal float,
									@DVH char(64), @Estado int, @Acquisition_DVH char(64) as

-- CUF11 pasos 7 y 9: el comprobante y el pase a Pagada van juntos o no se graba nada. Un comprobante con la
-- compra sin pagar no se podria volver a emitir, y una compra pagada sin comprobante no se podria reimprimir.
-- Los pagos ya estan grabados: si se cae antes de este paso, la compra queda con saldo cero y se reintenta.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Compra where Compra_Id = @Compra_Id and Estado = 5)
		throw 51012, 'La adquisicion ya no esta en estado Documentacion completa.', 1;

	if exists (select * from FormaPagoCompra t
				where t.Compra_Id = @Compra_Id
				and round(t.Monto - isnull((select sum(x.Monto) from PagoCompra x
										where x.Compra_Id = t.Compra_Id and x.TipoPago_Id = t.TipoPago_Id), 0), 2) <> 0)
		throw 51013, 'La compra todavia tiene saldo pendiente.', 1;

	insert into ComprobanteCompra(ComprobanteCompra_Id, Numero, Compra_Id, Fecha, NombrePersona, CuilCuit, Detalle, MontoNeto, MontoImpuesto, MontoTotal, DVH)
	values (@ComprobanteCompra_Id, @Numero, @Compra_Id, @Fecha, @NombrePersona, @CuilCuit, @Detalle, @MontoNeto, @MontoImpuesto, @MontoTotal, @DVH)

	update Compra
	set DVH = @Acquisition_DVH,
		Estado = @Estado
	where Compra_Id = @Compra_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Acquisition paid succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_Update	@Compra_Id uniqueidentifier, @Numero int, @Persona_Id uniqueidentifier, @VehiculoProspecto_Id uniqueidentifier, @Vendedor_Id uniqueidentifier,
											@Estado int, @MotivoRechazo int, @Fecha datetime, @TasacionAceptada_Id uniqueidentifier, @PrecioAcordado float,
											@CuilCuitTitular varchar(13), @FechaFirmaContrato datetime, @CodigoPrecarga08 varchar(50), @FechaCitaTransferencia datetime,
											@Vehiculo_Id uniqueidentifier, @FechaCierre datetime, @DVH char(64) as

-- Fila completa: el DVH se recalcula con todas las columnas aunque solo cambie el estado.
update Compra
set DVH = @DVH,
	Numero = @Numero,
	Persona_Id = @Persona_Id,
	VehiculoProspecto_Id = @VehiculoProspecto_Id,
	Vendedor_Id = @Vendedor_Id,
	Estado = @Estado,
	MotivoRechazo = @MotivoRechazo,
	Fecha = @Fecha,
	TasacionAceptada_Id = @TasacionAceptada_Id,
	PrecioAcordado = @PrecioAcordado,
	CuilCuitTitular = @CuilCuitTitular,
	FechaFirmaContrato = @FechaFirmaContrato,
	CodigoPrecarga08 = @CodigoPrecarga08,
	FechaCitaTransferencia = @FechaCitaTransferencia,
	Vehiculo_Id = @Vehiculo_Id,
	FechaCierre = @FechaCierre
where Compra_Id = @Compra_Id

select 'success' as SQLResultType,'Acquisition updated succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ComprobanteCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- El PDF del comprobante se arma siempre con lo que se guardo al emitirlo.
select	a.ComprobanteCompra_Id,
        a.Numero,
        a.Compra_Id,
        a.Fecha,
        a.NombrePersona,
        a.CuilCuit,
        a.Detalle,
        a.MontoNeto,
        a.MontoImpuesto,
        a.MontoTotal
from	ComprobanteCompra a
where	a.Compra_Id = @Compra_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ComprobanteCompra_GetNextNumber as

-- Igual que el numero de adquisicion: entra en el DVH, asi que se pide antes del insert.
select NEXT VALUE FOR dbo.ComprobanteCompraNumbers as Numero
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Cotizacion_Add	@Cotizacion_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Precio float, @Fecha datetime , @DVH char(64) as

insert into Cotizacion(Cotizacion_Id, Vehiculo_Id, Persona_Id, Precio, Fecha,DVH)
values (@Cotizacion_Id, @Vehiculo_Id, @Persona_Id, @Precio, @Fecha,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Cotizacion_GetAll @Cotizacion_Id uniqueidentifier as

select	a.Cotizacion_Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha
from	Cotizacion a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
        join Persona g on (g.Persona_Id = a.Persona_Id)
where	a.Cotizacion_Id = @Cotizacion_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Cotizacion_GetAllActiveByPerson @Persona_Id uniqueidentifier as

select	a.Cotizacion_Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha
from	Cotizacion a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
        join Persona g on (g.Persona_Id = a.Persona_Id)
where	a.Persona_Id = @Persona_Id
        and cast(a.Fecha as date) = cast(GETDATE() as date)
        and not exists 
        (
            select * 
            from Reserva r 
            left join Factura i on (i.Reserva_Id = r.Reserva_Id and i.EstadoPago = 1)
            where r.Vehiculo_Id = a.Vehiculo_Id 
            and 
            (
                i.Reserva_Id is not null
                or 
                (cast(getdate() as date) >= cast(r.Fecha as date)) and (cast(getdate() as date) <= cast(r.FechaVencimiento as date))
            )
        )
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Cotizacion_GetById @Cotizacion_Id uniqueidentifier as

select	a.Cotizacion_Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha
from	Cotizacion a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
        join Persona g on (g.Persona_Id = a.Persona_Id)
where	a.Cotizacion_Id = @Cotizacion_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_GetBaseRoles
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Permiso_Id,Nombre FROM dbo.Permiso WHERE Nombre='Base User' AND Tipo IS NULL;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_GetByTable @NombreTabla nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    SELECT NombreEsquema,NombreTabla,DVV,VersionAlgoritmo
    FROM dbo.DV WHERE NombreEsquema='dbo' AND NombreTabla=@NombreTabla;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_GetColumns @NombreTabla nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    SELECT c.name AS Name, ty.name AS SqlType, CONVERT(int,c.max_length) AS MaxLength,
        c.is_nullable AS IsNullable,
        ISNULL(CONVERT(int,COLLATIONPROPERTY(c.collation_name,'CodePage')),0) AS CodePage,
        CONVERT(bit,CASE WHEN EXISTS (
            SELECT 1 FROM sys.indexes i
            JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id
            WHERE i.object_id=c.object_id AND i.is_primary_key=1 AND ic.column_id=c.column_id
        ) THEN 1 ELSE 0 END) AS IsKey
    FROM sys.columns c JOIN sys.types ty ON ty.user_type_id=c.user_type_id
    WHERE c.object_id=OBJECT_ID(N'dbo.'+QUOTENAME(@NombreTabla)) AND c.name<>'DVH'
    ORDER BY c.column_id;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_GetHorizontal @NombreTabla nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    DECLARE @Sql nvarchar(max)=N'SELECT DVH FROM dbo.'+QUOTENAME(@NombreTabla);
    EXEC sys.sp_executesql @Sql;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_GetUsersByRole @RoleId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Usuario WHERE Rol_Id=@RoleId;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_Save
    @NombreTabla nvarchar(128), @DVV char(64)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    UPDATE dbo.DV SET DVV=@DVV, VersionAlgoritmo=1
        WHERE NombreEsquema='dbo' AND NombreTabla=@NombreTabla;
    IF @@ROWCOUNT=0
        INSERT INTO dbo.DV(NombreEsquema,NombreTabla,DVV,VersionAlgoritmo)
        VALUES ('dbo',@NombreTabla,@DVV,1);
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc DocumentoCompra_Add	@DocumentoCompra_Id uniqueidentifier, @Compra_Id uniqueidentifier, @TipoDocumentoCompra_Id uniqueidentifier, @Estado int,
											@FechaEmision datetime, @Observacion varchar(max), @UsuarioCarga_Id uniqueidentifier, @FechaCarga datetime, @DVH char(64) as

insert into DocumentoCompra(DocumentoCompra_Id, Compra_Id, TipoDocumentoCompra_Id, Estado, FechaEmision, Observacion, UsuarioCarga_Id, FechaCarga, DVH)
values (@DocumentoCompra_Id, @Compra_Id, @TipoDocumentoCompra_Id, @Estado, @FechaEmision, @Observacion, @UsuarioCarga_Id, @FechaCarga, @DVH)

select 'success' as SQLResultType,'Acquisition document added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc DocumentoCompra_Delete @DocumentoCompra_Id uniqueidentifier as

-- El archivo escaneado se borra en cascada (AcquisitionDocumentFiles).
delete from DocumentoCompra
where DocumentoCompra_Id = @DocumentoCompra_Id

select 'success' as SQLResultType,'Acquisition document deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc DocumentoCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- Documentos cargados de las dos etapas. Un tipo sin fila es un documento Pendiente.
select	a.DocumentoCompra_Id,
        a.Compra_Id,
        a.TipoDocumentoCompra_Id,
        a.Estado,
        a.FechaEmision,
        a.Observacion,
        a.UsuarioCarga_Id,
        a.FechaCarga,
        b.Descripcion,
        b.Etapa,
        c.NombreArchivo
from	DocumentoCompra a
        join TipoDocumentoCompra b on (b.TipoDocumentoCompra_Id = a.TipoDocumentoCompra_Id)
        left join ArchivoDocumentoCompra c on (c.DocumentoCompra_Id = a.DocumentoCompra_Id)
where	a.Compra_Id = @Compra_Id
order by b.Orden asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc DocumentoCompra_Observe	@DocumentoCompra_Id uniqueidentifier, @Compra_Id uniqueidentifier, @TipoDocumentoCompra_Id uniqueidentifier, @Estado int,
												@FechaEmision datetime, @Observacion varchar(max), @UsuarioCarga_Id uniqueidentifier, @FechaCarga datetime, @DVH char(64) as

-- CUF10 11.1: fila completa, el DVH se recalcula con todas las columnas aunque solo cambien estado y comentario.
update DocumentoCompra
set DVH = @DVH,
	Compra_Id = @Compra_Id,
	TipoDocumentoCompra_Id = @TipoDocumentoCompra_Id,
	Estado = @Estado,
	FechaEmision = @FechaEmision,
	Observacion = @Observacion,
	UsuarioCarga_Id = @UsuarioCarga_Id,
	FechaCarga = @FechaCarga
where DocumentoCompra_Id = @DocumentoCompra_Id

select 'success' as SQLResultType,'Acquisition document observed succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_Add	@Factura_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Reserva_Id uniqueidentifier, @Detalle varchar(max),
									@CuilCuit varchar(max), @RazonSocial varchar(max), @Valor float, @EstadoPago bit, @Fecha datetime , @DVH char(64) as

insert into Factura(Factura_Id, Vehiculo_Id, Persona_Id, Reserva_Id, Detalle, CuilCuit, RazonSocial, Valor, EstadoPago, Fecha,DVH)
values (@Factura_Id, @Vehiculo_Id, @Persona_Id, @Reserva_Id, @Detalle, @CuilCuit, @RazonSocial, @Valor, @EstadoPago, @Fecha,@DVH)

select 'success' as SQLResultType,'Invoice added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_Delete	@Factura_Id uniqueidentifier as

delete Factura
where Factura_Id = @Factura_Id

select 'success' as SQLResultType,'Invoice deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_GetAllPendingOfPaperworkByPerson @Persona_Id uniqueidentifier as

select	a.Factura_Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Reserva_Id, 
        a.Detalle, 
        a.CuilCuit,
        a.RazonSocial, 
        a.Valor,
        a.EstadoPago, 
        a.Fecha
from	Factura a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
where   a.EstadoPago = 1
        and a.Persona_Id = @Persona_Id
        and not exists 
        (
            select * from Tramite x where x.Factura_Id = a.Factura_Id
        )
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_GetById @Factura_Id uniqueidentifier as

select	a.Factura_Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Reserva_Id, 
        a.Detalle, 
        a.CuilCuit,
        a.RazonSocial, 
        a.Valor,
        a.EstadoPago, 
        a.Fecha
from	Factura a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
where	a.Factura_Id = @Factura_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_Update	@Factura_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Reserva_Id uniqueidentifier, @Detalle varchar(max),
										@CuilCuit varchar(max), @RazonSocial varchar(max), @Valor float, @EstadoPago bit, @Fecha datetime , @DVH char(64) as

update Factura
set DVH = @DVH,
	Vehiculo_Id = @Vehiculo_Id,
	Persona_Id = @Persona_Id,
	Reserva_Id = @Reserva_Id,
	Detalle = @Detalle,
	CuilCuit = @CuilCuit,
	RazonSocial = @RazonSocial,
	Valor = @Valor,
	EstadoPago = @EstadoPago,
	Fecha = @Fecha
where Factura_Id = @Factura_Id

select 'success' as SQLResultType,'Invoice updated succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FormaPagoCompra_Add	@Compra_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Monto float, @DVH char(64) as

insert into FormaPagoCompra(Compra_Id, TipoPago_Id, Monto, DVH)
values (@Compra_Id, @TipoPago_Id, @Monto, @DVH)

select 'success' as SQLResultType,'Payment term added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FormaPagoCompra_DeleteByAcquisition @Compra_Id uniqueidentifier as

-- La forma de pago se reemplaza completa mientras el contrato no este firmado.
delete from FormaPagoCompra
where Compra_Id = @Compra_Id

select 'success' as SQLResultType,'Payment terms deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FormaPagoCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- Forma de pago pactada y lo que el cajero ya pago por cada medio (CUF11).
select	a.Compra_Id,
        a.TipoPago_Id,
        b.Descripcion as TipoPago_Descripcion,
        a.Monto,
        isnull((select sum(x.Monto) from PagoCompra x
                where x.Compra_Id = a.Compra_Id and x.TipoPago_Id = a.TipoPago_Id), 0) as MontoPagado
from	FormaPagoCompra a
        join TipoPago b on (b.TipoPago_Id = a.TipoPago_Id)
where	a.Compra_Id = @Compra_Id
order by a.Monto desc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FotografiaInspeccion_Add	@FotografiaInspeccion_Id uniqueidentifier, @Inspeccion_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max), @FechaCarga datetime as

insert into FotografiaInspeccion(FotografiaInspeccion_Id, Inspeccion_Id, NombreArchivo, Contenido, FechaCarga)
values (@FotografiaInspeccion_Id, @Inspeccion_Id, @NombreArchivo, @Contenido, @FechaCarga)

select 'success' as SQLResultType,'Inspection photo added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FotografiaInspeccion_Delete @FotografiaInspeccion_Id uniqueidentifier as

delete from FotografiaInspeccion
where FotografiaInspeccion_Id = @FotografiaInspeccion_Id

select 'success' as SQLResultType,'Inspection photo deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FotografiaInspeccion_GetByInspection @Inspeccion_Id uniqueidentifier as

select	a.FotografiaInspeccion_Id,
        a.Inspeccion_Id,
        a.NombreArchivo,
        a.Contenido,
        a.FechaCarga
from	FotografiaInspeccion a
where	a.Inspeccion_Id = @Inspeccion_Id
order by a.FechaCarga asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_Add	@Inspeccion_Id uniqueidentifier, @Compra_Id uniqueidentifier, @Mecanico_Id uniqueidentifier, @FechaProgramada datetime,
									@FechaRealizada datetime, @KilometrajeVerificado int, @Resultado int, @Observaciones varchar(max), @DVH char(64) as

insert into Inspeccion(Inspeccion_Id, Compra_Id, Mecanico_Id, FechaProgramada, FechaRealizada, KilometrajeVerificado, Resultado, Observaciones, DVH)
values (@Inspeccion_Id, @Compra_Id, @Mecanico_Id, @FechaProgramada, @FechaRealizada, @KilometrajeVerificado, @Resultado, @Observaciones, @DVH)

select 'success' as SQLResultType,'Inspection added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_GetById @Inspeccion_Id uniqueidentifier as

-- Resultado de la inspeccion registrada en CUF08.
select	a.Inspeccion_Id,
        a.Compra_Id,
        a.Mecanico_Id,
        a.FechaProgramada,
        a.FechaRealizada,
        a.KilometrajeVerificado,
        a.Resultado,
        a.Observaciones
from	Inspeccion a
where	a.Inspeccion_Id = @Inspeccion_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_GetDetail @Inspeccion_Id uniqueidentifier as

-- Datos del vehiculo que se inspecciona, con lo declarado en CUF07.
select	a.Inspeccion_Id,
        a.Compra_Id,
        b.Numero,
        g.Patente,
        g.NumeroChasis,
        g.NumeroMotor,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        g.Puertas,
        g.Kilometraje
from	Inspeccion a
        join Compra b on (b.Compra_Id = a.Compra_Id)
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = b.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
where	a.Inspeccion_Id = @Inspeccion_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_GetMechanics @Tipo varchar(50) as

-- Mecanico: usuario activo y no bloqueado cuyo rol tiene la patente @Type en cualquier nivel.
with recursive as	(
						select	sp2.Padre_Id as Rol_Id, sp2.Hijo_Id
						from	Permiso_Permiso SP2
						UNION ALL
						select	r.Rol_Id, sp.Hijo_Id
						from	Permiso_Permiso sp
								inner join recursive r on (r.Hijo_Id=sp.Padre_Id)
					)
select	u.Usuario_Id, u.DNI, u.NombreUsuario, Clave='' , u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u
where	u.Activo = 1
		and u.Bloqueado = 0
		and exists	(
						select	*
						from	recursive r
								inner join Permiso p on (r.Hijo_Id = p.Permiso_Id)
						where	r.Rol_Id = u.Rol_Id
								and p.Tipo = @Tipo
					)
order by u.NombreUsuario asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_GetNextFreeSlot	@Mecanico_Id uniqueidentifier, @From datetime, @Slot_Minutes int, @Day_Start_Hour int,
												@Day_End_Hour int, @Max_Days int as

-- Turnos candidatos desde el dia de @From. El 01/01/1900 fue lunes: datediff(day, 0, fecha) % 7
-- da 0 a 4 de lunes a viernes sin depender de SET DATEFIRST.
with slots as	(
					select	dateadd(hour, @Day_Start_Hour, cast(cast(@From as date) as datetime)) as Turno
					UNION ALL
					select	dateadd(minute, @Slot_Minutes, s.Turno)
					from	slots s
					where	s.Turno < dateadd(day, @Max_Days, @From)
				)
select	top 1 s.Turno as FechaProgramada
from	slots s
where	s.Turno >= @From
		and datediff(day, 0, s.Turno) % 7 < 5
		and datepart(hour, s.Turno) >= @Day_Start_Hour
		and dateadd(minute, @Slot_Minutes, s.Turno) <= dateadd(hour, @Day_End_Hour, cast(cast(s.Turno as date) as datetime))
		and not exists	(
							select	*
							from	Inspeccion a
									join Compra b on (b.Compra_Id = a.Compra_Id)
							where	a.Mecanico_Id = @Mecanico_Id
									and b.Estado = 1
									and a.FechaProgramada > dateadd(minute, -@Slot_Minutes, s.Turno)
									and a.FechaProgramada < dateadd(minute, @Slot_Minutes, s.Turno)
						)
order by s.Turno
option (maxrecursion 0)
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_GetScheduledByMechanic @Mecanico_Id uniqueidentifier as

-- Inspecciones pendientes del mecanico: adquisiciones en estado Inspeccion programada (1).
select	a.Inspeccion_Id,
        a.Compra_Id,
        a.Mecanico_Id,
        a.FechaProgramada,
        b.Numero,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion
from	Inspeccion a
        join Compra b on (b.Compra_Id = a.Compra_Id)
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = b.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
where	a.Mecanico_Id = @Mecanico_Id
        and b.Estado = 1
order by a.FechaProgramada asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_IsMechanicBusy @Mecanico_Id uniqueidentifier, @FechaProgramada datetime, @Slot_Minutes int as

-- Solo ocupan la agenda las inspecciones de adquisiciones en estado Inspeccion programada (1).
select	count(*) as Inspecciones
from	Inspeccion a
        join Compra b on (b.Compra_Id = a.Compra_Id)
where	a.Mecanico_Id = @Mecanico_Id
        and b.Estado = 1
        and a.FechaProgramada > dateadd(minute, -@Slot_Minutes, @FechaProgramada)
        and a.FechaProgramada < dateadd(minute, @Slot_Minutes, @FechaProgramada)
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_Record	@Inspeccion_Id uniqueidentifier, @Compra_Id uniqueidentifier, @FechaRealizada datetime, @KilometrajeVerificado int,
									@Resultado int, @Observaciones varchar(max), @DVH char(64), @Scores dbo.PuntajeInspeccionRows READONLY,
									@Estado int, @MotivoRechazo int, @Acquisition_DVH char(64) as

-- CUF08: la inspeccion, sus calificaciones y el estado de la adquisicion se graban juntos o no se graba nada.
-- .NET calcula cada DVH con la fila completa; aca solo se escriben las columnas que cambian.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Compra where Compra_Id = @Compra_Id and Estado = 1)
		throw 51010, 'La adquisicion ya no esta en estado Inspeccion programada.', 1;

	update Inspeccion
	set DVH = @DVH,
		FechaRealizada = @FechaRealizada,
		KilometrajeVerificado = @KilometrajeVerificado,
		Resultado = @Resultado,
		Observaciones = @Observaciones
	where Inspeccion_Id = @Inspeccion_Id
		and Compra_Id = @Compra_Id

	insert into PuntajeInspeccion(Inspeccion_Id, ItemInspeccion_Id, Puntaje, Observaciones, DVH)
	select @Inspeccion_Id, a.ItemInspeccion_Id, a.Puntaje, a.Observaciones, a.DVH
	from @Scores a

	update Compra
	set DVH = @Acquisition_DVH,
		Estado = @Estado,
		MotivoRechazo = @MotivoRechazo
	where Compra_Id = @Compra_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Inspection recorded succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ItemInspeccion_GetAll as

select	a.ItemInspeccion_Id,
        a.Descripcion,
        a.Orden
from	ItemInspeccion a
order by a.Orden asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Marca_Delete	@Marca_Id uniqueidentifier as

delete Marca
where Marca_Id = @Marca_Id

select 'success' as SQLResultType,'Make deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Marca_GetAll as

select Marca_Id, Descripcion 
from Marca 
order by Descripcion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Marca_Insert @Marca_Id uniqueidentifier, @Descripcion varchar(50) , @DVH char(64) as

if not exists (select * from Marca where Descripcion = @Descripcion)
begin
	insert into Marca(Marca_Id,Descripcion,DVH) values (@Marca_Id,@Descripcion,@DVH);
end

select 'success' as SQLResultType,'Make added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Modelo_Delete	@Modelo_Id uniqueidentifier as

delete Modelo
where Modelo_Id = @Modelo_Id

select 'success' as SQLResultType,'Model deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Modelo_GetAllByMake @Marca_Id uniqueidentifier as

select	a.Modelo_Id,a.Marca_Id, b.Descripcion as Marca_Descripcion, a.Descripcion 
from	Modelo a
		join Marca b on (b.Marca_Id = a.Marca_Id)
where	a.Marca_Id = @Marca_Id
order by a.Descripcion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Modelo_Insert @Modelo_Id uniqueidentifier, @Marca_Id uniqueidentifier, @Descripcion varchar(255) , @DVH char(64) as

if not exists (select * from Modelo where Marca_Id = @Marca_Id and Descripcion = @Descripcion)
begin
	insert into Modelo(Modelo_Id,Marca_Id,Descripcion,DVH) values (@Modelo_Id,@Marca_Id,@Descripcion,@DVH);
end

select 'success' as SQLResultType, 'Operation completed.' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc PagoCompra_Add	@PagoCompra_Id uniqueidentifier, @Compra_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Cajero_Id uniqueidentifier,
											@Monto float, @Banco varchar(100), @CbuAlias varchar(50), @NumeroOperacion varchar(50),
											@NumeroCheque varchar(30), @FechaPago datetime, @FechaCreacion datetime, @DVH char(64) as

-- La FK contra AcquisitionPaymentTerms impide pagar con un medio que no se pacto en CUF10.
insert into PagoCompra(PagoCompra_Id, Compra_Id, TipoPago_Id, Cajero_Id, Monto, Banco, CbuAlias, NumeroOperacion, NumeroCheque, FechaPago, FechaCreacion, DVH)
values (@PagoCompra_Id, @Compra_Id, @TipoPago_Id, @Cajero_Id, @Monto, @Banco, @CbuAlias, @NumeroOperacion, @NumeroCheque, @FechaPago, @FechaCreacion, @DVH)

select 'success' as SQLResultType,'Payment added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc PagoCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- Pagos registrados por el cajero (CUF11 paso 5), con el medio y quien pago.
select	a.PagoCompra_Id,
        a.Compra_Id,
        a.TipoPago_Id,
        b.Descripcion as TipoPago_Descripcion,
        a.Cajero_Id,
        (u.Nombre + ' ' + u.Apellido) as Cajero,
        a.Monto,
        a.Banco,
        a.CbuAlias,
        a.NumeroOperacion,
        a.NumeroCheque,
        a.FechaPago,
        a.FechaCreacion
from	PagoCompra a
        join TipoPago b on (b.TipoPago_Id = a.TipoPago_Id)
        join Usuario u on (u.Usuario_Id = a.Cajero_Id)
where	a.Compra_Id = @Compra_Id
order by a.FechaCreacion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Pago_Add	@Pago_Id uniqueidentifier, @Factura_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Monto float, @Detalle varchar(255) , @DVH char(64) as

insert into Pago(Pago_Id, Factura_Id, TipoPago_Id, Monto, Detalle,DVH)
values (@Pago_Id, @Factura_Id, @TipoPago_Id, @Monto, @Detalle,@DVH)

select 'success' as SQLResultType,'Payment added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Pago_Delete	@Pago_Id uniqueidentifier as

delete Pago
where Pago_Id = @Pago_Id

select 'success' as SQLResultType,'Payment deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Pago_GetAllByInvoice @Factura_Id uniqueidentifier as

select	a.Pago_Id,
        a.Factura_Id,
        a.TipoPago_Id, b.Descripcion as TipoPago_Descripcion,
        a.Monto,
        a.Detalle
from	Pago a
		join TipoPago b on (b.TipoPago_Id = a.TipoPago_Id)
where	a.Factura_Id = @Factura_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.Permiso_DeleteCompleteFamily
    @Permiso_Id uniqueidentifier,
    @BaseRoleId uniqueidentifier,
    @UserDigests dbo.UsuarioDvhUpdates READONLY
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        -- .NET calcula un DVH por usuario con el nuevo Role_Id antes de llamar al SP.
        IF EXISTS (SELECT Usuario_Id FROM dbo.Usuario WHERE Rol_Id=@Permiso_Id EXCEPT SELECT Usuario_Id FROM @UserDigests)
            OR EXISTS (SELECT Usuario_Id FROM @UserDigests EXCEPT SELECT Usuario_Id FROM dbo.Usuario WHERE Rol_Id=@Permiso_Id)
            THROW 51001, 'Los usuarios afectados cambiaron. Reintente la eliminacion de la familia.', 1;
        IF EXISTS (SELECT 1 FROM @UserDigests) AND
            (@BaseRoleId IS NULL OR @BaseRoleId=@Permiso_Id OR NOT EXISTS
                (SELECT 1 FROM dbo.Permiso WHERE Permiso_Id=@BaseRoleId AND Nombre='Base User' AND Tipo IS NULL))
            THROW 51002, 'No existe una familia Base User valida para reasignar los usuarios.', 1;
        DELETE FROM dbo.Permiso_Permiso WHERE Padre_Id=@Permiso_Id OR Hijo_Id=@Permiso_Id;
        UPDATE u SET Rol_Id=@BaseRoleId, DVH=d.DVH
            FROM dbo.Usuario u JOIN @UserDigests d ON d.Usuario_Id=u.Usuario_Id;
        DELETE FROM dbo.Permiso WHERE Permiso_Id=@Permiso_Id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_DeleteFamily @Permiso_Id uniqueidentifier as

delete from Permiso_Permiso where Padre_Id=@Permiso_Id;
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.Permiso_DeletePatent @Permiso_Id uniqueidentifier
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        DELETE FROM dbo.Permiso_Permiso WHERE Padre_Id=@Permiso_Id OR Hijo_Id=@Permiso_Id;
        DELETE FROM dbo.Permiso WHERE Permiso_Id=@Permiso_Id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetAll @Family uniqueidentifier as

with recursive as	(
						select	sp2.Padre_Id, sp2.Hijo_Id  
						from	Permiso_Permiso SP2
						where	(sp2.Padre_Id = @Family or @Family is null)
						UNION ALL 
						select sp.Padre_Id, sp.Hijo_Id 
						from	Permiso_Permiso sp 
								inner join recursive r on (r.Hijo_Id=sp.Padre_Id)
					)
select	r.Padre_Id,
		r.Hijo_Id,
		p.Permiso_Id,
		p.Nombre, 
		p.Tipo
from	recursive r 
		inner join Permiso p on (r.Hijo_Id = p.Permiso_Id)
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetAllByUser @User_Id uniqueidentifier as

select p.* from Users_Permissions up inner join Permiso p on up.Permission_Id=p.Permiso_Id where up.User_Id=@User_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetFamilies as

select	* 
from	Permiso p 
where	p.Tipo is  null
order by p.Nombre asc;
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetPatents  as

select	* 
from	Permiso p 
where	p.Tipo is not null
order by p.Nombre asc;
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_InsertComponent @Permiso_Id uniqueidentifier, @Nombre varchar(50), @Tipo varchar(50) , @DVH char(64) as

insert into Permiso(Permiso_Id,Nombre,Tipo,DVH) values (@Permiso_Id,@Nombre,@Tipo,@DVH);
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_Insert_Permission_Permission @Padre_Id uniqueidentifier, @Hijo_Id uniqueidentifier , @DVH char(64) as

insert into Permiso_Permiso(Padre_Id,Hijo_Id,DVH) values (@Padre_Id,@Hijo_Id,@DVH);
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Persona_Add	@Persona_Id uniqueidentifier, @DNI int, @Nombre varchar(max), @Apellido varchar(max), @Direccion varchar(max),
									@TelefonoPersonal varchar(max), @TelefonoCasa varchar(max), @Email varchar(max), @FechaNacimiento datetime , @DVH char(64) as

insert into Persona(Persona_Id,DNI,Nombre,Apellido,Direccion,TelefonoPersonal,TelefonoCasa,Email,FechaNacimiento,DVH)
values (@Persona_Id,@DNI,@Nombre,@Apellido,@Direccion,@TelefonoPersonal,@TelefonoCasa,@Email,@FechaNacimiento,@DVH)

select 'success' as SQLResultType,'Person added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Persona_GetByDni @DNI int as

select	u.Persona_Id,u.DNI,u.Nombre,u.Apellido,u.Direccion,u.TelefonoPersonal,u.TelefonoCasa,u.Email,u.FechaNacimiento
from	Persona  u
where	u.DNI = @DNI
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Persona_GetById @Persona_Id uniqueidentifier as

select	Persona_Id,DNI,Nombre,Apellido,Direccion,TelefonoPersonal,TelefonoCasa,Email,FechaNacimiento
from	Persona  
where	Persona_Id = @Persona_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc PuntajeInspeccion_GetByInspection @Inspeccion_Id uniqueidentifier as

select	a.Inspeccion_Id,
        a.ItemInspeccion_Id,
        b.Descripcion,
        b.Orden,
        a.Puntaje,
        a.Observaciones
from	PuntajeInspeccion a
        join ItemInspeccion b on (b.ItemInspeccion_Id = a.ItemInspeccion_Id)
where	a.Inspeccion_Id = @Inspeccion_Id
order by b.Orden asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Reserva_Add	@Reserva_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Precio float, @Fecha datetime, @FechaVencimiento datetime , @DVH char(64) as

insert into Reserva(Reserva_Id, Vehiculo_Id, Persona_Id, Precio, Fecha,FechaVencimiento,DVH)
values (@Reserva_Id, @Vehiculo_Id, @Persona_Id, @Precio, @Fecha,@FechaVencimiento,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Reserva_GetAll @Reserva_Id uniqueidentifier as

select	a.Reserva_Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha,
        a.FechaVencimiento
from	Reserva a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
        join Persona g on (g.Persona_Id = a.Persona_Id)
where	a.Reserva_Id = @Reserva_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Reserva_GetAllActiveByPerson @Persona_Id uniqueidentifier as

select	a.Reserva_Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha,
        a.FechaVencimiento
from	Reserva a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
        join Persona g on (g.Persona_Id = a.Persona_Id)
        left join Factura i on (i.Reserva_Id = a.Reserva_Id and i.EstadoPago = 1)
where	a.Persona_Id = @Persona_Id
        and (cast(getdate() as date) >= cast(a.Fecha as date)) and (cast(getdate() as date) <= cast(a.FechaVencimiento as date))
        and i.Factura_Id is null
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Reserva_GetById @Reserva_Id uniqueidentifier as

select	a.Reserva_Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha,
        a.FechaVencimiento
from	Reserva a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
        join Persona g on (g.Persona_Id = a.Persona_Id)
where	a.Reserva_Id = @Reserva_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tasacion_Add	@Tasacion_Id uniqueidentifier, @Compra_Id uniqueidentifier, @Inspeccion_Id uniqueidentifier, @Tasador_Id uniqueidentifier,
									@Valor float, @Justificacion varchar(max), @Fecha datetime, @Vigencia datetime, @DVH char(64) as

insert into Tasacion(Tasacion_Id, Compra_Id, Inspeccion_Id, Tasador_Id, Valor, Justificacion, Fecha, Vigencia, DVH)
values (@Tasacion_Id, @Compra_Id, @Inspeccion_Id, @Tasador_Id, @Valor, @Justificacion, @Fecha, @Vigencia, @DVH)

select 'success' as SQLResultType,'Appraisal added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tasacion_GetLastByAcquisition @Compra_Id uniqueidentifier as

-- La tasacion vigente de una adquisicion es la ultima que se registro.
select	top 1
        a.Tasacion_Id,
        a.Compra_Id,
        a.Inspeccion_Id,
        a.Tasador_Id,
        a.Valor,
        a.Justificacion,
        a.Fecha,
        a.Vigencia,
        (b.Nombre + ' ' + b.Apellido) as Tasador
from	Tasacion a
        join Usuario b on (b.Usuario_Id = a.Tasador_Id)
where	a.Compra_Id = @Compra_Id
order by a.Fecha desc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc TipoDocumentoCompra_GetByStage @Etapa int as

-- Documentos obligatorios de cada etapa de CUF10: 1 Documentar, 2 Transferir.
select	a.TipoDocumentoCompra_Id,
        a.Descripcion,
        a.Etapa,
        a.DiasVigencia,
        a.Orden
from	TipoDocumentoCompra a
where	a.Etapa = @Etapa
order by a.Orden asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc TipoPago_GetAll as

select TipoPago_Id, Descripcion 
from TipoPago 
order by Descripcion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_Add	@Tramite_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Factura_Id uniqueidentifier, @CodigoPrecarga08 varchar(50),
									@FechaTransferencia datetime, @Observaciones varchar(max), @Finalizado bit , @DVH char(64) as

insert into Tramite(Tramite_Id, Vehiculo_Id, Persona_Id, Factura_Id, CodigoPrecarga08, FechaTransferencia, Observaciones, Finalizado,DVH)
values (@Tramite_Id, @Vehiculo_Id, @Persona_Id, @Factura_Id, @CodigoPrecarga08, @FechaTransferencia, @Observaciones, @Finalizado,@DVH)

select 'success' as SQLResultType,'Paperwork added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_Delete	@Tramite_Id uniqueidentifier as

delete Tramite
where Tramite_Id = @Tramite_Id

select 'success' as SQLResultType,'Paperwork deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_GetAllActiveByPerson @Persona_Id uniqueidentifier as

select	a.Tramite_Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Factura_Id, 
        a.CodigoPrecarga08, 
        a.FechaTransferencia,
        a.Observaciones, 
        a.Finalizado
from	Tramite a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
where	(a.Persona_Id = @Persona_Id or (@Persona_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
		and a.Finalizado = 0
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_GetAllByPerson @Persona_Id uniqueidentifier as

select	a.Tramite_Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Factura_Id, 
        a.CodigoPrecarga08, 
        a.FechaTransferencia,
        a.Observaciones, 
        a.Finalizado
from	Tramite a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
where	(a.Persona_Id = @Persona_Id or (@Persona_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_GetById @Tramite_Id uniqueidentifier as

select	a.Tramite_Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Factura_Id, 
        a.CodigoPrecarga08, 
        a.FechaTransferencia,
        a.Observaciones, 
        a.Finalizado
from	Tramite a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
where	a.Tramite_Id = @Tramite_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_Update	@Tramite_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Factura_Id uniqueidentifier, @CodigoPrecarga08 varchar(50),
									@FechaTransferencia datetime, @Observaciones varchar(max), @Finalizado bit , @DVH char(64) as

update Tramite
set DVH = @DVH,
	Vehiculo_Id = @Vehiculo_Id,
	Persona_Id = @Persona_Id,
	Factura_Id = @Factura_Id,
	CodigoPrecarga08 = @CodigoPrecarga08,
	FechaTransferencia = @FechaTransferencia,
	Observaciones = @Observaciones,
	Finalizado = @Finalizado
where Tramite_Id = @Tramite_Id

select 'success' as SQLResultType,'Paperwork updated succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_Add	@Usuario_Id uniqueidentifier, @DNI int, @NombreUsuario varchar(50), @Clave varchar(max), @Nombre nvarchar(max), @Apellido nvarchar(max),
											@Rol_Id uniqueidentifier, @Bloqueado bit, @Activo bit, @IntentosDisponibles int, @CodigoIdioma varchar(2) , @DVH char(64) as

insert into Usuario(Usuario_Id,DNI,NombreUsuario,Clave,Nombre,Apellido,Rol_Id,Bloqueado,Activo,IntentosDisponibles,CodigoIdioma,DVH)
values (@Usuario_Id,@DNI,@NombreUsuario,@Clave,@Nombre,@Apellido,@Rol_Id,@Bloqueado,@Activo,@IntentosDisponibles,@CodigoIdioma,@DVH)

select 'success' as SQLResultType,'User added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_Delete	@Usuario_Id uniqueidentifier as

delete Usuario
where Usuario_Id = @Usuario_Id

select 'success' as SQLResultType,'User deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_DeleteAllPermissions @Usuario_Id varchar(50) as

delete from Users_Permissions where User_id=@Usuario_Id;
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetAll as

select	u.Usuario_Id,u.NombreUsuario
from	Usuario u 
order by u.NombreUsuario asc	
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetAllByState @Activo bit as

select	u.Usuario_Id, u.DNI, u.NombreUsuario, Clave='' , u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	(@Activo = 1 and u.Activo = @Activo) or @Activo = 0 --Si active es 0 muestro todos los usuarios. 
order by u.NombreUsuario asc	
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetUserByDni @DNI int as

select	u.Usuario_Id, u.DNI, u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.DNI = @DNI
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetUserById @Usuario_Id uniqueidentifier as

select	u.Usuario_Id, u.DNI, u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.Usuario_Id = @Usuario_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetUserByUsername @NombreUsuario varchar(50) as

select	u.Usuario_Id, u.DNI,u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.NombreUsuario = @NombreUsuario
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_InsertPermission @User_Id UniqueIdentifier, @Permission_Id UniqueIdentifier as

insert into Users_Permissions(User_id,Permission_id) values (@User_Id,@Permission_Id)
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_Update	@Usuario_Id uniqueidentifier, @DNI int, @NombreUsuario varchar(50), @Clave varchar(max), @Nombre nvarchar(max), @Apellido nvarchar(max),
											@Rol_Id uniqueidentifier, @Bloqueado bit, @Activo bit, @IntentosDisponibles int, @CodigoIdioma varchar(2) , @DVH char(64) as

update Usuario
set DVH = @DVH,
	DNI = @DNI,
	NombreUsuario = @NombreUsuario,
	Clave = @Clave,
	Nombre = @Nombre,
	Apellido = @Apellido,
	Rol_Id = @Rol_Id,
	Bloqueado = @Bloqueado,
	Activo = @Activo,
	IntentosDisponibles = @IntentosDisponibles,
	CodigoIdioma = @CodigoIdioma
where Usuario_Id = @Usuario_Id

select 'success' as SQLResultType,'User updated succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc VehiculoProspecto_Add	@VehiculoProspecto_Id uniqueidentifier, @Patente varchar(10), @NumeroChasis varchar(17), @NumeroMotor varchar(20), @Marca_Id uniqueidentifier,
										@Modelo_Id uniqueidentifier, @Version_Id uniqueidentifier, @Color_Id uniqueidentifier, @Anio int, @Puertas int,
										@Kilometraje int, @DVH char(64) as

insert into VehiculoProspecto(VehiculoProspecto_Id, Patente, NumeroChasis, NumeroMotor, Marca_Id, Modelo_Id, Version_Id, Color_Id, Anio, Puertas, Kilometraje, DVH)
values (@VehiculoProspecto_Id, @Patente, @NumeroChasis, @NumeroMotor, @Marca_Id, @Modelo_Id, @Version_Id, @Color_Id, @Anio, @Puertas, @Kilometraje, @DVH)

select 'success' as SQLResultType,'Prospect vehicle added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_Add	@Vehiculo_Id uniqueidentifier, @Patente varchar(255), @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier, @Version_Id uniqueidentifier, @Color_Id uniqueidentifier,
									@Precio float, @Opcionales varchar(255), @Observaciones varchar(max), @Puertas int, @Anio int, @Kilometraje int, @LinkImagen nvarchar(max) , @DVH char(64) as

insert into Vehiculo(Vehiculo_Id,Patente,Marca_Id,Modelo_Id,Version_Id,Color_Id,Precio,Opcionales,Observaciones,Puertas,Anio,Kilometraje,LinkImagen,DVH)
values (@Vehiculo_Id,@Patente,@Marca_Id,@Modelo_Id,@Version_Id,@Color_Id,@Precio,@Opcionales,@Observaciones,@Puertas,@Anio,@Kilometraje,@LinkImagen,@DVH)

select 'success' as SQLResultType,'Vehicle added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_Delete	@Vehiculo_Id uniqueidentifier as

delete Vehiculo
where Vehiculo_Id = @Vehiculo_Id

select 'success' as SQLResultType,'Vehicle deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_GetActiveVehiclesByFilters @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier , @Version_Id uniqueidentifier , @Color_Id uniqueidentifier , @Price_From float, 
                                                         @Price_To float, @Year_From int, @Year_To int, @Doors_From int, @Doors_To int, @Kilometers_From int, @Kilometers_To int as

select	a.Vehiculo_Id,
        a.Patente,
        a.Marca_Id, b.Descripcion as Marca_Descripcion,
        a.Modelo_Id, c.Descripcion as Modelo_Descripcion, 
        a.Version_Id, d.Descripcion as Version_Descripcion, 
        a.Color_Id, e.Descripcion as Color_Descripcion, 
        a.Precio,
        a.Opcionales, 
        a.Observaciones,
        a.Puertas, 
        a.Anio, 
        a.Kilometraje, 
        a.LinkImagen
from	Vehiculo a
		join Marca b on (b.Marca_Id = a.Marca_Id)
		join Modelo c on (c.Modelo_Id = a.Modelo_Id)
		join Version d on (d.Version_Id = a.Version_Id)
		join Color e on (e.Color_Id = a.Color_Id)
where	(a.Marca_Id = @Marca_Id or (@Marca_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Modelo_Id = @Modelo_Id or (@Modelo_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Version_Id = @Version_Id or (@Version_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Color_Id = @Color_Id or (@Color_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Precio >= @Price_From or (@Price_From = 0 or @Price_From is null))
        and (a.Precio <= @Price_To or (@Price_To = 0 or @Price_To is null))
        and (a.Kilometraje >= @Kilometers_From or (@Kilometers_From = 0 or @Kilometers_From is null))
        and (a.Kilometraje <= @Kilometers_To or (@Kilometers_To = 0 or @Kilometers_To is null))
        and (a.Puertas >= @Doors_From or (@Doors_From = 0 or @Doors_From is null))
        and (a.Puertas <= @Doors_To or (@Doors_To = 0 or @Doors_To is null))
        and (a.Anio >= @Year_From or (@Year_From = 0 or @Year_From is null))
        and (a.Anio <= @Year_To or (@Year_To = 0 or @Year_To is null))
        and not exists 
        (
            select * 
            from Reserva r 
            left join Factura i on (i.Reserva_Id = r.Reserva_Id and i.EstadoPago = 1)
            where r.Vehiculo_Id = a.Vehiculo_Id 
            and 
            (
                i.Reserva_Id is not null
                or 
                (cast(getdate() as date) >= cast(r.Fecha as date)) and (cast(getdate() as date) <= cast(r.FechaVencimiento as date))
            )
        )
order by b.Descripcion, c.Descripcion, d.Descripcion, e.Descripcion, a.Anio, a.Kilometraje
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_GetAll as

select	a.Vehiculo_Id,
        a.Patente,
        a.Marca_Id, b.Descripcion as Marca_Descripcion,
        a.Modelo_Id, c.Descripcion as Modelo_Descripcion, 
        a.Version_Id, d.Descripcion as Version_Descripcion, 
        a.Color_Id, e.Descripcion as Color_Descripcion, 
        a.Precio,
        a.Opcionales, 
        a.Observaciones,
        a.Puertas, 
        a.Anio, 
        a.Kilometraje, 
        a.LinkImagen
from	Vehiculo a
		join Marca b on (b.Marca_Id = a.Marca_Id)
		join Modelo c on (c.Modelo_Id = a.Modelo_Id)
		join Version d on (d.Version_Id = a.Version_Id)
		join Color e on (e.Color_Id = a.Color_Id)
order by b.Descripcion, c.Descripcion, d.Descripcion, e.Descripcion, a.Anio, a.Kilometraje
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_GetById @Vehiculo_Id uniqueidentifier as

select	a.Vehiculo_Id,
        a.Patente,
        a.Marca_Id, b.Descripcion as Marca_Descripcion,
        a.Modelo_Id, c.Descripcion as Modelo_Descripcion, 
        a.Version_Id, d.Descripcion as Version_Descripcion, 
        a.Color_Id, e.Descripcion as Color_Descripcion, 
        a.Precio,
        a.Opcionales, 
        a.Observaciones,
        a.Puertas, 
        a.Anio, 
        a.Kilometraje, 
        a.LinkImagen
from	Vehiculo a
		join Marca b on (b.Marca_Id = a.Marca_Id)
		join Modelo c on (c.Modelo_Id = a.Modelo_Id)
		join Version d on (d.Version_Id = a.Version_Id)
		join Color e on (e.Color_Id = a.Color_Id)
where	a.Vehiculo_Id = @Vehiculo_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_Update	@Vehiculo_Id uniqueidentifier, @Patente varchar(255), @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier, @Version_Id uniqueidentifier, @Color_Id uniqueidentifier,
									@Precio float, @Opcionales varchar(255), @Observaciones varchar(max), @Puertas int, @Anio int, @Kilometraje int, @LinkImagen nvarchar(max) , @DVH char(64) as

update Vehiculo
set DVH = @DVH,
	Patente = @Patente,
	Marca_Id = @Marca_Id,
	Modelo_Id = @Modelo_Id,
	Version_Id = @Version_Id,
	Color_Id = @Color_Id,
	Precio = @Precio,
	Opcionales = @Opcionales,
	Observaciones = @Observaciones,
	Puertas = @Puertas,
	Anio = @Anio,
	Kilometraje = @Kilometraje,
	LinkImagen = @LinkImagen
where Vehiculo_Id = @Vehiculo_Id

select 'success' as SQLResultType,'Vehicle updated succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Version_Delete	@Version_Id uniqueidentifier as

delete Version
where Version_Id = @Version_Id

select 'success' as SQLResultType,'Version deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Version_GetAllByMakeModel @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier as

select	a.Version_Id, a.Marca_Id, b.Descripcion as Marca_Descripcion, a.Modelo_Id, c.Descripcion as Modelo_Descripcion, a.Descripcion 
from	Version a
		join Marca b on (b.Marca_Id = a.Marca_Id)
		join Modelo c on (c.Modelo_Id = a.Modelo_Id)
where	a.Marca_Id = @Marca_Id
		and a.Modelo_Id = @Modelo_Id
order by a.Descripcion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Version_Insert @Version_Id uniqueidentifier, @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier, @Descripcion varchar(255) , @DVH char(64) as

if not exists (select * from Version where Marca_Id = @Marca_Id and Modelo_Id = @Modelo_Id and Descripcion = @Descripcion)
begin
	insert into Version(Version_Id,Marca_Id,Modelo_Id,Descripcion,DVH) values (@Version_Id,@Marca_Id,@Modelo_Id,@Descripcion,@DVH);
end

select 'success' as SQLResultType, 'Operation completed.' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

COMMIT TRANSACTION
GO
