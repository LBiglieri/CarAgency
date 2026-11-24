CREATE TYPE [dbo].[PuntajeInspeccionRows] AS TABLE(
	[ItemInspeccion_Id] [uniqueidentifier] NOT NULL,
	[Puntaje] [int] NOT NULL,
	[Observaciones] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ItemInspeccion_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF),
	CHECK ((len([DVH])=(64) AND NOT [DVH] like '%[^0-9A-F]%'))
)
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
CREATE SEQUENCE [dbo].[CompraNumbers] AS int START WITH 1 INCREMENT BY 1
GO
CREATE SEQUENCE [dbo].[ComprobanteCompraNumbers] AS int START WITH 1 INCREMENT BY 1
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ArchivoDocumentoCompra](
	[DocumentoCompra_Id] [uniqueidentifier] NOT NULL,
	[NombreArchivo] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Contenido] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_ArchivoDocumentoCompra] PRIMARY KEY CLUSTERED 
(
	[DocumentoCompra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ArchivoTramite](
	[ArchivoTramite_Id] [uniqueidentifier] NOT NULL,
	[Tramite_Id] [uniqueidentifier] NOT NULL,
	[NombreArchivo] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Contenido] [varbinary](max) NOT NULL,
	[FechaCarga] [datetime] NOT NULL,
 CONSTRAINT [PK_ArchivoTramite] PRIMARY KEY CLUSTERED 
(
	[ArchivoTramite_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ArchivoTramite] ADD  CONSTRAINT [DF_ArchivoTramite_ArchivoTramite_Id]  DEFAULT (newid()) FOR [ArchivoTramite_Id]
GO
ALTER TABLE [dbo].[ArchivoTramite] ADD  CONSTRAINT [DF_ArchivoTramite_FechaCarga]  DEFAULT (getdate()) FOR [FechaCarga]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BitacoraCambios](
	[BitacoraCambios_Id] [uniqueidentifier] NOT NULL,
	[Vehiculo_Id] [uniqueidentifier] NOT NULL,
	[Patente] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Operacion] [int] NOT NULL,
	[Campo] [varchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValorAnterior] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValorNuevo] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Usuario_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_BitacoraCambios] PRIMARY KEY CLUSTERED 
(
	[BitacoraCambios_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BitacoraCambios_Fecha] ON [dbo].[BitacoraCambios]
(
	[Fecha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BitacoraCambios_Vehiculo_Fecha] ON [dbo].[BitacoraCambios]
(
	[Vehiculo_Id] ASC,
	[Fecha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BitacoraCambios] ADD  CONSTRAINT [DF_BitacoraCambios_Id]  DEFAULT (newid()) FOR [BitacoraCambios_Id]
GO
ALTER TABLE [dbo].[BitacoraCambios] ADD  CONSTRAINT [DF_BitacoraCambios_Fecha]  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[BitacoraCambios]  WITH CHECK ADD  CONSTRAINT [CK_BitacoraCambios_Campo] CHECK  (([Operacion]=(2) AND [Campo] IS NOT NULL OR [Operacion]<>(2) AND [Campo] IS NULL))
GO
ALTER TABLE [dbo].[BitacoraCambios] CHECK CONSTRAINT [CK_BitacoraCambios_Campo]
GO
ALTER TABLE [dbo].[BitacoraCambios]  WITH CHECK ADD  CONSTRAINT [CK_BitacoraCambios_Operacion] CHECK  (([Operacion]=(3) OR [Operacion]=(2) OR [Operacion]=(1)))
GO
ALTER TABLE [dbo].[BitacoraCambios] CHECK CONSTRAINT [CK_BitacoraCambios_Operacion]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BitacoraEventos](
	[BitacoraEventos_Id] [uniqueidentifier] NOT NULL,
	[Usuario_Id] [uniqueidentifier] NULL,
	[LoginIntentado] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fecha] [datetime] NOT NULL,
	[Modulo] [varchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TipoEvento] [varchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Criticidad] [int] NOT NULL,
	[Objetivo_Id] [uniqueidentifier] NULL,
	[DVH] [char](64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_BitacoraEventos] PRIMARY KEY CLUSTERED 
(
	[BitacoraEventos_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BitacoraEventos_Fecha] ON [dbo].[BitacoraEventos]
(
	[Fecha] DESC,
	[BitacoraEventos_Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BitacoraEventos_Usuario] ON [dbo].[BitacoraEventos]
(
	[Usuario_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BitacoraEventos]  WITH CHECK ADD  CONSTRAINT [CK_BitacoraEventos_Criticidad] CHECK  (([Criticidad]>=(1) AND [Criticidad]<=(5)))
GO
ALTER TABLE [dbo].[BitacoraEventos] CHECK CONSTRAINT [CK_BitacoraEventos_Criticidad]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Color](
	[Color_Id] [uniqueidentifier] NOT NULL,
	[Descripcion] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[Color_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Color]  WITH CHECK ADD  CONSTRAINT [CK_Color_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Color] CHECK CONSTRAINT [CK_Color_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Compra](
	[Compra_Id] [uniqueidentifier] NOT NULL,
	[Numero] [int] NOT NULL,
	[Persona_Id] [uniqueidentifier] NOT NULL,
	[VehiculoProspecto_Id] [uniqueidentifier] NOT NULL,
	[Vendedor_Id] [uniqueidentifier] NOT NULL,
	[Estado] [int] NOT NULL,
	[MotivoRechazo] [int] NULL,
	[Fecha] [datetime] NOT NULL,
	[TasacionAceptada_Id] [uniqueidentifier] NULL,
	[PrecioAcordado] [float] NULL,
	[CuilCuitTitular] [varchar](13) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FechaFirmaContrato] [datetime] NULL,
	[CodigoPrecarga08] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FechaCitaTransferencia] [datetime] NULL,
	[Vehiculo_Id] [uniqueidentifier] NULL,
	[FechaCierre] [datetime] NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Compra] PRIMARY KEY CLUSTERED 
(
	[Compra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Compra_Numero] UNIQUE NONCLUSTERED 
(
	[Numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Compra_VehiculoProspecto] UNIQUE NONCLUSTERED 
(
	[VehiculoProspecto_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Compra_Estado] ON [dbo].[Compra]
(
	[Estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Compra_Persona] ON [dbo].[Compra]
(
	[Persona_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Compra_Vehiculo] ON [dbo].[Compra]
(
	[Vehiculo_Id] ASC
)
WHERE ([Vehiculo_Id] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [CK_Compra_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [CK_Compra_DVH]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [CK_Compra_Estado] CHECK  (([Estado]>=(1) AND [Estado]<=(10)))
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [CK_Compra_Estado]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [CK_Compra_MotivoRechazo] CHECK  (([MotivoRechazo] IS NULL OR [MotivoRechazo]>=(1) AND [MotivoRechazo]<=(3)))
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [CK_Compra_MotivoRechazo]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [CK_Compra_PrecioAcordado] CHECK  (([PrecioAcordado] IS NULL OR [PrecioAcordado]>(0)))
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [CK_Compra_PrecioAcordado]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ComprobanteCompra](
	[ComprobanteCompra_Id] [uniqueidentifier] NOT NULL,
	[Numero] [int] NOT NULL,
	[Compra_Id] [uniqueidentifier] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[NombrePersona] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CuilCuit] [varchar](13) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Detalle] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MontoNeto] [float] NOT NULL,
	[MontoImpuesto] [float] NOT NULL,
	[MontoTotal] [float] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_ComprobanteCompra] PRIMARY KEY CLUSTERED 
(
	[ComprobanteCompra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_ComprobanteCompra_Compra] UNIQUE NONCLUSTERED 
(
	[Compra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_ComprobanteCompra_Numero] UNIQUE NONCLUSTERED 
(
	[Numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ComprobanteCompra]  WITH CHECK ADD  CONSTRAINT [CK_ComprobanteCompra_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[ComprobanteCompra] CHECK CONSTRAINT [CK_ComprobanteCompra_DVH]
GO
ALTER TABLE [dbo].[ComprobanteCompra]  WITH CHECK ADD  CONSTRAINT [CK_ComprobanteCompra_Montos] CHECK  (([MontoNeto]>(0) AND [MontoImpuesto]>=(0) AND [MontoTotal]>(0)))
GO
ALTER TABLE [dbo].[ComprobanteCompra] CHECK CONSTRAINT [CK_ComprobanteCompra_Montos]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cotizacion](
	[Cotizacion_Id] [uniqueidentifier] NOT NULL,
	[Vehiculo_Id] [uniqueidentifier] NULL,
	[Persona_Id] [uniqueidentifier] NULL,
	[Precio] [float] NULL,
	[Fecha] [datetime] NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Cotizacion] PRIMARY KEY CLUSTERED 
(
	[Cotizacion_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IXFK_Cotizacion_Vehiculo] ON [dbo].[Cotizacion]
(
	[Vehiculo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cotizacion]  WITH CHECK ADD  CONSTRAINT [CK_Cotizacion_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Cotizacion] CHECK CONSTRAINT [CK_Cotizacion_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DocumentoCompra](
	[DocumentoCompra_Id] [uniqueidentifier] NOT NULL,
	[Compra_Id] [uniqueidentifier] NOT NULL,
	[TipoDocumentoCompra_Id] [uniqueidentifier] NOT NULL,
	[Estado] [int] NOT NULL,
	[FechaEmision] [datetime] NOT NULL,
	[Observacion] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UsuarioCarga_Id] [uniqueidentifier] NOT NULL,
	[FechaCarga] [datetime] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_DocumentoCompra] PRIMARY KEY CLUSTERED 
(
	[DocumentoCompra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_DocumentoCompra_TipoDocumentoCompra] UNIQUE NONCLUSTERED 
(
	[Compra_Id] ASC,
	[TipoDocumentoCompra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[DocumentoCompra]  WITH CHECK ADD  CONSTRAINT [CK_DocumentoCompra_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[DocumentoCompra] CHECK CONSTRAINT [CK_DocumentoCompra_DVH]
GO
ALTER TABLE [dbo].[DocumentoCompra]  WITH CHECK ADD  CONSTRAINT [CK_DocumentoCompra_Estado] CHECK  (([Estado]>=(1) AND [Estado]<=(2)))
GO
ALTER TABLE [dbo].[DocumentoCompra] CHECK CONSTRAINT [CK_DocumentoCompra_Estado]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DV](
	[NombreEsquema] [sysname] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NombreTabla] [sysname] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DVV] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
	[VersionAlgoritmo] [tinyint] NOT NULL,
 CONSTRAINT [PK_DV] PRIMARY KEY CLUSTERED 
(
	[NombreEsquema] ASC,
	[NombreTabla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DV]  WITH CHECK ADD  CONSTRAINT [CK_DV_DVV] CHECK  ((len([DVV])=(64) AND NOT [DVV] like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[DV] CHECK CONSTRAINT [CK_DV_DVV]
GO
ALTER TABLE [dbo].[DV]  WITH CHECK ADD  CONSTRAINT [CK_DV_VersionAlgoritmo] CHECK  (([VersionAlgoritmo]=(1)))
GO
ALTER TABLE [dbo].[DV] CHECK CONSTRAINT [CK_DV_VersionAlgoritmo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Factura](
	[Factura_Id] [uniqueidentifier] NOT NULL,
	[Vehiculo_Id] [uniqueidentifier] NOT NULL,
	[Persona_Id] [uniqueidentifier] NOT NULL,
	[Reserva_Id] [uniqueidentifier] NOT NULL,
	[Detalle] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CuilCuit] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RazonSocial] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Valor] [float] NOT NULL,
	[EstadoPago] [bit] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[Factura_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FormaPagoCompra](
	[Compra_Id] [uniqueidentifier] NOT NULL,
	[TipoPago_Id] [uniqueidentifier] NOT NULL,
	[Monto] [float] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_FormaPagoCompra] PRIMARY KEY CLUSTERED 
(
	[Compra_Id] ASC,
	[TipoPago_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FormaPagoCompra]  WITH CHECK ADD  CONSTRAINT [CK_FormaPagoCompra_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[FormaPagoCompra] CHECK CONSTRAINT [CK_FormaPagoCompra_DVH]
GO
ALTER TABLE [dbo].[FormaPagoCompra]  WITH CHECK ADD  CONSTRAINT [CK_FormaPagoCompra_Monto] CHECK  (([Monto]>(0)))
GO
ALTER TABLE [dbo].[FormaPagoCompra] CHECK CONSTRAINT [CK_FormaPagoCompra_Monto]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FotografiaInspeccion](
	[FotografiaInspeccion_Id] [uniqueidentifier] NOT NULL,
	[Inspeccion_Id] [uniqueidentifier] NOT NULL,
	[NombreArchivo] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Contenido] [varbinary](max) NOT NULL,
	[FechaCarga] [datetime] NOT NULL,
 CONSTRAINT [PK_FotografiaInspeccion] PRIMARY KEY CLUSTERED 
(
	[FotografiaInspeccion_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FotografiaInspeccion_Inspeccion] ON [dbo].[FotografiaInspeccion]
(
	[Inspeccion_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FotografiaInspeccion] ADD  CONSTRAINT [DF_FotografiaInspeccion_FotografiaInspeccion_Id]  DEFAULT (newid()) FOR [FotografiaInspeccion_Id]
GO
ALTER TABLE [dbo].[FotografiaInspeccion] ADD  CONSTRAINT [DF_FotografiaInspeccion_FechaCarga]  DEFAULT (getdate()) FOR [FechaCarga]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Inspeccion](
	[Inspeccion_Id] [uniqueidentifier] NOT NULL,
	[Compra_Id] [uniqueidentifier] NOT NULL,
	[Mecanico_Id] [uniqueidentifier] NOT NULL,
	[FechaProgramada] [datetime] NOT NULL,
	[FechaRealizada] [datetime] NULL,
	[KilometrajeVerificado] [int] NULL,
	[Resultado] [int] NULL,
	[Observaciones] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Inspeccion] PRIMARY KEY CLUSTERED 
(
	[Inspeccion_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Inspeccion_Compra] UNIQUE NONCLUSTERED 
(
	[Compra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Inspeccion_Mecanico_FechaProgramada] ON [dbo].[Inspeccion]
(
	[Mecanico_Id] ASC,
	[FechaProgramada] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Inspeccion]  WITH CHECK ADD  CONSTRAINT [CK_Inspeccion_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Inspeccion] CHECK CONSTRAINT [CK_Inspeccion_DVH]
GO
ALTER TABLE [dbo].[Inspeccion]  WITH CHECK ADD  CONSTRAINT [CK_Inspeccion_KilometrajeVerificado] CHECK  (([KilometrajeVerificado] IS NULL OR [KilometrajeVerificado]>=(0)))
GO
ALTER TABLE [dbo].[Inspeccion] CHECK CONSTRAINT [CK_Inspeccion_KilometrajeVerificado]
GO
ALTER TABLE [dbo].[Inspeccion]  WITH CHECK ADD  CONSTRAINT [CK_Inspeccion_Resultado] CHECK  (([Resultado] IS NULL OR [Resultado]>=(1) AND [Resultado]<=(3)))
GO
ALTER TABLE [dbo].[Inspeccion] CHECK CONSTRAINT [CK_Inspeccion_Resultado]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ItemInspeccion](
	[ItemInspeccion_Id] [uniqueidentifier] NOT NULL,
	[Descripcion] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Orden] [int] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_ItemInspeccion] PRIMARY KEY CLUSTERED 
(
	[ItemInspeccion_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ItemInspeccion]  WITH CHECK ADD  CONSTRAINT [CK_ItemInspeccion_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[ItemInspeccion] CHECK CONSTRAINT [CK_ItemInspeccion_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Marca](
	[Marca_Id] [uniqueidentifier] NOT NULL,
	[Descripcion] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Marca] PRIMARY KEY CLUSTERED 
(
	[Marca_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Marca]  WITH CHECK ADD  CONSTRAINT [CK_Marca_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Marca] CHECK CONSTRAINT [CK_Marca_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Modelo](
	[Modelo_Id] [uniqueidentifier] NOT NULL,
	[Marca_Id] [uniqueidentifier] NOT NULL,
	[Descripcion] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Modelo] PRIMARY KEY CLUSTERED 
(
	[Modelo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IXFK_Modelo_Marca] ON [dbo].[Modelo]
(
	[Marca_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Modelo]  WITH CHECK ADD  CONSTRAINT [CK_Modelo_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Modelo] CHECK CONSTRAINT [CK_Modelo_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pago](
	[Pago_Id] [uniqueidentifier] NOT NULL,
	[Factura_Id] [uniqueidentifier] NOT NULL,
	[TipoPago_Id] [uniqueidentifier] NOT NULL,
	[Monto] [float] NOT NULL,
	[Detalle] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Pago] PRIMARY KEY CLUSTERED 
(
	[Pago_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [CK_Pago_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [CK_Pago_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PagoCompra](
	[PagoCompra_Id] [uniqueidentifier] NOT NULL,
	[Compra_Id] [uniqueidentifier] NOT NULL,
	[TipoPago_Id] [uniqueidentifier] NOT NULL,
	[Cajero_Id] [uniqueidentifier] NOT NULL,
	[Monto] [float] NOT NULL,
	[Banco] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CbuAlias] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NumeroOperacion] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NumeroCheque] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FechaPago] [datetime] NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_PagoCompra] PRIMARY KEY CLUSTERED 
(
	[PagoCompra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_PagoCompra_Compra] ON [dbo].[PagoCompra]
(
	[Compra_Id] ASC,
	[TipoPago_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PagoCompra]  WITH CHECK ADD  CONSTRAINT [CK_PagoCompra_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[PagoCompra] CHECK CONSTRAINT [CK_PagoCompra_DVH]
GO
ALTER TABLE [dbo].[PagoCompra]  WITH CHECK ADD  CONSTRAINT [CK_PagoCompra_Monto] CHECK  (([Monto]>(0)))
GO
ALTER TABLE [dbo].[PagoCompra] CHECK CONSTRAINT [CK_PagoCompra_Monto]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Permiso](
	[Permiso_Id] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Tipo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Permiso] PRIMARY KEY CLUSTERED 
(
	[Permiso_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Permiso]  WITH CHECK ADD  CONSTRAINT [CK_Permiso_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Permiso] CHECK CONSTRAINT [CK_Permiso_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Permiso_Permiso](
	[Padre_Id] [uniqueidentifier] NOT NULL,
	[Hijo_Id] [uniqueidentifier] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Permiso_Permiso] PRIMARY KEY CLUSTERED 
(
	[Padre_Id] ASC,
	[Hijo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Permiso_Permiso]  WITH CHECK ADD  CONSTRAINT [CK_Permiso_Permiso_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Permiso_Permiso] CHECK CONSTRAINT [CK_Permiso_Permiso_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Persona](
	[Persona_Id] [uniqueidentifier] NOT NULL,
	[DNI] [int] NOT NULL,
	[Nombre] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Apellido] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Direccion] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TelefonoPersonal] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TelefonoCasa] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FechaNacimiento] [datetime] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[Persona_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Persona_DNI] UNIQUE NONCLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Persona]  WITH CHECK ADD  CONSTRAINT [CK_Persona_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Persona] CHECK CONSTRAINT [CK_Persona_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PuntajeInspeccion](
	[Inspeccion_Id] [uniqueidentifier] NOT NULL,
	[ItemInspeccion_Id] [uniqueidentifier] NOT NULL,
	[Puntaje] [int] NOT NULL,
	[Observaciones] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_PuntajeInspeccion] PRIMARY KEY CLUSTERED 
(
	[Inspeccion_Id] ASC,
	[ItemInspeccion_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[PuntajeInspeccion]  WITH CHECK ADD  CONSTRAINT [CK_PuntajeInspeccion_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[PuntajeInspeccion] CHECK CONSTRAINT [CK_PuntajeInspeccion_DVH]
GO
ALTER TABLE [dbo].[PuntajeInspeccion]  WITH CHECK ADD  CONSTRAINT [CK_PuntajeInspeccion_Puntaje] CHECK  (([Puntaje]>=(1) AND [Puntaje]<=(3)))
GO
ALTER TABLE [dbo].[PuntajeInspeccion] CHECK CONSTRAINT [CK_PuntajeInspeccion_Puntaje]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reserva](
	[Reserva_Id] [uniqueidentifier] NOT NULL,
	[Vehiculo_Id] [uniqueidentifier] NOT NULL,
	[Persona_Id] [uniqueidentifier] NOT NULL,
	[Precio] [float] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaVencimiento] [datetime] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Reserva] PRIMARY KEY CLUSTERED 
(
	[Reserva_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IXFK_Reserva_Vehiculo] ON [dbo].[Reserva]
(
	[Vehiculo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tasacion](
	[Tasacion_Id] [uniqueidentifier] NOT NULL,
	[Compra_Id] [uniqueidentifier] NOT NULL,
	[Inspeccion_Id] [uniqueidentifier] NOT NULL,
	[Tasador_Id] [uniqueidentifier] NOT NULL,
	[Valor] [float] NOT NULL,
	[Justificacion] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Vigencia] [datetime] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Tasacion] PRIMARY KEY CLUSTERED 
(
	[Tasacion_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Tasacion_Compra] ON [dbo].[Tasacion]
(
	[Compra_Id] ASC,
	[Fecha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tasacion]  WITH CHECK ADD  CONSTRAINT [CK_Tasacion_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Tasacion] CHECK CONSTRAINT [CK_Tasacion_DVH]
GO
ALTER TABLE [dbo].[Tasacion]  WITH CHECK ADD  CONSTRAINT [CK_Tasacion_Valor] CHECK  (([Valor]>(0)))
GO
ALTER TABLE [dbo].[Tasacion] CHECK CONSTRAINT [CK_Tasacion_Valor]
GO
ALTER TABLE [dbo].[Tasacion]  WITH CHECK ADD  CONSTRAINT [CK_Tasacion_Vigencia] CHECK  (([Vigencia]>[Fecha]))
GO
ALTER TABLE [dbo].[Tasacion] CHECK CONSTRAINT [CK_Tasacion_Vigencia]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoDocumentoCompra](
	[TipoDocumentoCompra_Id] [uniqueidentifier] NOT NULL,
	[Descripcion] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Etapa] [int] NOT NULL,
	[DiasVigencia] [int] NULL,
	[Orden] [int] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_TipoDocumentoCompra] PRIMARY KEY CLUSTERED 
(
	[TipoDocumentoCompra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TipoDocumentoCompra]  WITH CHECK ADD  CONSTRAINT [CK_TipoDocumentoCompra_DiasVigencia] CHECK  (([DiasVigencia] IS NULL OR [DiasVigencia]>(0)))
GO
ALTER TABLE [dbo].[TipoDocumentoCompra] CHECK CONSTRAINT [CK_TipoDocumentoCompra_DiasVigencia]
GO
ALTER TABLE [dbo].[TipoDocumentoCompra]  WITH CHECK ADD  CONSTRAINT [CK_TipoDocumentoCompra_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[TipoDocumentoCompra] CHECK CONSTRAINT [CK_TipoDocumentoCompra_DVH]
GO
ALTER TABLE [dbo].[TipoDocumentoCompra]  WITH CHECK ADD  CONSTRAINT [CK_TipoDocumentoCompra_Etapa] CHECK  (([Etapa]>=(1) AND [Etapa]<=(2)))
GO
ALTER TABLE [dbo].[TipoDocumentoCompra] CHECK CONSTRAINT [CK_TipoDocumentoCompra_Etapa]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoEvento](
	[TipoEvento] [varchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Modulo] [varchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Criticidad] [int] NOT NULL,
 CONSTRAINT [PK_TipoEvento] PRIMARY KEY CLUSTERED 
(
	[TipoEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_TipoEvento_Definition] UNIQUE NONCLUSTERED 
(
	[TipoEvento] ASC,
	[Modulo] ASC,
	[Criticidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TipoEvento]  WITH CHECK ADD  CONSTRAINT [CK_TipoEvento_Criticidad] CHECK  (([Criticidad]>=(1) AND [Criticidad]<=(5)))
GO
ALTER TABLE [dbo].[TipoEvento] CHECK CONSTRAINT [CK_TipoEvento_Criticidad]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoPago](
	[TipoPago_Id] [uniqueidentifier] NOT NULL,
	[Descripcion] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_TipoPago] PRIMARY KEY CLUSTERED 
(
	[TipoPago_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TipoPago]  WITH CHECK ADD  CONSTRAINT [CK_TipoPago_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[TipoPago] CHECK CONSTRAINT [CK_TipoPago_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tramite](
	[Tramite_Id] [uniqueidentifier] NOT NULL,
	[Vehiculo_Id] [uniqueidentifier] NOT NULL,
	[Persona_Id] [uniqueidentifier] NOT NULL,
	[Factura_Id] [uniqueidentifier] NOT NULL,
	[CodigoPrecarga08] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FechaTransferencia] [datetime] NULL,
	[Observaciones] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Finalizado] [bit] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Tramite] PRIMARY KEY CLUSTERED 
(
	[Tramite_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tramite]  WITH CHECK ADD  CONSTRAINT [CK_Tramite_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Tramite] CHECK CONSTRAINT [CK_Tramite_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[Usuario_Id] [uniqueidentifier] NOT NULL,
	[DNI] [int] NOT NULL,
	[NombreUsuario] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Clave] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Nombre] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Apellido] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Rol_Id] [uniqueidentifier] NOT NULL,
	[Bloqueado] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
	[IntentosDisponibles] [int] NOT NULL,
	[CodigoIdioma] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Usuario_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [CK_Usuario_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [CK_Usuario_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehiculo](
	[Vehiculo_Id] [uniqueidentifier] NOT NULL,
	[Patente] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Marca_Id] [uniqueidentifier] NOT NULL,
	[Modelo_Id] [uniqueidentifier] NOT NULL,
	[Version_Id] [uniqueidentifier] NOT NULL,
	[Color_Id] [uniqueidentifier] NOT NULL,
	[Precio] [float] NOT NULL,
	[Opcionales] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Observaciones] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Puertas] [int] NOT NULL,
	[Anio] [int] NOT NULL,
	[Kilometraje] [int] NOT NULL,
	[LinkImagen] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Vehiculo] PRIMARY KEY CLUSTERED 
(
	[Vehiculo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Vehiculo_Patente] UNIQUE NONCLUSTERED 
(
	[Patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IXFK_Vehiculo_Color] ON [dbo].[Vehiculo]
(
	[Color_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IXFK_Vehiculo_Marca] ON [dbo].[Vehiculo]
(
	[Marca_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IXFK_Vehiculo_Modelo] ON [dbo].[Vehiculo]
(
	[Modelo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IXFK_Vehiculo_Version] ON [dbo].[Vehiculo]
(
	[Version_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Vehiculo]  WITH CHECK ADD  CONSTRAINT [CK_Vehiculo_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Vehiculo] CHECK CONSTRAINT [CK_Vehiculo_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VehiculoProspecto](
	[VehiculoProspecto_Id] [uniqueidentifier] NOT NULL,
	[Patente] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NumeroChasis] [varchar](17) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NumeroMotor] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Marca_Id] [uniqueidentifier] NOT NULL,
	[Modelo_Id] [uniqueidentifier] NOT NULL,
	[Version_Id] [uniqueidentifier] NOT NULL,
	[Color_Id] [uniqueidentifier] NOT NULL,
	[Anio] [int] NOT NULL,
	[Puertas] [int] NOT NULL,
	[Kilometraje] [int] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_VehiculoProspecto] PRIMARY KEY CLUSTERED 
(
	[VehiculoProspecto_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_VehiculoProspecto_NumeroChasis] ON [dbo].[VehiculoProspecto]
(
	[NumeroChasis] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_VehiculoProspecto_NumeroMotor] ON [dbo].[VehiculoProspecto]
(
	[NumeroMotor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_VehiculoProspecto_Patente] ON [dbo].[VehiculoProspecto]
(
	[Patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VehiculoProspecto]  WITH CHECK ADD  CONSTRAINT [CK_VehiculoProspecto_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[VehiculoProspecto] CHECK CONSTRAINT [CK_VehiculoProspecto_DVH]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Version](
	[Version_Id] [uniqueidentifier] NOT NULL,
	[Marca_Id] [uniqueidentifier] NOT NULL,
	[Modelo_Id] [uniqueidentifier] NOT NULL,
	[Descripcion] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED 
(
	[Version_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IXFK_Version_Marca] ON [dbo].[Version]
(
	[Marca_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IXFK_Version_Modelo] ON [dbo].[Version]
(
	[Modelo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Version]  WITH CHECK ADD  CONSTRAINT [CK_Version_DVH] CHECK  (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
GO
ALTER TABLE [dbo].[Version] CHECK CONSTRAINT [CK_Version_DVH]
GO
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'f3c16db0-0bc2-4492-ad49-0a6247c9f92b', N'Orange', N'BB2A0B0DE5037F8F487C6A10551F211AA93F267A51F27ECEC2F2E84F06D059A3')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'e261671f-0722-4ef4-b7c3-1719053a73df', N'Purple', N'E548C7AD8A66C4B13E30B829E0FC898346DEAF2BD89867E5E5DBEBC5BB9A3809')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'69c571ec-b376-40a5-a846-1ebafdd56724', N'Black', N'C547E8FC83AFEF527E027830BEA165560FA1D283C932A4AE63DDFB1BF2306E6D')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'e0fcf22c-504f-4d00-92fe-35bbb1ede9ca', N'Red', N'D6144B59E85BC5D85095D6FF85F4D99083454567CD9705A959AC33130716F1BD')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'a7d249af-19b9-4108-8d0c-4c5b235b67c8', N'Lime', N'30CE2FBBB30AFBC9EB76F27413575457BAF6349244C7E7385D9699D6538596E8')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'75942109-a843-46dd-9347-54dc1f6e2a19', N'Blue', N'C0B838598A023681F2DB067399D3D58307DBE10159942A04D42B5DC82FC6CD03')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'2ef6bd94-c559-4d30-94ff-697aa2e29c4c', N'Gray', N'2636A3CD081ABB477385A58FBF01C21F1FB746FB0CB2B30DE1C032B106D04EB2')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'41a97aea-b0dc-4467-b096-a9d11587d92a', N'White', N'DFAEB548443A7858FBAEF5679B7B20D96B12C62AF0CE8DFC0BFD187A82797490')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'292cc8ed-651a-4727-9a94-ab3c87561a3e', N'Turquoise', N'EE4395453E5CDF68ADA4D3FC2AF9122E4336CDF4E144CFD8ABE5DFB2BC9B56A1')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'3dac2cd9-ef56-4740-8de7-af4f529df9b0', N'Yellow', N'9CC40079D095643BC7855C3D25110D592316C3B02B11FF51050E9E08964D0AEB')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'46c8f626-c0d0-4a3a-9e66-b727e9f57527', N'Pink', N'3913F9B4B1F9A1FF2287E98A275483560068B2EBE59E5345F61C2AAC42792284')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'fe5e118d-bec9-42b0-bdf7-bcd766479581', N'Olive', N'20444A4680185947B413E3016AD8BF150242271B488FE253B573EF811E6E711E')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'a0a25b04-0be5-482d-99e3-c27f46c54353', N'Green', N'6588E3917E4D104F96820B8E2AB1F7578A09864DDBEB628F5CB800E54B104A3E')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'4736518d-7978-44a9-8cd5-c51159dd8e35', N'Light Gray', N'CF19A73B317D732FC8549EB780FF558F57B613896EC4746CC50CE1122939926C')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'34ea6e3d-0824-4964-81cc-c928d2ed6162', N'Magenta', N'617955D213614934C2398736849E0D39724BDD447E86891FDDA15BD47FD38FDA')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'36090393-95c7-4cd3-b052-d0e8e592f8da', N'Brown', N'B636402EAEC7034A6B97957FE0F45D9F3180D022F1D416EE1EE996D615E0CC0B')
INSERT [dbo].[Color] ([Color_Id], [Descripcion], [DVH]) VALUES (N'fa3c6974-9fd6-4833-9e06-da7803afacd0', N'Beige', N'DC07F86266C0B40B0B3B27C888EFEC7EDEF71CEA2207880846E61B9074E9A426')
GO
INSERT [dbo].[ItemInspeccion] ([ItemInspeccion_Id], [Descripcion], [Orden], [DVH]) VALUES (N'2d636cbe-2e86-40e1-be2a-04796a0bb3a7', N'Brakes', 3, N'182D89D8F2204EF6C9F0ECFFCDF23E9B33E2DD638A9060C4D6E22CDD44924C3B')
INSERT [dbo].[ItemInspeccion] ([ItemInspeccion_Id], [Descripcion], [Orden], [DVH]) VALUES (N'bf63f1cb-5384-4b82-bbf4-101e0f6e7ea0', N'Engine', 1, N'6C2388ED08E4A389909622DCEEB585471F15E1DA5450540108D8ADE5944A8294')
INSERT [dbo].[ItemInspeccion] ([ItemInspeccion_Id], [Descripcion], [Orden], [DVH]) VALUES (N'1d69384c-5525-439a-a359-2518f4f8eef3', N'Transmission', 2, N'2BE9AD5E6762B54E0294884F682B94B6D76908FDEB3BA63A29882A133B680540')
INSERT [dbo].[ItemInspeccion] ([ItemInspeccion_Id], [Descripcion], [Orden], [DVH]) VALUES (N'56d50f99-fe50-4421-bae5-4573d1874192', N'Interior', 6, N'9EAEFCE00480BE3B2E0E3E63C008D4742461E77DB809E76062BDE79E362F09D6')
INSERT [dbo].[ItemInspeccion] ([ItemInspeccion_Id], [Descripcion], [Orden], [DVH]) VALUES (N'5f05bf89-4d3e-4538-b4ce-470d774b8db2', N'Bodywork', 5, N'354716979350D4A31248DC15F3EF9CDD9C515A442833FFE13F11CB9F17CCFEA7')
INSERT [dbo].[ItemInspeccion] ([ItemInspeccion_Id], [Descripcion], [Orden], [DVH]) VALUES (N'cf84b30a-eef1-4353-bca3-556ab9d306fc', N'Suspension', 4, N'B4EF08D1C11377DFE910113E08B2A4FD4BC0F4EE36D808511A2D53CDC164AA69')
INSERT [dbo].[ItemInspeccion] ([ItemInspeccion_Id], [Descripcion], [Orden], [DVH]) VALUES (N'6c0aece7-38c1-4173-90c1-6d3671f921ed', N'Tires', 8, N'854EFD6284F2BC96D0475B2FC4DFC0999B759275F7D0F991DB95C3B84F672467')
INSERT [dbo].[ItemInspeccion] ([ItemInspeccion_Id], [Descripcion], [Orden], [DVH]) VALUES (N'ea6122f9-2070-46d5-b86f-a7150b8cb570', N'Electrical System', 7, N'2891643D6495C1C311F2DECC0CF029BA5945949E5457ED0B4840EA841F77AF67')
GO
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'004ef38f-0a07-45b4-bf3c-05016f606143', N'Mitsubishi', N'1CD33E3CFBCB2A0683DADE3376FA7A5C9A1AC6F8B502D789791228239175DF01')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'e5c72cad-3f3b-4772-acc7-09c5d0de2df5', N'Lotus', N'1FDF24EF3D86E24F733296DC093F4CB3811DF5F53550B118A385B1684C04763C')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Volkswagen', N'C48C46BA3E12CE93D65D0EAE2F127D832E7AC4E8109C28ABBBEFE749893AB968')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'f7cadfc4-971b-4c94-a188-294993121c46', N'McLaren', N'9BBD6BD30CFF8B2487B8FA46A24D881CB5EC58B70D027BFC225FBB2FEF8848D6')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'c6e22639-c326-4a11-b2c5-2ca6a1fce297', N'Mazda', N'30BA2138B72806EB4A54C3B4DD218963D8D2CE3964606F2951E04AED60AF07EE')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'a9bccb6c-7a0d-4843-93e4-462b688130be', N'Maserati', N'6CBA674903D79B1949727AC6AB6CE4EED4B3A6C0916A7FADB4517CF013D7EED2')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'd0675ef9-bbc2-4ac0-bc73-4c399664f67f', N'Kia', N'EB4F678F792C7BAD791EF18C3481B2759BDFD70CB452431A32E8AC1D65E19C3D')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'Nissan', N'FA6AA66C3351A20B6B38F291F531B63A04C8F8D6D92BAFAA4EFC2A2E39BA533A')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'fd608800-4857-448b-871a-529135e2b26d', N'Audi', N'963C70A66561571972352466EEA63A9A118742D157FA627EE4A77CD2951E1677')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'Ford', N'F845CE9B0F574C6D55CA8B7337D551E0622D022A904CED8992A3B0F848FD072A')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'08e821d5-e981-46b1-a862-659c6917ae4b', N'Honda', N'0A4DEE92C629461BA61CE28D888611DC35E97FD54ED624501E7A02C507B8B50B')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'0649d0d5-cccf-4a20-9229-7535688916ec', N'Alfa Romeo', N'F096DA4725137ED817C0605CD5C5970A82B1826D0C04F884CEAD39D1538FD862')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'Peugeot', N'E03E615704212F96AAB0A83A0BD7FC19C800038EE751F931A4F6928597D00CFB')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'a5436202-8bf0-4f04-994b-7bf25d453884', N'Jeep', N'291D33445581EF6B6C266F9354438868855E9A037F587DD5E3829FC198301F26')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'd9e8ebfd-1891-4569-9822-8ba9da609283', N'Suzuki', N'7899E8F21BA712669FEEA88E632B61469DBB9825437EC16F13E691723897E02A')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'Toyota', N'82A57B67510E30A7BD4B34C9CF9AEEA92C2C8F93D0DC674AC0BC470B9C671246')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'Chevrolet', N'323214A6435081A117D991C72CE3B208C806C01CCF3929D524A3D5B2CF67D5AF')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'Fiat', N'075ED748A6BA016E8CAE22F5ECC6142E090B0C1A52BFFAF38E89A29914DCCA46')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'badbe58b-257e-4f44-9e4d-acabda3a2abb', N'Lexus', N'7D3504360EB41CB045989BA8B067518AE0C7A5803A28A81704D49FA9AE78C31B')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'51df51d4-cd02-4457-b1cb-aceb35899925', N'BMW', N'28177AD53BF95A1064B13023D7EDB17B4B8AF4923C43926C8B3889008D00E36F')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'caeebfb7-c63c-4b2a-b31f-b43283dae1d5', N'Land Rover', N'9C9899DFCCFF66F1FE22060C169483C6680843993A9351E9B43F603F9C0E356C')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'acc85a9f-84ae-4075-9b92-b640a2e3bea6', N'Mercedes-Benz', N'411F040AB72F5263519428CD99440E5428E70DAC35D81CA4FA0977582515F5DC')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'78f8a52c-f6a6-443e-9c02-bd783abbb59a', N'Hyundai', N'FEBD9AA8CC6B698F4BCBFFAD163F5DE77680D0D75282E30179F16D9D744E04DA')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'Renault', N'CA951BA6C8EF013D3C1ACCBDC0ABD7C689C8CDF8004028D115678964C4F59C56')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'48456a63-55b5-4c5d-a20b-c5483e5a6511', N'Citroën', N'556F5ADD86AD5D1C6DE8AD0D2F66BC897811EF5E3D0D2ACFB2A7E4E9E1E431D4')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'8075896c-f9e6-4360-9d94-cbd865da4250', N'Subaru', N'115237524B7FA11638FCE00213924DFD64A4BFCCC99B557EC842CDCEBE01330B')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'c545704a-16f7-4191-b242-da582081b0fb', N'Lamborghini', N'BCD720CEFC01C98B87C5D3825848496FA7903DE8A85F656924ECB745572D4094')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'73b3ab77-268d-450c-92d2-dc4adadf981d', N'Ferrari', N'3773DA57AA5EFE96102F1C36BBC0A483F869A8739939E88CAAD4CD68EC75540C')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'ba3259e7-4737-4127-a158-e6e7a694bee5', N'Dodge', N'A177E8DF094D81FDE797EDAD0DEEAB8D76ECDC83E944F9A05D3A693CCB78B131')
INSERT [dbo].[Marca] ([Marca_Id], [Descripcion], [DVH]) VALUES (N'6fee6413-f13b-483b-86f9-fc72b24a6c64', N'Chrysler', N'10E287BE0E9BCEBF46906C0B958479C5D42D91B067F16AC53B2B8EFB5BB54D08')
GO
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'1e56ad70-f2ed-4523-8ebc-023dc10d6612', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'504', N'B43B4A32CF95B6DF82F91A6BB13AEFB914392B0C762E584DB00D8D6F465A16F9')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'b6c74818-0407-43f8-b4c9-058963acca06', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'C-10', N'89EC565651C94F3160C098A2EB764D04BE4A0A794B0A58D668D6689E20CB5AAB')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'6ca85dc6-17a5-4f5b-98e3-097979d694f9', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'Accord', N'A16A0662642342A9ACD1A2A7CEE7DF79BAEDAEB3BC59E71DFF9996BE946FB325')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'922253f4-2458-46e0-b036-0b76fdcab925', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'Yaris', N'71F206900C5688D4047F1BFBDD0E713F0840BD1869BF092BBE62710025191A45')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'05d00969-2520-4c27-ba48-0c9ad48051dc', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'HR-V', N'5199F7C1644BD5648E1977FAEFC4E616202F8E994D45E98737F0567977C62AD8')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'5bc08ba0-5961-4127-9d3e-0e853ea45f50', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'Duster', N'AB9D8D561D0382B8086BA89BC7FCC2D1AD67DCE03124FFFC6759D726C06402F3')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'a6127020-693e-4128-a6ae-14cb13e7504b', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'405', N'A7ECA8A7B427905CB44176D5C8DA02AF3C34112C9A8709B86425DFE6DB59F943')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'07e310a0-29de-473a-b103-15c09ef387ef', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'Camry', N'EDC4273F9407A9B30190038E7116EFB4E23F99A1E405EB76167D02489BE51B16')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'8b593860-b996-4bb1-99d4-1869bc68ab24', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'Pilot', N'C8BEDBE51EDE49A8F2BF2C0B3669DCC67111C434268BB4D1F1AF7F1F9849CF42')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'503c5d33-0e2c-4ebc-8f6f-1d6039e6ba38', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'3008', N'244E457E0DDF20CEF5124104479E6B489ECA4FCEAAC35FE7C68738672C8E1DC9')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'185fe2e0-73ed-4de4-866f-1d79ea7f77f9', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'Fit', N'104A9DD5F46564CA2EE05C8AC465903ED4A541D23D90388842114FCA8BD5B23C')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'097f426c-a33e-423f-bb47-1e0568b5795d', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Taos', N'A449F9DE143EF09E63F19A58A9B5EFA03E3D0C45BF2E3FE78AF7B796C07A8089')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'6ceea7d4-eff6-4303-90b1-1f1c742e9025', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'Kicks', N'54ABA6BBC8907BF6F52ABD803872817D083C494E93C361BCA29AC914492882A7')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'2e5c350b-f76f-4c32-84e7-23f165d2db5b', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'Cronos', N'A729AAD7A2E5C9B04FA9231E2DCAF6BF9CD7299F8D1A41BA0CF4D58459A4BF8D')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'f28720fa-7302-435f-9610-24aa9fca3cad', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'Punto', N'E7686C4F04D03C0604FA21866855C641CC5A69B6959032F9A11DA8E9FFCF3658')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'235d2a3e-a3bc-44d9-9f06-29377fd514e4', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'Ranger', N'277328D69B523C4A641671A8481385794BA260779ACB61135C3D36ABEF34E634')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'2675039a-f5e3-4140-9edd-2cc60438c1c3', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'S-10', N'53BEB57DA9B594300F728536EE4803A4892AF98E9450C14F26A4B0314422530F')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'938d07be-badc-4f5b-9dbc-2eff9d0dc6e4', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'Rav4', N'469DB66FBAEF649535AD7F29611A838D219647410E3E1E4B825E849B66F11D0B')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'e3e7a037-a610-4859-ad7c-31f90354c593', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'Palio', N'D2A508698F05F2ED77A56F4CF3621CAB1F013030C160EB9B0999AD7D4A8D0673')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'69fbf34f-ae5b-42c3-bbc2-31feb83720ae', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'Agile', N'241DF42DE86E508FC61D3D6AEAC34A0EDBE8384DF7C79D5FEA7A053AA34EEE97')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'aaed517b-7d76-4aa4-b03b-36d1412f34c5', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Suran', N'03DB5BFA7564A3ECC4D6E82CADB91D17523AFAF8A718778C0EC11EC91F872906')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'8a4f47ab-87c7-460a-bf59-3ccea013e3cc', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'208', N'F300D4254FF6192E1F29B18890E6E88BB9506963110DED399A8C052B8885A75A')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'1f35f1d4-b95f-47fb-ad6b-4029b75fc910', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'Kuga', N'D6341C24FDB6438669ADB31F1C8C5DB43564D17A756BC8D3167964A1197A5D7A')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'e4e95acc-33a1-433d-9a00-420bc92dfcf8', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'Corsa', N'DFA91C6EF6C256690C8D52EA15C4D02C2F35C39F34081A63A1F2EDA7B5BA95CC')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'54987e31-ab0d-44ae-b62e-46910b835122', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'X-Trail', N'8986887DC0560C7026ACA405D09DFA23B64B8758864632EDB7FCFF407487DBDD')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'49d644d8-cca7-4a72-9117-524022f9f164', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'Sentra', N'748549C33AAFFEF92BF82F5C9DCE9A5625392C93ED6EF377423FD6E5F5682D01')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'b697ac98-2a2c-47e2-afff-5d67d27216ac', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'Argo', N'59B153A5A2E0E8C2B3C07DB4F4D800A4F32636C95E35CE494985270284806489')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'b08672b3-eeae-4fe2-9541-5e78ce2f2456', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'Focus', N'D54C40ED60F165957BA897355BE657CC4B9971941071A4B18A6B905DDCA705CF')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'727d193e-e57d-48c5-b1a4-618d1fe56066', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'Toro', N'EB1C7982B2D0ECF582F9184E6F99034043E472F7127EDF344D004E3B46DF6B31')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'0a1d4ba7-5811-4e56-b6cd-622e5c10b906', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'Prius', N'E9FB4754C59086C587B8B09FD33264505F20CB5B28D55DDA79781AFA25789DAC')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'c49a3608-7243-4c70-bf41-635ed02467f1', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'March', N'8377F8863C7A1778001EF89220BB0CAA4755E973D11F1883459A94C346FCA324')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'd4eab283-b909-4020-9f13-6584349e0ba8', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'Logan', N'D868BBB22509D7014704400C4298FA693F6A03716BB98A8C95EDC7A81028305F')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'14d1c39a-72e7-489d-bb6e-65baac07abb0', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Polo', N'89DF2C5F08186D319FEFB30DA69C0484D1CBDAC68559B2E0D9D3D1FB5D550E72')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'6de1611c-faff-4310-a731-6ddea5e5fd99', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'Corolla', N'470B310FCFF01CF703E9DB00D6B99FB176188304346DD9E9B5C028F26D57E9AD')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'78b92d89-e7ab-4fee-a52c-6fdcd7e07cd2', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'Tipo', N'1781BD7FA56B70CAEB10E429C7A20B8246199129359E694532B746BC053A71CE')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'9552c585-0337-4aae-a278-7268ddb0e860', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'Clio', N'D0890B6FC833E1241ABAC9E6E7BBBEBFF4E0A3FD7D8BC0BEED7F0CFAE6940A5A')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'4938dcf6-694d-4ce8-b04f-746aa52c30f9', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'Falcon', N'29588DCC3370BC1E0325A1820556F4C7F8DE5DA72D9F83D680031C1E9D9FC6A8')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'449fab3c-6db0-440e-a0c8-74b879d27b6b', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Fox', N'449A6B4F73E48F6044156AB4577AAA23D752A77D5F1555497AF279A716C8E27F')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'bfa0c5f6-5cde-4b0c-a84e-770772225a9f', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'Murano', N'AA54E00709761781BC0D112607E99A5F55E2C18A3BA38ABE1F39DC79438EEC89')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'9688580d-d2ce-4922-87b6-78cd766d9d94', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'Tracker', N'1DF0F2A878FA0C5694054CBA7A79A6AEDD2E52AFA9CE5B1825E9B2D8E6DEDFE9')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'6b3a36bc-1d9a-4430-b505-79d3ce909650', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'Mondeo', N'F2A8E364BAC1AF032F6991F331D6D219F38390E0353F00B34F337925E16259B2')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'd1ef6111-7616-4f00-a011-7abacd3b2ceb', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'Captur', N'5E1B5830FB81F679513EE08288E129F5E04AB6C6A7EB2BB8277F19DAE00D1B24')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'f8947c26-d57a-444f-9abc-7eeefd8e525a', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'2008', N'5F59B939197F90E38255C257601A945454E6019AA7883F6EE0781B9FEC37B2A5')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'9c31b675-614c-4d43-b8f4-80c5fc7a769d', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'Civic', N'47718C3D3F3B470B1C54033834269A080075DB3F93C2A27E9EF14094E5E3CD5B')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'0b992189-e1ab-4d53-9309-81b686031b70', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'Leaf', N'6EED872F5CC6F668EF3CDF4750D45DD4FA9F20B92D19C3C513BE0993D8D558A6')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'ccd9967a-f6ec-4edd-a91d-84626e7b32f3', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'Onix', N'9A41B0FB8FECCBC8A753C6B92E6B6E19BB6853773251C6227F7BC5479D6BCE41')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'cc6a62d3-a149-4c9f-9299-84eaae001fb4', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'Uno', N'075E8AC6A3F5D556971BEE87103F2A22422F46BF209F35813EB5BD2C0433C268')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'569a0062-a236-44b8-bca1-86d818c32e4a', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'CR-V', N'16454388DBBBB2747E782D2A1E487A2308C7D613D2752090D7A73AFD5EFCA19F')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'4c7ab539-80a8-4305-bc51-8951dbd57749', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'Aveo', N'CE2BD6A26FE31356B48D5DA0807CF11465265D9EFD404ED54C61976B13A74898')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'3829c802-7866-4d3d-8ee9-8bf8c43304e2', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'Note', N'C20A4388C4E852D849D46BA315D22267B86C541C6A763A1CFCD46F5DFFE5F49A')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'cf400d07-44ff-4fc2-be22-8f4713e94374', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'Frontier', N'202B3C4F22E2FCD975592AB0E09AE76EA0D691321B9F210E13B39000E99B9AD1')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'd24806ff-9cfe-4a4a-a983-90c2db6aebd1', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'F-100', N'D05652B9BDE7E58458A0A480E376C7CF59002A85657573B2A7247976A48C229D')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'83778495-dc19-480e-9eb5-90ec1a6f6434', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'Partner', N'BADFD855B7FE945FEEFB8C833F842600128D8048F12AEE573F1A35FD18675891')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'6b340928-bcb2-4b0c-aeb7-915446fd6d8c', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'Siena', N'F1050A21C479686E31A9E95B78A0B8A985B1A5AEE0B9A35B0A920816AD7FA838')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'a7585ec1-e405-4354-a40d-926d0d12962e', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'Sandero', N'1C38973355379557D9F6AB67876412251721E8F10CCE7AFA466251F111C508BC')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'05c86100-3eb9-4211-9a88-969ce43f8a55', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'Fluence', N'A7547403EF04C22ACFB1708FB3E4DCEDCF4FA4B69DA8506B3233AD070EE97242')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'ebdb9042-35a7-4215-9143-997e14ca1d00', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'Fiesta', N'A9E9CC1CDC8B7B6348CE490A16395D16EB8D6AE9B4BD775C8F90BCC075B0F10B')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'e4841a65-435d-4fe3-839d-99eaacb7e455', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'Land Cruiser', N'15C1594A5A8E4D5A4EC816CF29E9DCC246AB7FB4088329532A2E0D2D1AB50E69')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'02ffc7e6-5c03-4960-bd31-9ca6055c5683', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Saveiro', N'0C0075E862ED2B4DD1A4A9E93DCFFED3EB10C7C6EF9015A023640DCFD8658526')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'71012599-48d1-447c-b0a3-9e62631c51b9', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'308', N'A046CB69E4D1A99D8C895DFD9BFA2905B0AC9A9EA4B6DF7ED16DCC1E8E9A9FA8')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'157cae27-a6d7-4076-ad4f-a1ed27b69f97', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'City', N'B113F83A56A405E9F498039E7E3D4DC1290447F307A6FAA4DD6FE50ED8C84FF1')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'94635a6e-5f93-4597-92de-a4e747f01cd8', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Passat', N'24CB3A2DC097E49038486687C31D6EAD9656CDFCCCAFC8CCDD6F67FEE30BC6B0')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'78604797-62a0-494f-855f-ad4d5a6be7f8', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'Cruze', N'0524F3B4D382C9C8925FDAE14FCDA5C4C5A62C3E50DB33F83D4A108DA466BB76')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'18f4e9a1-4169-4927-822e-b5ecc7f7fcfe', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'Torino', N'32F6F8F7EED0908ADBCE820A76B92E9499FAE17A0EE17546179D883CC18F54E5')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'd9528843-a358-4fe3-a55a-b729e1def003', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'Strada', N'EDC33D62CFD7DE8E712949C977446EA74E25A44691B92CCAA6F45BD9A2E01D3C')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'd682e006-e2ad-4076-bcb1-b754b34a5767', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'Odyssey', N'564946EA0D002C75C89D5BA2356F6D0CCB5DB4CC0E2F29AA665578DDFBD82EF3')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'517e92d9-b080-431a-bd5e-bad17ded0d7e', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'Tiida', N'D184E5EFF67D59E26035E8A3EF1277CE1B659B64F1EDE59F2BC5740958AA6251')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'c4c68147-3eeb-4d92-aace-bd3c0b9386bd', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'Etios', N'D592E86C52BF760CF0CB24B64DCD8ADC4A35A9325D71FCA6846796312172B818')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'dc7468b0-4b0f-4e22-b5f7-c82d7b71174a', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'Kwid', N'6BD08D350FC1441D37B28C3D218521B14B786B24E423162200975F010D338F5C')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'765dffea-36c7-46be-a97a-c90e1b5fd266', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Gol', N'FA1E5B262E55253CF8CE2906BD022695F9F597C1A903E915753F14D36396972B')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'82d40c31-c727-45e4-b289-ca26a7285a8a', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'Kangoo', N'F048970FA8CB6CAD24F79CDDC08266E2B6F59E7839E3A14DDEB5B1D6FE695FBA')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'296784fa-afd3-4d46-bddf-cb823dd07479', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'Ka', N'4564B52CFEF3BA4421857250D4D2421C88BB2143D40B57AE684F48FABB891DC4')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'e819d539-ca21-4f1e-830f-ce8265c33075', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'307', N'AC38DDF3B75A82B3D57C55C172F0B252696396A3CC585FDCF2EDB624D8CC3FD2')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'431cbef4-4298-4055-8e01-ce886d410c1e', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'Spark', N'9E3ABF7A0F7EFD5705AD463135ABBC52EFB313F5CB9AD6DE25F6885F2880202F')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'f8ea90df-197d-44a1-9218-d0b601930f5b', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'Boxer', N'8AFBB0DCC85180CB1FE678F1062BAF25B354893D463EFFDF2C0BA339889023A6')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'0291a9ac-e62e-42bd-b87d-d956113949e3', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Vento', N'2EBB4408090EDF0761F37885A20421EA68D61B73291402F8EA2ADC700C25983A')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'a5c21623-f75b-45cf-9295-da26717c49ef', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Amarok', N'C300D3BB30462D7F5297A982C0185AF2E144A939F9E934EF82C28005FA7818D7')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'97de57da-8f06-4730-bc4e-ddbfd68a7ad9', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'600', N'F50F03E2AE828DA6A59C4ABBC5EB257A59BCDCEA72D4FA2E74F9355F3BE0DBB7')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'91bef4c2-e350-4321-a6de-deadb4d255ae', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'12', N'C6A085FF6CECF2F965596A81871FF8034CC92E8C398490AF2831487BC90619FD')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'9bf6a112-cc10-431a-9e84-df741745fa03', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'207', N'0A41381EB24033A873FBB16E4077F50F624071519DB8B60B50DACB2EE4DE805B')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'67973c30-1b5f-40be-9ad0-e213921f8268', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'Chevy', N'CC61CE3E564A042ADC0930790C767BB4B2C97279C761C21024B028A17A18F635')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'2da2789b-bf6c-4d1c-8d12-e42670f437f4', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'Transit', N'C273C004CB8C76D2EE3DC940C1BEFBBF5DD45234F49DBD47107FDE08294CA9FB')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'3cf41628-d1e8-4803-92c2-ea5e522ed1ac', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'SW4', N'5F6850234F004487938ECB59999C412DF5786D1BF85D4D32638ED461A590F0A9')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'e3a4456b-5a3d-48fa-b20e-ecfd59176900', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'Versa', N'3CA0213B17884AFE265EA9E8F64D72C96A930524F29C5F081C536583B3A39AF5')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'0422cabb-b263-4661-81f0-ee3f07dd6dc0', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'Tiguan', N'47B489852E023C0E98D050A4D65A09884D98B0874247A5251B5DF4FAB7C46477')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'157ec41c-526f-49bd-81ef-f967b7c757f4', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'EcoSport', N'E5BE2F94B4DFA659820C7F354E2A992267970D1C409A4EB70CBED6D5B11C4F04')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'0401d533-5664-4a3c-9a61-fc8f23a09faf', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'206', N'D6D9648E4D7559E0A3D48C490E2DB18518793F9AD07AF97B997BA750E5075D5B')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'82b40e37-f246-4da0-af32-fd6b8f955883', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'SW4', N'F8959DB38E050EB8BB4EC8EDD972489E31FF366389824E3652697E389447E2E7')
INSERT [dbo].[Modelo] ([Modelo_Id], [Marca_Id], [Descripcion], [DVH]) VALUES (N'c35dcb7c-339b-46ba-83ef-fdd1914fbdba', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'Hilux', N'32077B6550DCD54F525370EB8EE980B54C4CB199AC8B3380BBAA5BBF2440EBC6')
GO
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'dacf376a-77cf-4970-ae9b-10c14a3187d3', N'Register Vehicle for Appraisal', N'RegisterCompraForm', N'9F725B9BC7D3F5EB9C0A57B748F6688BED52FBF606201295A206C927856400C2')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'b5a35e91-41d7-4a37-9fde-187c019a3caa', N'Pay Vehicle Purchase', N'PayCompraForm', N'2083A7D9B5110574BF6F8D5B27C2013A3842352FF73DF46DADCC7FF83ECCC319')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'f14efe30-5c10-4fb1-9c46-1e6a27ec8f5c', N'Register Mechanical Inspection', N'RegisterInspeccionForm', N'C9A9D40852FCE96CD51D24E3325C9E8DC1A44D30AC727369FE9832E474C2B79A')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'Admin', NULL, N'4E00757615C17AB27F273B7FF2C3A942B10D564A0B25A97B5741F45CF8A5FBF4')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'5fda21db-c888-4d63-9890-2dca815d19c5', N'Administrative Staff', NULL, N'8F42842FB083088E015969F3E07D5C931E092D683031D8470EA761C43B6209AB')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'b46b4cf6-d952-4405-a981-2ff3aafee3fc', N'Vehicle Management', N'VehiculoManagementForm', N'39EC51C079F5E5A8F4898DDD36FCEFB16E04FF0C3CB9BE14AD7261577C23F9E7')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'445d3d41-2082-485d-8738-308d1563ac74', N'Proyeccion de ventas', N'ProyeccionVentasForm', N'694527483D0F355EBDEAC7DA3EB6018EEA6C9A411D3924B76E9EA04F4FA4268C')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'5098638a-8697-4234-a955-359f29f0eefc', N'Generate Reservation', N'GenerateReservaForm', N'337158E02105A490F653D6DE46971F0EA647778971E8345C7B3B8395C4869676')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'b3197ad2-9a95-4a3d-9ae1-4fd0afb0bc00', N'User Management', N'UsuarioManagementForm', N'190FBD03B5254C8CDC526077026F9B690797252C97C761BC7E64B9A5AE417F5A')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'4bcc437b-a249-4df3-b824-5b42f33041be', N'Vehicle Model Configuration', N'VehiculoModeloConfigurationForm', N'462E5AA9D6ACF625710F767CDBA99DAF5F10E12186F4738AF09ECE2D485BD6C8')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'fe08f3cd-ff47-4c46-88a6-5ce41b688b56', N'Bitacora de eventos', N'BitacoraEventosForm', N'BE017682DA3052618D0694BED18E3E55F3F2976B2D9155520A0EE8DDBD613D1A')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'bbd629ba-d1c7-4d7e-b49f-5ffcc710af9d', N'Add or Edit Paperwork', N'TramiteForm', N'FBD90A8B96557638094E3F84F24622365527CEF649B4CBFED9C4C9B9EC19A742')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'cb8be7e5-9607-4906-9dc0-632254235014', N'Add Payment', N'AddPagoForm', N'2A2058EF4B996655D94B9193515A18065A829DAF98C1D1866579549D2AA98401')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'998a4eae-4bc9-4078-803b-6e2f90d53f46', N'Salesman', NULL, N'B6309E489E8A1B2C9AEB732484C8347A724DE16A819815F86227AC4F1B8EF1AC')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'dc52da02-4628-414f-9253-70c68f5f5476', N'Create Person', N'CreatePersonaForm', N'4E2A41F483B1D667231D97DD4800F98C464F905AF1485639FD42639DB18D9F95')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'99652a20-ef01-4982-ab21-7d06fd1270de', N'Generate Quotation', N'GenerateCotizacionForm', N'5F5ACF7DFC71CF444B5BC96DA437C2A481A9DC2FE27D8FB0E6FF483DC7EC7349')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'5ea3d44b-453d-454b-baf6-8f012fd3d8ac', N'Change Password', N'ChangePasswordForm', N'EBB85A2407E0B0E983999AE77509B5DF5CFFD53A595305D1AE8DD6C5237BA33C')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'32d2ede6-fe7c-47f8-94d2-a161dad6d274', N'Bitacora de cambios', N'BitacoraCambiosForm', N'0D75446C8DCF44F1B68EBAA667D730AA33D8B459D97074B845AAF7E68BAFAB19')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'6e01dd6e-5164-484c-b576-aa83bd26c1ac', N'Mechanic', NULL, N'E74000F3230E24F35BEF63F506E609F4359785487B3BB8BD44BD949A38567B0A')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'521ab5ef-abf2-4fd6-89bd-aebdfdba560c', N'Manage Vehicle Purchase', N'ManageCompraForm', N'8F1FB2E764134092C6497502CFAFA9DADCF8F2C881B2E9D6BD52BDDB037DD4D1')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'7e8bd151-63ff-4abb-87a6-b54fcefdce8e', N'Permission Management', N'PermisoManagementForm', N'D6B78CAD2FDCC299CFE91B54431E550AE8C2AD064EDD3939BFAFDCC72FEE4986')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'8269844e-36db-46e3-8d66-bcdc3514639c', N'Proyeccion de compras', N'ProyeccionComprasForm', N'10A03A4EDB011BBA8F64D8C1B6FA15DB611E0159B85F3177BFF3401DBA8E2867')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'4293e0e8-4a85-486f-927c-c4999a8f4038', N'Base User', NULL, N'756A43BDAD807E774505A21C21921D36B68EC7BDA0F7BC68FB853F023B3CE88F')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'543f4160-1340-412b-a31f-d3d35949aaff', N'Generate Invoice', N'GenerateFacturaForm', N'B4852976691485C00107232825DD924E61D1DF89D046262DF79638876E42EC54')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'98ae6dcc-af25-4bdf-91aa-e00b26c40987', N'Recalcular DV', N'RecalculateDV', N'2B74AFA0D54D6DF648E5BF52469D7CD69B690CEEDDCA36BC985296D2058CDE4A')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'57b792bb-5368-4fe3-baf8-e054d090a854', N'Manage Paperwork', N'ManageTramiteForm', N'62C135E068F2A95F4323B33BD8F8F6567E1F1B2AAD0152E5B3A41F6C7499EB36')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'dd7a8d2f-8273-45ff-9a53-e1d0b332e8fb', N'Backup / Restore Database', N'BackupRestoreForm', N'47B12ABA8515BB59950EFC011BEA94133942573F356AC2AB72A29BB98E0C8E17')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'f53282e8-496e-413d-afa7-f04fe6ab3a26', N'Cashier', NULL, N'3FCD356D9C0A404BCD61A36688A76EC89EC7DE94873A4B45939476C73F2D427A')
INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES (N'9f70629a-7d78-400b-bd94-f13f5f356e24', N'Appraise Vehicle', N'AppraiseVehiculoForm', N'5A9CEF1B65D8C62EBE9465316709437074078CF045FA94DD763B11F6F682D126')
GO
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'b46b4cf6-d952-4405-a981-2ff3aafee3fc', N'32492B1058412385D838D1B86F38BC360EFF6B1ED0C9B14CFA53E463DE107427')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'445d3d41-2082-485d-8738-308d1563ac74', N'72B764F890B4E7F76756D511D3EEB8DC5001D8402C044195073C5A4D7C6AEBC4')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'b3197ad2-9a95-4a3d-9ae1-4fd0afb0bc00', N'8683AF227B1D47BD74118E549D9781577240F3B7E890159E22803688D7125ED7')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'4bcc437b-a249-4df3-b824-5b42f33041be', N'F35FEED8A704285D0D6F1869A1A24B7B31C4EE16F5B968825CB5F8FC3D88A2BA')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'fe08f3cd-ff47-4c46-88a6-5ce41b688b56', N'4DDA6FB6AD45F5BAB95DB399B0A127E6CD0BD7CD47A2B7C2CD9F2EA04153E0FB')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'32d2ede6-fe7c-47f8-94d2-a161dad6d274', N'C880B995C2158F046D3EDCB6AC53DAF806CC48AF89A8A602E5EB5F84F5C23CB3')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'7e8bd151-63ff-4abb-87a6-b54fcefdce8e', N'D44F609A4156AB247E39F97F76FB80B0FE2716358B0292C87BC550356FB2428E')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'8269844e-36db-46e3-8d66-bcdc3514639c', N'DC328A7FE0A7F4D50DD989FA66B8DBFE820C8409C88C2C14C446C1ACAE1865CE')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'4293e0e8-4a85-486f-927c-c4999a8f4038', N'A141B4D4A6BF29E3266EBFDC29198E3AD1B1319987212E0488788714703AA303')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'98ae6dcc-af25-4bdf-91aa-e00b26c40987', N'DA2074BB2292F176CED6DF21F17DDABF1066E138E732FC92A52DBA94B355F51E')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', N'dd7a8d2f-8273-45ff-9a53-e1d0b332e8fb', N'BAB0D397D41D43087171AD3A5D249A1AFCAAED56A4549F8974716428620F767F')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'5fda21db-c888-4d63-9890-2dca815d19c5', N'bbd629ba-d1c7-4d7e-b49f-5ffcc710af9d', N'976D38E50346B776F5648DA20066435A93D58E1FC1CAAEF8E200BEB6F2698AB6')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'5fda21db-c888-4d63-9890-2dca815d19c5', N'521ab5ef-abf2-4fd6-89bd-aebdfdba560c', N'1EB02987B4E92D1C7F3BF1A2688BBE74F0ED28C37B8CDC113E2DDB5139F1C2D5')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'5fda21db-c888-4d63-9890-2dca815d19c5', N'4293e0e8-4a85-486f-927c-c4999a8f4038', N'4063182267690DE3928402543B291E2CADF5EDA3C646D78DA95A7278F606557E')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'5fda21db-c888-4d63-9890-2dca815d19c5', N'57b792bb-5368-4fe3-baf8-e054d090a854', N'7F5C7AD52CD7FD4528A1C8BB2C91D9A11CEC25FAC28A9F342B1D511FFB69806D')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'5fda21db-c888-4d63-9890-2dca815d19c5', N'9f70629a-7d78-400b-bd94-f13f5f356e24', N'40C591637F347EAFCA1F348F9E50CC47D80126328F405A85A3F4A28976B7E692')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'998a4eae-4bc9-4078-803b-6e2f90d53f46', N'dacf376a-77cf-4970-ae9b-10c14a3187d3', N'A17598E0269D8A2861A8AEF17D2997A17586356F67C103531045090A1BF7DFBF')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'998a4eae-4bc9-4078-803b-6e2f90d53f46', N'5098638a-8697-4234-a955-359f29f0eefc', N'B6562BF388B0358D836381C94840E70BCC007C4E5E647CD8EE4D6E110BF54003')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'998a4eae-4bc9-4078-803b-6e2f90d53f46', N'dc52da02-4628-414f-9253-70c68f5f5476', N'43E3FA834C4E891834D7B46A82BB2E017AE8054112A4D3B4A0B60621923F9D66')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'998a4eae-4bc9-4078-803b-6e2f90d53f46', N'99652a20-ef01-4982-ab21-7d06fd1270de', N'433533F2C5B9800276FFB54C06E28A7B6ED528527BF151465E4BFC09BFB3F916')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'998a4eae-4bc9-4078-803b-6e2f90d53f46', N'4293e0e8-4a85-486f-927c-c4999a8f4038', N'93FEE9121547DBE95361765B410BE2FD79DA95A25A6E2D538D53DD99296EB429')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'6e01dd6e-5164-484c-b576-aa83bd26c1ac', N'f14efe30-5c10-4fb1-9c46-1e6a27ec8f5c', N'5BE1B9617FD60927088133ED0B6FEA0D911F38C0F14B382435E6E54AD05A949A')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'6e01dd6e-5164-484c-b576-aa83bd26c1ac', N'4293e0e8-4a85-486f-927c-c4999a8f4038', N'EBD4752E89B46D42E10504F0BA5C16EABB84CF8E35A58FFCC3C062BA1124E1CA')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'4293e0e8-4a85-486f-927c-c4999a8f4038', N'5ea3d44b-453d-454b-baf6-8f012fd3d8ac', N'6CE6A61079B79761800F1CC9F4157E4551778B8E22F07A054CC65438B79DC4BE')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'f53282e8-496e-413d-afa7-f04fe6ab3a26', N'b5a35e91-41d7-4a37-9fde-187c019a3caa', N'8046D3FF05B7312FD6B81173A7EA17FEC746AAF58367EC7B95EBE4DAF249E1A3')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'f53282e8-496e-413d-afa7-f04fe6ab3a26', N'cb8be7e5-9607-4906-9dc0-632254235014', N'0B8796BED58CD8E3CEE69C8BCC08F7C52570FDC5E34BFA62C488CF79C234EC36')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'f53282e8-496e-413d-afa7-f04fe6ab3a26', N'4293e0e8-4a85-486f-927c-c4999a8f4038', N'9FBE3715DE714378A63D256F44ABC4931BA2D896222E6578C8FB53F013002E96')
INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES (N'f53282e8-496e-413d-afa7-f04fe6ab3a26', N'543f4160-1340-412b-a31f-d3d35949aaff', N'1FA2CDE8A84A8D5CEB03F410C6BE99425BC6B081E3F1E9857E5F42439A70D98C')
GO
INSERT [dbo].[TipoDocumentoCompra] ([TipoDocumentoCompra_Id], [Descripcion], [Etapa], [DiasVigencia], [Orden], [DVH]) VALUES (N'7ff9addb-ca5c-4d98-bda9-1e27a7d05f65', N'Traffic Fines Clearance', 1, 30, 6, N'F01D6E0D9FC4F966BA8840584183DAAEEE5F9744C5F6E0AD3D9A145C49F92B98')
INSERT [dbo].[TipoDocumentoCompra] ([TipoDocumentoCompra_Id], [Descripcion], [Etapa], [DiasVigencia], [Orden], [DVH]) VALUES (N'3c39430e-2a6c-41ba-a193-5792a28296a2', N'Vehicle Title', 1, NULL, 1, N'7448CC840CD43CDF77AD2F71CA7535E8E5338FFECF78B4525DA369DE2DA1BCF2')
INSERT [dbo].[TipoDocumentoCompra] ([TipoDocumentoCompra_Id], [Descripcion], [Etapa], [DiasVigencia], [Orden], [DVH]) VALUES (N'f85bcccb-17b3-4924-bdb0-780f6f2671c8', N'Signed Form 08', 2, NULL, 8, N'2C2ED68E7E7E35355C485EB3B3A897A25E9604F196B4E2840B36463235E892A3')
INSERT [dbo].[TipoDocumentoCompra] ([TipoDocumentoCompra_Id], [Descripcion], [Etapa], [DiasVigencia], [Orden], [DVH]) VALUES (N'ba5b825c-dde1-4e73-b776-7bc368e31b21', N'Form 12', 1, 150, 2, N'9F77BD80F7B215B0D3D2F5520B1D913D921A881E4428A7779ABA69E5F1177C04')
INSERT [dbo].[TipoDocumentoCompra] ([TipoDocumentoCompra_Id], [Descripcion], [Etapa], [DiasVigencia], [Orden], [DVH]) VALUES (N'95abfc47-47a0-43b6-8e3d-7fc04179d142', N'Vehicle Registration Card', 1, NULL, 3, N'438BBDA3F3B9FA2A52DCAD0F7039291A749421D16F949981EAC95D07E8AA0085')
INSERT [dbo].[TipoDocumentoCompra] ([TipoDocumentoCompra_Id], [Descripcion], [Etapa], [DiasVigencia], [Orden], [DVH]) VALUES (N'74c65397-668a-4a32-89b0-9672959e16f8', N'Signed Purchase Contract', 1, NULL, 7, N'A117EA51BF219E9F5A62EF8FF8AED5351D1519E2DF4505B0AF94ADE699FC1610')
INSERT [dbo].[TipoDocumentoCompra] ([TipoDocumentoCompra_Id], [Descripcion], [Etapa], [DiasVigencia], [Orden], [DVH]) VALUES (N'9ffbca9e-eddb-4934-af37-a1a705749d84', N'Vehicle Tax Clearance', 1, 30, 5, N'BA0E1CF18D8B2C03374FD0EF004AFF6D49376A90EEAD91DBD6FE2ADBB61E13F5')
INSERT [dbo].[TipoDocumentoCompra] ([TipoDocumentoCompra_Id], [Descripcion], [Etapa], [DiasVigencia], [Orden], [DVH]) VALUES (N'9707d21e-4103-4f35-bc83-af87fcd0cac3', N'Ownership Certificate', 1, 15, 4, N'E62A3545B7C89D7441B434F7E13406B8E33EB42E30A9BF8AD3709621427796D8')
INSERT [dbo].[TipoDocumentoCompra] ([TipoDocumentoCompra_Id], [Descripcion], [Etapa], [DiasVigencia], [Orden], [DVH]) VALUES (N'bce2bf6f-b4c5-40a9-97c7-f6375f187e63', N'Municipal Deregistration', 2, NULL, 9, N'7892EEF4CCA21015FF5021E6D149BC6EC7BB0A280722E2CA9E859D2EDA23E46A')
GO
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionCancelled', N'Acquisitions', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionCreated', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionDocumentCreated', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionDocumentDeleted', N'Acquisitions', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionDocumentObserved', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionFinished', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionFormalized', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionPaymentCreated', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionRejected', N'Acquisitions', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionTransferred', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AcquisitionTransferScheduled', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AppraisalCreated', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AuditExported', N'Audit', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AuditPrintRequested', N'Audit', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'AuditViewed', N'Audit', 5)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'BackupCreated', N'Security', 2)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'FamilyCreated', N'Permissions', 1)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'FamilyDeleted', N'Permissions', 1)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'FamilyUpdated', N'Permissions', 1)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'InspectionRecorded', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'IntegrityRecalculated', N'Security', 1)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'InvoiceCreated', N'Billing', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'InvoiceDeleted', N'Billing', 2)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'InvoiceExported', N'Billing', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'InvoiceUpdated', N'Billing', 2)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'LanguageChanged', N'Users', 5)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'Login', N'Users', 5)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'LoginFailed', N'Users', 2)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'Logout', N'Users', 5)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'MakeCreated', N'Vehicles', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'MakeDeleted', N'Vehicles', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'ModelCreated', N'Vehicles', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'ModelDeleted', N'Vehicles', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PaperworkCreated', N'Management', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PaperworkDeleted', N'Management', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PaperworkFileCreated', N'Management', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PaperworkFileDeleted', N'Management', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PaperworkUpdated', N'Management', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PasswordChanged', N'Users', 2)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PaymentCreated', N'Billing', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PaymentDeleted', N'Billing', 2)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PermissionCreated', N'Permissions', 1)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PermissionDeleted', N'Permissions', 1)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PersonCreated', N'People', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'PurchaseReceiptIssued', N'Acquisitions', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'QuotationCreated', N'Sales', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'ReservationCreated', N'Sales', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'RestoreCompleted', N'Security', 1)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'RestoreFailed', N'Security', 1)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'RestoreStarted', N'Security', 2)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'UserBlocked', N'Users', 1)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'UserCreated', N'Users', 2)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'UserDeleted', N'Users', 2)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'UserUnblocked', N'Users', 1)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'UserUpdated', N'Users', 2)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'VehicleCreated', N'Vehicles', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'VehicleDeleted', N'Vehicles', 3)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'VehicleUpdated', N'Vehicles', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'VersionCreated', N'Vehicles', 4)
INSERT [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad]) VALUES (N'VersionDeleted', N'Vehicles', 3)
GO
INSERT [dbo].[TipoPago] ([TipoPago_Id], [Descripcion], [DVH]) VALUES (N'403a0a26-eb35-4597-bebc-3ae4ad968f22', N'PayPal', N'EC127265EF21316DF47B73DB249CE0D84EEF98CCAA19A336F1E53B190EF4BE04')
INSERT [dbo].[TipoPago] ([TipoPago_Id], [Descripcion], [DVH]) VALUES (N'39541ea7-2127-4d52-85f8-5568493fbe3e', N'Bank Transfer', N'F9E2C117E5A4497FCD0CB92D4316D795A4E81DAEDAC9B1A07E642A754B9C7B98')
INSERT [dbo].[TipoPago] ([TipoPago_Id], [Descripcion], [DVH]) VALUES (N'b3c64ce9-6581-4a1b-84ed-5c7d3b4f30cc', N'Check', N'D5A55192E5403F2F36C1A21077E5A758EA9338E8A58F50F2A3A8293A2D9DCA3C')
INSERT [dbo].[TipoPago] ([TipoPago_Id], [Descripcion], [DVH]) VALUES (N'5648da1e-ea17-40e7-989b-b1ab8bb052b5', N'Debit Card', N'0761D678C62489982F228F6CF39269F1950E73C63EF610DC4226F68FADB46025')
INSERT [dbo].[TipoPago] ([TipoPago_Id], [Descripcion], [DVH]) VALUES (N'3febb85f-a091-4b6a-916c-c470c2194999', N'Cryptocurrency', N'2480B0E0090179B2CA87ED81B6F58C4D8D3E4E3F43CD0F491EEC0DDD2BDF9985')
INSERT [dbo].[TipoPago] ([TipoPago_Id], [Descripcion], [DVH]) VALUES (N'62e87a3d-58d8-4de8-8645-de888daf61fd', N'Credit Card', N'2F219B80A8E76947314680433AE17EC3950D1A70423610111B633EC0507372A7')
INSERT [dbo].[TipoPago] ([TipoPago_Id], [Descripcion], [DVH]) VALUES (N'98e517b3-2a56-4e55-9f2d-fe818ef38d48', N'Cash', N'93AF8AE2107793BE68ECFE3D84378162ACEFEF6849BDFF957694D124D5052023')
GO
INSERT [dbo].[Usuario] ([Usuario_Id], [DNI], [NombreUsuario], [Clave], [Nombre], [Apellido], [Rol_Id], [Bloqueado], [Activo], [IntentosDisponibles], [CodigoIdioma], [DVH]) VALUES (N'93622f2b-302a-423e-bf08-8dc823c81257', 41978656, N'Admin', N'100000.DoZO08XkmuLnN/qyPp3Vzw==.x/YUgAtxKRpMZJpWe06v4xhCb3So/gSNC6r5BLlVVYE=', N'Lautaro', N'Biglieri', N'890db080-56b1-42fa-9f6d-2c3dd685ac3e', 0, 1, 3, N'es', N'9456ACF7DF86542393458052DE3F927285A915F8762DCE345E9570C7F3B06824')
GO
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'73479523-7a77-4f96-84eb-02d48f109c2a', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'f28720fa-7302-435f-9610-24aa9fca3cad', N'Base', N'5F0FBFC3323D1123E975753966686E694F8D31F783B8F54B6C1C848ECC923C69')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'9d77f5df-0ef6-4d86-a302-03117609a7c8', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'cc6a62d3-a149-4c9f-9299-84eaae001fb4', N'Base', N'B27FBA282119505EEA22042284B27FBA3EE42F68EC1874039CBD081991A8260D')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'e701eaa4-e08f-42db-a154-032a2267cc39', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'727d193e-e57d-48c5-b1a4-618d1fe56066', N'Base', N'1438FD5D919B4BEDC339ADF730E1AE4C1228295C3E9126323818A0650CD53877')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'a657a7bd-dfd7-4925-8c43-06f1616f231a', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'8b593860-b996-4bb1-99d4-1869bc68ab24', N'Base', N'DF3B3A3F2BD7B5F53E87A5425B8C1EBD394143418667D9D8372CFED827FBCE4B')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'10648b01-4392-41db-a405-080eb0e56125', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'6b3a36bc-1d9a-4430-b505-79d3ce909650', N'Base', N'283D9517BAA8796070B9D5F4570227D9CCAEE5773D86C0DFDD24F27C396FFFB5')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'1f777b71-335a-436a-86ef-091cb042b4fe', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'91bef4c2-e350-4321-a6de-deadb4d255ae', N'Base', N'AC1E7C5B1908DCE9B3EAB3561671D4F3BE2783C4416C5D8ECB78783F91525DC9')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'68da1848-8337-4596-a026-0df64f9ce41f', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'9688580d-d2ce-4922-87b6-78cd766d9d94', N'Base', N'53E9A8370C40F2FDB5A3EE9D14EE4F1DACACD0806C47E11EE3E76601A52AA929')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'5e3a312b-1284-4b78-865a-0e8f6376c0f8', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'235d2a3e-a3bc-44d9-9f06-29377fd514e4', N'Base', N'08BF8959BF414A56D097A729529D8B79812BCF6FA183EC2E90E0D5E18D950B0E')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'b55ecd98-9c82-47f4-8c6e-101a5482e4e1', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'3cf41628-d1e8-4803-92c2-ea5e522ed1ac', N'Base', N'376AEA34105691DA27E6CF38D4B70F8CC0E344F702C7C398D171646E5DD02480')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'4630636c-8192-4031-b0c0-114f6869c029', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'449fab3c-6db0-440e-a0c8-74b879d27b6b', N'Base', N'8660F70ED4B10A87CC1A2219E0C4B5C4B94B301BABAC2509D1950033D4E8E99D')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'2c9d55b4-b134-4d4d-9e11-15785d85003c', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'f8947c26-d57a-444f-9abc-7eeefd8e525a', N'Base', N'6F3870C0BB203CA3F06181FDF0BDB3860A56DA86E44348C53FDD82AF4CD61BEE')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'072b6672-daed-4294-a854-159e5c9b8fc7', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'6b340928-bcb2-4b0c-aeb7-915446fd6d8c', N'Base', N'4AF41896B12FAC0C1900B9E18AF539E684F26EEED5E957E3A2E3BB0B162454AA')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'1f0dbd94-a8bd-46e1-9fd9-15b75bc51045', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'69fbf34f-ae5b-42c3-bbc2-31feb83720ae', N'Base', N'70CD94E2D10271BB1FFFC34C0E55DBE9149FDE6790B1B2CD719DC4A6BEAB735A')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'41b3bfaf-2e45-4818-b56b-160fe46f9bc5', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'c4c68147-3eeb-4d92-aace-bd3c0b9386bd', N'Base', N'7D52EE109F6D3B670CC7AD544D5558E6B45F68F543D220F954DA1F4374699E3A')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'5448919d-a915-4723-8b6d-16adf267776e', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'503c5d33-0e2c-4ebc-8f6f-1d6039e6ba38', N'Base', N'1005B712C9DFF09D08735C51561DC0C0886C9CD6648F1A914BB02CC8D734CA83')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'9571fe55-78e5-4870-b911-218f2d33ebf0', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'6ceea7d4-eff6-4303-90b1-1f1c742e9025', N'Base', N'12E2CA321C7888FED6C22F5530D7FB0EFB61BA38673468FFA941CC950B8ADADB')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'd7fd493b-fc34-4a4d-8777-21fded75c9f4', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'2da2789b-bf6c-4d1c-8d12-e42670f437f4', N'Base', N'E393FC512772278D82F53BA617E24D82E75F9D2E107CF31D83F969F2A56B60C6')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'e69e561f-323a-4ece-8f57-22358445b3c8', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'b08672b3-eeae-4fe2-9541-5e78ce2f2456', N'Base', N'978B04474C62A24EE3314CC0F1C19EC4864327004B6B98E846D779AC384BDCBE')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'64a69a65-d524-478a-923d-22b44eb79655', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'54987e31-ab0d-44ae-b62e-46910b835122', N'Base', N'F6D23941E591D2D6729B6B7DA41E5C585BDE92C201D4F333FE27DFC271193AA8')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'5f0fed0a-6d38-4866-8ad8-25b19328aab6', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'82b40e37-f246-4da0-af32-fd6b8f955883', N'Base', N'80A4D0349C3FC12DF9A4195EF12C2FE8B91D1A6B2EE3A2449B29528BE3F7C9C5')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'58cb0fc1-8c58-447c-8481-26142da2ed90', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'9bf6a112-cc10-431a-9e84-df741745fa03', N'Base', N'DF878D94EF9ACF507EF0547B4F02372922521E559732F4A9D18B1396693376B5')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'c66c0c87-c381-425e-83ab-2a39a82f9310', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'd9528843-a358-4fe3-a55a-b729e1def003', N'Base', N'6BC6EA78C97D901CDEC7972B827A718A8D2DEBBA3EEFA269E5CC1336D47674F4')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'654fcc3c-c316-49ee-863f-2bb8f8cf5a70', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'0a1d4ba7-5811-4e56-b6cd-622e5c10b906', N'Base', N'101F59AA13EB79A0115C0ACAB6829EDC237B205469ACE35C427DA27145378E5A')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'd0032b07-4aaa-42e1-a31d-2e03c520f554', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'2675039a-f5e3-4140-9edd-2cc60438c1c3', N'Base', N'ACCDE56780CB7B011A60CF6B0724255606CFC8CB370D97D68EE7499AC821842E')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'12bb3589-3789-470c-b4f8-2fefac1ac2a3', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'9c31b675-614c-4d43-b8f4-80c5fc7a769d', N'Base', N'301218C4331576EC101D6C6BC9B3FE32A7D7F5DD9FF78CFF770CE310EB5A8502')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'011df7aa-6fc4-4bfe-8d68-318eb308ca52', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'05d00969-2520-4c27-ba48-0c9ad48051dc', N'Base', N'424DF7F503895385D114908064817A7556A1C1A987CDBEC2F9F43AF00AE48CA9')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'ab5227cd-a43b-4ed3-942d-31b77962bea3', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'78b92d89-e7ab-4fee-a52c-6fdcd7e07cd2', N'Base', N'A2480B258E4CCC0383C314DE19C5A8534C55EE0096105E18C41455DCD1124E4C')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'02c65e48-67ac-4f1b-9e3f-336e065ed512', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'765dffea-36c7-46be-a97a-c90e1b5fd266', N'Base', N'5D5C6F0F29517768BC488B04778B19DBC47E9F7111B0CBA0050B748B6623C303')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'e44a08da-f2c7-4150-ae61-367c36b55ae4', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'0b992189-e1ab-4d53-9309-81b686031b70', N'Base', N'562F4934F79072B6DCD7AD3DC80857771F6FF7833D537B66207DB38779A80B1F')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'19d340a7-1894-4ce2-9300-3693818f85a8', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'e4e95acc-33a1-433d-9a00-420bc92dfcf8', N'Base', N'F88FA41FE745B740EC2DD6E859E94354488ECFFAC866BED995B38FE2C5C37D40')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'd0c01485-3a62-447a-a6bc-37b7efedf17c', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'd682e006-e2ad-4076-bcb1-b754b34a5767', N'Base', N'47C3425EF75AABEB8F442E392203D1C4A2620298111B93DB17B93D27E6323077')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'dfe8ec2f-dbb8-4776-8849-3b3b4a4369d2', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'e819d539-ca21-4f1e-830f-ce8265c33075', N'Base', N'504A3847DF1DD62E014E2A95C24534F51204AE754644E6F2DA44873DC4917103')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'9de5bcdf-cb6b-41f0-af9a-3db35157a167', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'3829c802-7866-4d3d-8ee9-8bf8c43304e2', N'Base', N'3EFBC08A154737A3E60CA1980D1632DCC918E52F4C1BBF3A156664F38E18B6EF')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'ff8fb90a-6a84-4e9a-9800-3dc73ec0e07a', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'2e5c350b-f76f-4c32-84e7-23f165d2db5b', N'Base', N'62903622376E39582313348B624953038EAE363B5AE47B0ADEE5D914FC900A85')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'49c52439-fb6c-4e8c-ba3d-49176a975a68', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'6de1611c-faff-4310-a731-6ddea5e5fd99', N'Base', N'59CA957E39B6B514A2EB6D7414FEDBD49A50C22FF609CAD40166F5F80EEA360E')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'0fb9a4b3-6077-47ab-ba34-4b47f3eef73d', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'157cae27-a6d7-4076-ad4f-a1ed27b69f97', N'Base', N'7155749472D62332B31D18AD3CA976D5A787CD39F3A2C371B11E1FB9F5DDEE77')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'6dba4923-128d-448b-900c-4cbc24e6ef65', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'e3e7a037-a610-4859-ad7c-31f90354c593', N'Base', N'5B6E5ACCFA0C51C37EA39761CAD2DEF407B80DA70103F7D1F0420566144FB866')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'e5336910-8efd-4b2b-86b7-4db764dd9c74', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'4938dcf6-694d-4ce8-b04f-746aa52c30f9', N'Base', N'512C55B35C0CCCAFD0417F8141467E3B24431F8F9ADDE453A344B04F3445CF2F')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'53d01b6c-59b5-4ca6-b7b0-4dbdde7a29d8', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'e4841a65-435d-4fe3-839d-99eaacb7e455', N'Base', N'CB509AFA402710B6BFF9D9B6D26CD405A2B269BE2C30F471B6510029E47DEF41')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'7cbac8b8-1f2a-427d-98d5-542a2c59c627', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'1e56ad70-f2ed-4523-8ebc-023dc10d6612', N'Base', N'DEC7B2E02655E0D57DE18730650B2AE2E59B85DD960E27470DB52571E16389A3')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'bac8fc0c-d54e-4e1d-9276-59d0f202bca5', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'cf400d07-44ff-4fc2-be22-8f4713e94374', N'Base', N'84C57DFB7409F8076DC686BCE70F2C78864A32249F74D44FE1E6112A7E03AB71')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'aa3e469e-201e-4e08-994f-5a742cbc202d', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'938d07be-badc-4f5b-9dbc-2eff9d0dc6e4', N'Base', N'E4F843AFD8E6B02D202FC4DEE3EBB706B9DDAACDDD7B5C2369F9802A8527036B')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'2975531a-92a1-47b4-81f9-5d6cd9d4817d', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'97de57da-8f06-4730-bc4e-ddbfd68a7ad9', N'Base', N'09FC68401BB90B62CC3621AAF8EE3D421DF88F0DA2D29047B2062DB2269155CE')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'c311fd8d-bc67-4850-babc-5f4442717426', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'ebdb9042-35a7-4215-9143-997e14ca1d00', N'Base', N'FA0C3952FE51EF2F17B2BB30D24FA993353B8A1B30E32393178BE9F660B6654F')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'd91ba585-73b7-4f6d-87b4-62c52d2e5f8c', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'8a4f47ab-87c7-460a-bf59-3ccea013e3cc', N'Base', N'B7EDC2C34BCA891DB070F0858F2D954664F4186ED3C1B9413D42FE8C49FE36F8')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'56501cb4-5706-4c33-9765-6a94349e2ab3', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'82d40c31-c727-45e4-b289-ca26a7285a8a', N'Base', N'83C3ACC5922C6B6E725FF91E1941EAB68003C51A53C856B9F93B93EDCBCFC322')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'47b740cd-2223-4927-af5f-754f2fa50a21', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'0422cabb-b263-4661-81f0-ee3f07dd6dc0', N'Base', N'59C652B93F178CF3871AE0C86102FC46BA3D7DB4EAB67D26C28C01EFE9AD0171')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'dd8e1514-b38b-4837-a3d4-7aa68bdf68a9', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'b6c74818-0407-43f8-b4c9-058963acca06', N'Base', N'793FD9B659ABE79492714FBECE1007C84E7F04C7C50406E028C2FD295B6A32EC')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'0c8fd8a6-492b-4950-a54b-7b8b3c0b6b37', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'67973c30-1b5f-40be-9ad0-e213921f8268', N'Base', N'A077CB8C344D29A0BEAD7F733EB9D0FA3F21A67D7895ABE39F21F5FD65CFEED6')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'92dc5095-a1f4-4ff4-8686-7bac42e10fd1', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'14d1c39a-72e7-489d-bb6e-65baac07abb0', N'Base', N'3C11CFAE84E03901614FA14E158483840F5EB14CBAAB57DFD6BBE2FA7611079A')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'44d9f403-90a4-4676-8c4e-7cc0cbb6b053', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'157ec41c-526f-49bd-81ef-f967b7c757f4', N'Base', N'2B9374B59D0D79BBCFDEC22F102BB1B959D9612CFB2EFB3A3B6A6D9F536F8BFC')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'432e4527-8056-44b1-82dd-7f40337e37c6', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'e3a4456b-5a3d-48fa-b20e-ecfd59176900', N'Base', N'CB7F6E4D97B886A5FD9BAA1D26DC63E28597837DE17EAA4F7498D0B0D67A2823')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'ce604bd9-e5ab-419a-bdec-81d252ee4126', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'78604797-62a0-494f-855f-ad4d5a6be7f8', N'Base', N'5F7C2B6B079919B2990B576F2EEF2357D0DC4DDFAF8E1745E70DE487ECC00578')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'6368b1ac-8a3a-48cb-b727-82dd1331e01c', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'dc7468b0-4b0f-4e22-b5f7-c82d7b71174a', N'Base', N'FEE9EC03C5FFF108A489AE1CA0252F1085CAB2B47FD95A702F3E4667C912F229')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'7975d555-2f17-4e27-b870-88adadc8fad7', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'c49a3608-7243-4c70-bf41-635ed02467f1', N'Base', N'5A0E93D1BC0681B53423D5ECE571AFDE2FEBE485047A83167972E4C9F9D3B5D1')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'7111d8ff-d609-4b82-848b-924736137378', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'd4eab283-b909-4020-9f13-6584349e0ba8', N'Base', N'3E7FB74FFF74B6CD737106C68E13BB0CBAB21178694A9B6200904C8882421AB9')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'df8e151c-f9d6-46b3-87f4-93590a438604', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'c35dcb7c-339b-46ba-83ef-fdd1914fbdba', N'Base', N'30EAA356185B5900CFA3915A5F1098900E37437DADC1AD10B543747FBA2A3900')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'6c7da200-7df3-4504-810f-98c48a1f17a5', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'296784fa-afd3-4d46-bddf-cb823dd07479', N'Base', N'7FE9E15FF82031E24B195C13C4172A6EB053DD0CA1B34A0F78ED99724DD4C991')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'b7a02c20-5165-40de-87f4-9a5466a0ca89', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'a5c21623-f75b-45cf-9295-da26717c49ef', N'Base', N'FD95A617621006AB44809BED5F8A609D34976FACCF7005F9C567A1F870A0D8C4')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'09ff99c7-5b98-4e7c-b76a-9faa050cef8a', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'5bc08ba0-5961-4127-9d3e-0e853ea45f50', N'Base', N'7017A584D240A8176C2CABD41FCCFD1CA792607A80B54BDA2CBAC096C7349689')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'6d3d15d7-993b-44eb-a77d-a01e10165d62', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'aaed517b-7d76-4aa4-b03b-36d1412f34c5', N'Base', N'1D979ADC76C8EBA1355CB37275342CFC5242F6912325C778BFBBF6EC58213416')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'd74f0d47-d092-4239-a9ad-a1fba8e3a2e7', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'f8ea90df-197d-44a1-9218-d0b601930f5b', N'Base', N'86895D5183967B172FBF7E80AB55D056A5F353364B5D9FCF41D8A55B03883B87')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'ab861d91-7fd6-477c-bdc1-a538641c38da', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'1f35f1d4-b95f-47fb-ad6b-4029b75fc910', N'Base', N'BA4AF5EE142261548A68A09DEA4A140128D6BBE89E081FE09A2699FD6465B5CD')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'1f3f47d2-5a15-4262-8a44-a7371353269c', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'd1ef6111-7616-4f00-a011-7abacd3b2ceb', N'Base', N'DEAE7D65B0B11B6397F840CBB6359F527A5A51033B7425F01FCD0C5DB45F93B0')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'7aa07659-1fe0-4b94-a981-a7572a0d49a0', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'a7585ec1-e405-4354-a40d-926d0d12962e', N'Base', N'480A2C6D93E886E35746F7AE6E4C380394725EA36DEDC047EB7E294B8834B5C0')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'ed1951e2-7a69-410f-b228-aeeed71d8c8d', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'07e310a0-29de-473a-b103-15c09ef387ef', N'Base', N'61BC3C2898FAE5BA6939D986E09165E53502FEEC313E4570F9185DAF426F3472')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'447500d0-12af-473a-b544-b200f2af094d', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'569a0062-a236-44b8-bca1-86d818c32e4a', N'Base', N'20B02DA790E60D3930194499AF31A6A98FEC528165F614C11213B3E2502D460F')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'4e5241fd-dbac-4db2-983e-b336f6c8bd8a', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'83778495-dc19-480e-9eb5-90ec1a6f6434', N'Base', N'A8405512EEE4F45DB032EE6DB1904D720D102BD80FE6F08CE7DE15F7EDAF1072')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'41e242d1-8ba4-4704-b04e-b8a79bf79dba', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'6ca85dc6-17a5-4f5b-98e3-097979d694f9', N'Base', N'FA78D43F0DF1773EA29BBBB0CBF2AE6DFFCD3432813581F1B34C3E84B7313FF2')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'54aeab97-c3a4-461c-9b26-ba2820c54807', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'71012599-48d1-447c-b0a3-9e62631c51b9', N'Base', N'87D9EEEF5E434F5E0F49F6B454ACD2682B70054C2CB041CBB9E4C0CC891375A6')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'bc19fe71-5c44-42bf-8bca-bf4ec7540659', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'49d644d8-cca7-4a72-9117-524022f9f164', N'Base', N'CC0886066C60D07D6A0FB402647163485AE60A9B5EF5732F6E7D6511980BC74B')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'518852ca-a23f-4c1c-8a18-c36bc5b3dff5', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'517e92d9-b080-431a-bd5e-bad17ded0d7e', N'Base', N'20733B3EB8EE5E2E8827003C9BF7DED3CAF288026287762085683E90D9345F05')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'21f52f35-7137-451f-b770-c4f1ffb14882', N'08e821d5-e981-46b1-a862-659c6917ae4b', N'185fe2e0-73ed-4de4-866f-1d79ea7f77f9', N'Base', N'BED596AB650A9B7E876081B94A853C53B1F156B47D37314876FA4AD95794041A')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'd60b6cd6-f014-4eca-8adc-c6783862446f', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'a6127020-693e-4128-a6ae-14cb13e7504b', N'Base', N'7A82F61FD06200F64AE07E59106E7961F08D3D958390E32A058DC2E3AEB43B85')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'a2508fba-b934-4adf-8040-ca97a56dd783', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'097f426c-a33e-423f-bb47-1e0568b5795d', N'Base', N'DFD5187510C7D5A77F0C1873C7F99E87296989273216D0C6942746A2B7507315')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'961aca27-0f8d-478f-8fc0-cb2ed3f236d9', N'76a99ec2-0645-4551-a598-8e2c18f0c2b6', N'922253f4-2458-46e0-b036-0b76fdcab925', N'Base', N'E3F25106422F390DB98A532BA42AE69D92AD4229D83C887579328622A0CAEAEF')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'41ebaabf-53e8-451d-ad16-cb9ead3f3966', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'0291a9ac-e62e-42bd-b87d-d956113949e3', N'Base', N'C44C23E45A9314909CDDFA1A8F9183138D8E3D398634E54192AB109684D3A959')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'20a6a87d-b463-4e30-a3e6-d08e1aa4c7b5', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'02ffc7e6-5c03-4960-bd31-9ca6055c5683', N'Base', N'3E03252E5E7E81A375AF1E1FD09DF0DF416878112AC84273D15FDD2DF758B736')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'df85eff9-562f-4a97-851e-de66b0cb5817', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'9552c585-0337-4aae-a278-7268ddb0e860', N'Base', N'3E41B0119406A08BAC9C8ED53747CA53C7200BA97439666DA488796997902969')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'5828cc0d-38fd-44ac-ab87-e16ce5783f44', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'4c7ab539-80a8-4305-bc51-8951dbd57749', N'Base', N'612BB8ECD1F6DB9513D719B2C889344513C58BB24777AA838EA7B9D5C3D03C07')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'9b29685f-d6db-4801-af5b-e208811edeab', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'431cbef4-4298-4055-8e01-ce886d410c1e', N'Base', N'8346BEBCDF3BCB0FA20CC0AF8F306677D6410210BF1B54CDB1C4EF267E3089C3')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'4fdbf0eb-f34d-4592-8644-e7105888c578', N'9e201e35-f6d2-4a09-8805-55ff01ab2667', N'd24806ff-9cfe-4a4a-a983-90c2db6aebd1', N'Base', N'08C4DD4325E60006A4377C76F652EDB070D0C4FD020A026D9CB85F83CF37BF8D')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'15f2abad-cc4b-4f4c-8a69-e8a7bfa7b58c', N'867312f8-8204-4cde-b8d7-7a5becd7b92b', N'0401d533-5664-4a3c-9a61-fc8f23a09faf', N'Base', N'036CC636AB42B63DD76A3BE2D364A7CC7BA09CBBBABA97CD1AB1EB222BEC1FCE')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'7620bb64-28f8-45f2-b1c7-ec6304684efe', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'05c86100-3eb9-4211-9a88-969ce43f8a55', N'Base', N'71F3D80740B2F685065FABB0B6EFA8945018A69D67A33E9E28ACCF61B9646AB4')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'd64b7ea7-b6da-4426-a325-f38c6fae4446', N'93cd5781-9865-4030-bab7-c1c1f18d5b4f', N'18f4e9a1-4169-4927-822e-b5ecc7f7fcfe', N'Base', N'9BF5C65BF3455393339BBDE996BB4A3397FB39212340C5F3DA7FCBE3464B0477')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'995187b0-8698-4b89-be37-f47aa55e08c0', N'3e95b0ab-5d2d-47ad-88b2-9c50abafd680', N'ccd9967a-f6ec-4edd-a91d-84626e7b32f3', N'Base', N'27E7DFA437521D2F133F6FF02E4E1ABFA22D9F3CD300E96C347DA9BC1E198136')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'd367be6d-48ca-471d-937e-f592a60644bd', N'164f51ac-4d40-4902-9f1b-0a373ad2797d', N'94635a6e-5f93-4597-92de-a4e747f01cd8', N'Base', N'2F32A5BD801A48958E6CA101E53868848167D121F0803A9F0E39D7CFB2232DA5')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'1c28050e-9131-40c0-a1c7-ff87f48d8882', N'2fcd7237-a843-4ee7-b163-4d49ed3e22ec', N'bfa0c5f6-5cde-4b0c-a84e-770772225a9f', N'Base', N'5C93FAB93C98124FA6A30D1E7F21C836B5C1A2A6077B30CF2D41778408FF2393')
INSERT [dbo].[Version] ([Version_Id], [Marca_Id], [Modelo_Id], [Descripcion], [DVH]) VALUES (N'd3a4289d-d34f-4c75-b3d1-ffe4763f16c2', N'927d2d88-4c9f-4922-acdd-a3a696a87bf8', N'b697ac98-2a2c-47e2-afff-5d67d27216ac', N'Base', N'CE8FBF343276C92B93449EE52D1AF995DB760A6B2F818059D51198BF80ACE88C')
GO
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'BitacoraEventos', N'8BEF77D7CEF511FEE7EC563AED86043FEFA6B54F0FD2C699A3679FC267297604', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Color', N'F9FA66FD88164BB2A55A534F4E5E3C8206B4D00E7E23E6BB18D1737211FD100C', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Compra', N'279989EDBFBB87B5B04A45079C6DB00A06BA913730C5A7EE950A6392733365C4', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'ComprobanteCompra', N'44B45811C7753E61CC9221B6E06B45DDE083B1489632A4E1E44F6004B6AC4210', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Cotizacion', N'F830EE18F863E27C1A6C995CD910ABFDEF03730DF561C99BB0C9CB959EEBEE16', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'DocumentoCompra', N'FB1ED1C1255592C0E8226213FE75DBCD77934FBAFC9395FD1FF0DF0205559AE9', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Factura', N'954B4EA684AE955389948B2BFCF68607D35BB3F485C474B496F32E1F7D460410', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'FormaPagoCompra', N'B3F10E56081A9F66B130BF945E6AAFA6078CB9AD6CE1D36DDE6F93143F2F723E', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Inspeccion', N'61AEE064B8A5A8A987089DE89DD84C4D4D08A28883D752B521A3C5D9C6E33C98', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'ItemInspeccion', N'6A9BE70BAD57E52FDADAF848DD7B25A876988F9F30EAC024719E3D2A6AA6956A', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Marca', N'ED70EFA2B900B43B94CCDCAF8B7B1A92833F364F301832828B27A8BE1B500D20', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Modelo', N'D19A48D3D6D3D9016E1ED47A710AEF174E6BD9FEBC10A8E898F0C4F52C4141C0', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Pago', N'561FA204B4BF017F1ACFDA5269F964A68F1BE12E69E88F142393A0559A814152', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'PagoCompra', N'1D1EB19DD9DC8E8319EA8A5A654E36AAF489E4939A2559523637A830FE0265DA', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Permiso', N'78A16CC59B806E123CC3B5D28E928BF74CBE27E3E8108E979A4D658ECD673043', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Permiso_Permiso', N'0DA5549E08C69C68FF42E8A3C3E7E7A4C90955301AEB196A23027509A8D06784', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Persona', N'456D63D310D6FF37DA817DA1BE740FF7403A886E9745C52D010FACE8CCA6721A', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'PuntajeInspeccion', N'54AF2C8BDBDA4A25CC72425AFB9A83612E682C238B7B69492D0EAFD4E6E43F0D', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Reserva', N'20FFB6FD13A2D486566822C5233A5EDECD672C2D96E6BFD5EF133304BE281463', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Tasacion', N'C516003CB415C12A38202DF50EB31C4E89A31F8462FABB626753A146AF880728', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'TipoDocumentoCompra', N'FFC5E6FC42F231B51BCAFD818BBA71C437443528D2779CA35895F2260341796B', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'TipoPago', N'9453C540C897A510B93DED06FDC27031EC36393C75E0EB58D40ECDB973D8AA76', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Tramite', N'CCAF4E8CBD12F08A0AF0E3AB954E399FF6694EE615F9D1F8E7D477BBB83CF0F9', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Usuario', N'B2224F21F89CE93A6353353BF30C01A3A819AAE3F0A503FC383A18A84F25C9BF', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Vehiculo', N'7E6DCCFD8879A6E092BB907EF462C797AD1BC8517282FFA569F08CFF11FC2DD7', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'VehiculoProspecto', N'6FB1C7FEF0AA328602189E57BEE815EB2164EACE7BF054DCC1F0C60471D28148', 1)
INSERT [dbo].[DV] ([NombreEsquema], [NombreTabla], [DVV], [VersionAlgoritmo]) VALUES (N'dbo', N'Version', N'A5CE7719E045DADCFF298CC21648C3C96DD4B44CCE8412C3AFD467D1CE744B2A', 1)
GO
ALTER TABLE [dbo].[ArchivoDocumentoCompra]  WITH CHECK ADD  CONSTRAINT [FK_ArchivoDocumentoCompra_DocumentoCompra] FOREIGN KEY([DocumentoCompra_Id])
REFERENCES [dbo].[DocumentoCompra] ([DocumentoCompra_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArchivoDocumentoCompra] CHECK CONSTRAINT [FK_ArchivoDocumentoCompra_DocumentoCompra]
GO
ALTER TABLE [dbo].[ArchivoTramite]  WITH CHECK ADD  CONSTRAINT [FK_ArchivoTramite_Tramite] FOREIGN KEY([Tramite_Id])
REFERENCES [dbo].[Tramite] ([Tramite_Id])
GO
ALTER TABLE [dbo].[ArchivoTramite] CHECK CONSTRAINT [FK_ArchivoTramite_Tramite]
GO
ALTER TABLE [dbo].[BitacoraEventos]  WITH CHECK ADD  CONSTRAINT [FK_BitacoraEventos_TipoEvento] FOREIGN KEY([TipoEvento], [Modulo], [Criticidad])
REFERENCES [dbo].[TipoEvento] ([TipoEvento], [Modulo], [Criticidad])
GO
ALTER TABLE [dbo].[BitacoraEventos] CHECK CONSTRAINT [FK_BitacoraEventos_TipoEvento]
GO
ALTER TABLE [dbo].[BitacoraEventos]  WITH CHECK ADD  CONSTRAINT [FK_BitacoraEventos_Usuario] FOREIGN KEY([Usuario_Id])
REFERENCES [dbo].[Usuario] ([Usuario_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BitacoraEventos] CHECK CONSTRAINT [FK_BitacoraEventos_Usuario]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [FK_Compra_Persona] FOREIGN KEY([Persona_Id])
REFERENCES [dbo].[Persona] ([Persona_Id])
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [FK_Compra_Persona]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [FK_Compra_Tasacion] FOREIGN KEY([TasacionAceptada_Id])
REFERENCES [dbo].[Tasacion] ([Tasacion_Id])
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [FK_Compra_Tasacion]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [FK_Compra_Usuario] FOREIGN KEY([Vendedor_Id])
REFERENCES [dbo].[Usuario] ([Usuario_Id])
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [FK_Compra_Usuario]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [FK_Compra_Vehiculo] FOREIGN KEY([Vehiculo_Id])
REFERENCES [dbo].[Vehiculo] ([Vehiculo_Id])
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [FK_Compra_Vehiculo]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [FK_Compra_VehiculoProspecto] FOREIGN KEY([VehiculoProspecto_Id])
REFERENCES [dbo].[VehiculoProspecto] ([VehiculoProspecto_Id])
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [FK_Compra_VehiculoProspecto]
GO
ALTER TABLE [dbo].[ComprobanteCompra]  WITH CHECK ADD  CONSTRAINT [FK_ComprobanteCompra_Compra] FOREIGN KEY([Compra_Id])
REFERENCES [dbo].[Compra] ([Compra_Id])
GO
ALTER TABLE [dbo].[ComprobanteCompra] CHECK CONSTRAINT [FK_ComprobanteCompra_Compra]
GO
ALTER TABLE [dbo].[Cotizacion]  WITH CHECK ADD  CONSTRAINT [FK_Cotizacion_Persona] FOREIGN KEY([Persona_Id])
REFERENCES [dbo].[Persona] ([Persona_Id])
GO
ALTER TABLE [dbo].[Cotizacion] CHECK CONSTRAINT [FK_Cotizacion_Persona]
GO
ALTER TABLE [dbo].[Cotizacion]  WITH CHECK ADD  CONSTRAINT [FK_Cotizacion_Vehiculo] FOREIGN KEY([Vehiculo_Id])
REFERENCES [dbo].[Vehiculo] ([Vehiculo_Id])
GO
ALTER TABLE [dbo].[Cotizacion] CHECK CONSTRAINT [FK_Cotizacion_Vehiculo]
GO
ALTER TABLE [dbo].[DocumentoCompra]  WITH CHECK ADD  CONSTRAINT [FK_DocumentoCompra_Compra] FOREIGN KEY([Compra_Id])
REFERENCES [dbo].[Compra] ([Compra_Id])
GO
ALTER TABLE [dbo].[DocumentoCompra] CHECK CONSTRAINT [FK_DocumentoCompra_Compra]
GO
ALTER TABLE [dbo].[DocumentoCompra]  WITH CHECK ADD  CONSTRAINT [FK_DocumentoCompra_TipoDocumentoCompra] FOREIGN KEY([TipoDocumentoCompra_Id])
REFERENCES [dbo].[TipoDocumentoCompra] ([TipoDocumentoCompra_Id])
GO
ALTER TABLE [dbo].[DocumentoCompra] CHECK CONSTRAINT [FK_DocumentoCompra_TipoDocumentoCompra]
GO
ALTER TABLE [dbo].[DocumentoCompra]  WITH CHECK ADD  CONSTRAINT [FK_DocumentoCompra_Usuario] FOREIGN KEY([UsuarioCarga_Id])
REFERENCES [dbo].[Usuario] ([Usuario_Id])
GO
ALTER TABLE [dbo].[DocumentoCompra] CHECK CONSTRAINT [FK_DocumentoCompra_Usuario]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Persona] FOREIGN KEY([Persona_Id])
REFERENCES [dbo].[Persona] ([Persona_Id])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Persona]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Reserva] FOREIGN KEY([Reserva_Id])
REFERENCES [dbo].[Reserva] ([Reserva_Id])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Reserva]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Vehiculo] FOREIGN KEY([Vehiculo_Id])
REFERENCES [dbo].[Vehiculo] ([Vehiculo_Id])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Vehiculo]
GO
ALTER TABLE [dbo].[FormaPagoCompra]  WITH CHECK ADD  CONSTRAINT [FK_FormaPagoCompra_Compra] FOREIGN KEY([Compra_Id])
REFERENCES [dbo].[Compra] ([Compra_Id])
GO
ALTER TABLE [dbo].[FormaPagoCompra] CHECK CONSTRAINT [FK_FormaPagoCompra_Compra]
GO
ALTER TABLE [dbo].[FormaPagoCompra]  WITH CHECK ADD  CONSTRAINT [FK_FormaPagoCompra_TipoPago] FOREIGN KEY([TipoPago_Id])
REFERENCES [dbo].[TipoPago] ([TipoPago_Id])
GO
ALTER TABLE [dbo].[FormaPagoCompra] CHECK CONSTRAINT [FK_FormaPagoCompra_TipoPago]
GO
ALTER TABLE [dbo].[FotografiaInspeccion]  WITH CHECK ADD  CONSTRAINT [FK_FotografiaInspeccion_Inspeccion] FOREIGN KEY([Inspeccion_Id])
REFERENCES [dbo].[Inspeccion] ([Inspeccion_Id])
GO
ALTER TABLE [dbo].[FotografiaInspeccion] CHECK CONSTRAINT [FK_FotografiaInspeccion_Inspeccion]
GO
ALTER TABLE [dbo].[Inspeccion]  WITH CHECK ADD  CONSTRAINT [FK_Inspeccion_Compra] FOREIGN KEY([Compra_Id])
REFERENCES [dbo].[Compra] ([Compra_Id])
GO
ALTER TABLE [dbo].[Inspeccion] CHECK CONSTRAINT [FK_Inspeccion_Compra]
GO
ALTER TABLE [dbo].[Inspeccion]  WITH CHECK ADD  CONSTRAINT [FK_Inspeccion_Usuario] FOREIGN KEY([Mecanico_Id])
REFERENCES [dbo].[Usuario] ([Usuario_Id])
GO
ALTER TABLE [dbo].[Inspeccion] CHECK CONSTRAINT [FK_Inspeccion_Usuario]
GO
ALTER TABLE [dbo].[Modelo]  WITH CHECK ADD  CONSTRAINT [FK_Modelo_Marca] FOREIGN KEY([Marca_Id])
REFERENCES [dbo].[Marca] ([Marca_Id])
GO
ALTER TABLE [dbo].[Modelo] CHECK CONSTRAINT [FK_Modelo_Marca]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [FK_Pago_Factura] FOREIGN KEY([Factura_Id])
REFERENCES [dbo].[Factura] ([Factura_Id])
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [FK_Pago_Factura]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [FK_Pago_TipoPago] FOREIGN KEY([TipoPago_Id])
REFERENCES [dbo].[TipoPago] ([TipoPago_Id])
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [FK_Pago_TipoPago]
GO
ALTER TABLE [dbo].[PagoCompra]  WITH CHECK ADD  CONSTRAINT [FK_PagoCompra_FormaPagoCompra] FOREIGN KEY([Compra_Id], [TipoPago_Id])
REFERENCES [dbo].[FormaPagoCompra] ([Compra_Id], [TipoPago_Id])
GO
ALTER TABLE [dbo].[PagoCompra] CHECK CONSTRAINT [FK_PagoCompra_FormaPagoCompra]
GO
ALTER TABLE [dbo].[PagoCompra]  WITH CHECK ADD  CONSTRAINT [FK_PagoCompra_Usuario] FOREIGN KEY([Cajero_Id])
REFERENCES [dbo].[Usuario] ([Usuario_Id])
GO
ALTER TABLE [dbo].[PagoCompra] CHECK CONSTRAINT [FK_PagoCompra_Usuario]
GO
ALTER TABLE [dbo].[Permiso_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Permiso_Permiso_Hijo] FOREIGN KEY([Hijo_Id])
REFERENCES [dbo].[Permiso] ([Permiso_Id])
GO
ALTER TABLE [dbo].[Permiso_Permiso] CHECK CONSTRAINT [FK_Permiso_Permiso_Hijo]
GO
ALTER TABLE [dbo].[Permiso_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Permiso_Permiso_Padre] FOREIGN KEY([Padre_Id])
REFERENCES [dbo].[Permiso] ([Permiso_Id])
GO
ALTER TABLE [dbo].[Permiso_Permiso] CHECK CONSTRAINT [FK_Permiso_Permiso_Padre]
GO
ALTER TABLE [dbo].[PuntajeInspeccion]  WITH CHECK ADD  CONSTRAINT [FK_PuntajeInspeccion_Inspeccion] FOREIGN KEY([Inspeccion_Id])
REFERENCES [dbo].[Inspeccion] ([Inspeccion_Id])
GO
ALTER TABLE [dbo].[PuntajeInspeccion] CHECK CONSTRAINT [FK_PuntajeInspeccion_Inspeccion]
GO
ALTER TABLE [dbo].[PuntajeInspeccion]  WITH CHECK ADD  CONSTRAINT [FK_PuntajeInspeccion_ItemInspeccion] FOREIGN KEY([ItemInspeccion_Id])
REFERENCES [dbo].[ItemInspeccion] ([ItemInspeccion_Id])
GO
ALTER TABLE [dbo].[PuntajeInspeccion] CHECK CONSTRAINT [FK_PuntajeInspeccion_ItemInspeccion]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Persona] FOREIGN KEY([Persona_Id])
REFERENCES [dbo].[Persona] ([Persona_Id])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Persona]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Vehiculo] FOREIGN KEY([Vehiculo_Id])
REFERENCES [dbo].[Vehiculo] ([Vehiculo_Id])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Vehiculo]
GO
ALTER TABLE [dbo].[Tasacion]  WITH CHECK ADD  CONSTRAINT [FK_Tasacion_Compra] FOREIGN KEY([Compra_Id])
REFERENCES [dbo].[Compra] ([Compra_Id])
GO
ALTER TABLE [dbo].[Tasacion] CHECK CONSTRAINT [FK_Tasacion_Compra]
GO
ALTER TABLE [dbo].[Tasacion]  WITH CHECK ADD  CONSTRAINT [FK_Tasacion_Inspeccion] FOREIGN KEY([Inspeccion_Id])
REFERENCES [dbo].[Inspeccion] ([Inspeccion_Id])
GO
ALTER TABLE [dbo].[Tasacion] CHECK CONSTRAINT [FK_Tasacion_Inspeccion]
GO
ALTER TABLE [dbo].[Tasacion]  WITH CHECK ADD  CONSTRAINT [FK_Tasacion_Usuario] FOREIGN KEY([Tasador_Id])
REFERENCES [dbo].[Usuario] ([Usuario_Id])
GO
ALTER TABLE [dbo].[Tasacion] CHECK CONSTRAINT [FK_Tasacion_Usuario]
GO
ALTER TABLE [dbo].[Tramite]  WITH CHECK ADD  CONSTRAINT [FK_Tramite_Factura] FOREIGN KEY([Factura_Id])
REFERENCES [dbo].[Factura] ([Factura_Id])
GO
ALTER TABLE [dbo].[Tramite] CHECK CONSTRAINT [FK_Tramite_Factura]
GO
ALTER TABLE [dbo].[Tramite]  WITH CHECK ADD  CONSTRAINT [FK_Tramite_Persona] FOREIGN KEY([Persona_Id])
REFERENCES [dbo].[Persona] ([Persona_Id])
GO
ALTER TABLE [dbo].[Tramite] CHECK CONSTRAINT [FK_Tramite_Persona]
GO
ALTER TABLE [dbo].[Tramite]  WITH CHECK ADD  CONSTRAINT [FK_Tramite_Vehiculo] FOREIGN KEY([Vehiculo_Id])
REFERENCES [dbo].[Vehiculo] ([Vehiculo_Id])
GO
ALTER TABLE [dbo].[Tramite] CHECK CONSTRAINT [FK_Tramite_Vehiculo]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Permiso] FOREIGN KEY([Rol_Id])
REFERENCES [dbo].[Permiso] ([Permiso_Id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Permiso]
GO
ALTER TABLE [dbo].[Vehiculo]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculo_Color] FOREIGN KEY([Color_Id])
REFERENCES [dbo].[Color] ([Color_Id])
GO
ALTER TABLE [dbo].[Vehiculo] CHECK CONSTRAINT [FK_Vehiculo_Color]
GO
ALTER TABLE [dbo].[Vehiculo]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculo_Marca] FOREIGN KEY([Marca_Id])
REFERENCES [dbo].[Marca] ([Marca_Id])
GO
ALTER TABLE [dbo].[Vehiculo] CHECK CONSTRAINT [FK_Vehiculo_Marca]
GO
ALTER TABLE [dbo].[Vehiculo]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculo_Modelo] FOREIGN KEY([Modelo_Id])
REFERENCES [dbo].[Modelo] ([Modelo_Id])
GO
ALTER TABLE [dbo].[Vehiculo] CHECK CONSTRAINT [FK_Vehiculo_Modelo]
GO
ALTER TABLE [dbo].[Vehiculo]  WITH CHECK ADD  CONSTRAINT [FK_Vehiculo_Version] FOREIGN KEY([Version_Id])
REFERENCES [dbo].[Version] ([Version_Id])
GO
ALTER TABLE [dbo].[Vehiculo] CHECK CONSTRAINT [FK_Vehiculo_Version]
GO
ALTER TABLE [dbo].[VehiculoProspecto]  WITH CHECK ADD  CONSTRAINT [FK_VehiculoProspecto_Color] FOREIGN KEY([Color_Id])
REFERENCES [dbo].[Color] ([Color_Id])
GO
ALTER TABLE [dbo].[VehiculoProspecto] CHECK CONSTRAINT [FK_VehiculoProspecto_Color]
GO
ALTER TABLE [dbo].[VehiculoProspecto]  WITH CHECK ADD  CONSTRAINT [FK_VehiculoProspecto_Marca] FOREIGN KEY([Marca_Id])
REFERENCES [dbo].[Marca] ([Marca_Id])
GO
ALTER TABLE [dbo].[VehiculoProspecto] CHECK CONSTRAINT [FK_VehiculoProspecto_Marca]
GO
ALTER TABLE [dbo].[VehiculoProspecto]  WITH CHECK ADD  CONSTRAINT [FK_VehiculoProspecto_Modelo] FOREIGN KEY([Modelo_Id])
REFERENCES [dbo].[Modelo] ([Modelo_Id])
GO
ALTER TABLE [dbo].[VehiculoProspecto] CHECK CONSTRAINT [FK_VehiculoProspecto_Modelo]
GO
ALTER TABLE [dbo].[VehiculoProspecto]  WITH CHECK ADD  CONSTRAINT [FK_VehiculoProspecto_Version] FOREIGN KEY([Version_Id])
REFERENCES [dbo].[Version] ([Version_Id])
GO
ALTER TABLE [dbo].[VehiculoProspecto] CHECK CONSTRAINT [FK_VehiculoProspecto_Version]
GO
ALTER TABLE [dbo].[Version]  WITH CHECK ADD  CONSTRAINT [FK_Version_Marca] FOREIGN KEY([Marca_Id])
REFERENCES [dbo].[Marca] ([Marca_Id])
GO
ALTER TABLE [dbo].[Version] CHECK CONSTRAINT [FK_Version_Marca]
GO
ALTER TABLE [dbo].[Version]  WITH CHECK ADD  CONSTRAINT [FK_Version_Modelo] FOREIGN KEY([Modelo_Id])
REFERENCES [dbo].[Modelo] ([Modelo_Id])
GO
ALTER TABLE [dbo].[Version] CHECK CONSTRAINT [FK_Version_Modelo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoDocumentoCompra_Add @DocumentoCompra_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max) as

insert into ArchivoDocumentoCompra(DocumentoCompra_Id, NombreArchivo, Contenido)
values (@DocumentoCompra_Id, @NombreArchivo, @Contenido)

select 'success' as SQLResultType,'Acquisition document file added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoTramite_Add	@ArchivoTramite_Id uniqueidentifier, @Tramite_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max), @FechaCarga datetime as

insert into ArchivoTramite(ArchivoTramite_Id, Tramite_Id, NombreArchivo, Contenido, FechaCarga) 
values (@ArchivoTramite_Id, @Tramite_Id, @NombreArchivo, @Contenido, @FechaCarga)

select 'success' as SQLResultType,'Paperwork file added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoTramite_Delete	@ArchivoTramite_Id uniqueidentifier as

delete ArchivoTramite
where ArchivoTramite_Id = @ArchivoTramite_Id

select 'success' as SQLResultType,'Paperwork file deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.BitacoraCambios_Query
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
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Color_GetAll as

select Color_Id, Descripcion 
from Color 
order by Descripcion asc
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetNextNumber as

-- El numero entra en el DVH: se pide antes del insert y no puede salir de un IDENTITY.
select NEXT VALUE FOR dbo.CompraNumbers as Numero
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
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
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ComprobanteCompra_GetNextNumber as

-- Igual que el numero de adquisicion: entra en el DVH, asi que se pide antes del insert.
select NEXT VALUE FOR dbo.ComprobanteCompraNumbers as Numero
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Cotizacion_Add	@Cotizacion_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Precio float, @Fecha datetime , @DVH char(64) as

insert into Cotizacion(Cotizacion_Id, Vehiculo_Id, Persona_Id, Precio, Fecha,DVH)
values (@Cotizacion_Id, @Vehiculo_Id, @Persona_Id, @Precio, @Fecha,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc DocumentoCompra_Add	@DocumentoCompra_Id uniqueidentifier, @Compra_Id uniqueidentifier, @TipoDocumentoCompra_Id uniqueidentifier, @Estado int,
											@FechaEmision datetime, @Observacion varchar(max), @UsuarioCarga_Id uniqueidentifier, @FechaCarga datetime, @DVH char(64) as

insert into DocumentoCompra(DocumentoCompra_Id, Compra_Id, TipoDocumentoCompra_Id, Estado, FechaEmision, Observacion, UsuarioCarga_Id, FechaCarga, DVH)
values (@DocumentoCompra_Id, @Compra_Id, @TipoDocumentoCompra_Id, @Estado, @FechaEmision, @Observacion, @UsuarioCarga_Id, @FechaCarga, @DVH)

select 'success' as SQLResultType,'Acquisition document added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc DocumentoCompra_Delete @DocumentoCompra_Id uniqueidentifier as

-- El archivo escaneado se borra en cascada (AcquisitionDocumentFiles).
delete from DocumentoCompra
where DocumentoCompra_Id = @DocumentoCompra_Id

select 'success' as SQLResultType,'Acquisition document deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_Add	@Factura_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Reserva_Id uniqueidentifier, @Detalle varchar(max),
									@CuilCuit varchar(max), @RazonSocial varchar(max), @Valor float, @EstadoPago bit, @Fecha datetime , @DVH char(64) as

insert into Factura(Factura_Id, Vehiculo_Id, Persona_Id, Reserva_Id, Detalle, CuilCuit, RazonSocial, Valor, EstadoPago, Fecha,DVH)
values (@Factura_Id, @Vehiculo_Id, @Persona_Id, @Reserva_Id, @Detalle, @CuilCuit, @RazonSocial, @Valor, @EstadoPago, @Fecha,@DVH)

select 'success' as SQLResultType,'Invoice added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_Delete	@Factura_Id uniqueidentifier as

delete Factura
where Factura_Id = @Factura_Id

select 'success' as SQLResultType,'Invoice deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FormaPagoCompra_Add	@Compra_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Monto float, @DVH char(64) as

insert into FormaPagoCompra(Compra_Id, TipoPago_Id, Monto, DVH)
values (@Compra_Id, @TipoPago_Id, @Monto, @DVH)

select 'success' as SQLResultType,'Payment term added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FormaPagoCompra_DeleteByAcquisition @Compra_Id uniqueidentifier as

-- La forma de pago se reemplaza completa mientras el contrato no este firmado.
delete from FormaPagoCompra
where Compra_Id = @Compra_Id

select 'success' as SQLResultType,'Payment terms deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FotografiaInspeccion_Add	@FotografiaInspeccion_Id uniqueidentifier, @Inspeccion_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max), @FechaCarga datetime as

insert into FotografiaInspeccion(FotografiaInspeccion_Id, Inspeccion_Id, NombreArchivo, Contenido, FechaCarga)
values (@FotografiaInspeccion_Id, @Inspeccion_Id, @NombreArchivo, @Contenido, @FechaCarga)

select 'success' as SQLResultType,'Inspection photo added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FotografiaInspeccion_Delete @FotografiaInspeccion_Id uniqueidentifier as

delete from FotografiaInspeccion
where FotografiaInspeccion_Id = @FotografiaInspeccion_Id

select 'success' as SQLResultType,'Inspection photo deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_Add	@Inspeccion_Id uniqueidentifier, @Compra_Id uniqueidentifier, @Mecanico_Id uniqueidentifier, @FechaProgramada datetime,
									@FechaRealizada datetime, @KilometrajeVerificado int, @Resultado int, @Observaciones varchar(max), @DVH char(64) as

insert into Inspeccion(Inspeccion_Id, Compra_Id, Mecanico_Id, FechaProgramada, FechaRealizada, KilometrajeVerificado, Resultado, Observaciones, DVH)
values (@Inspeccion_Id, @Compra_Id, @Mecanico_Id, @FechaProgramada, @FechaRealizada, @KilometrajeVerificado, @Resultado, @Observaciones, @DVH)

select 'success' as SQLResultType,'Inspection added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
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
GO
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
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Marca_Delete	@Marca_Id uniqueidentifier as

delete Marca
where Marca_Id = @Marca_Id

select 'success' as SQLResultType,'Make deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Marca_GetAll as

select Marca_Id, Descripcion 
from Marca 
order by Descripcion asc
GO
SET ANSI_NULLS ON
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Modelo_Delete	@Modelo_Id uniqueidentifier as

delete Modelo
where Modelo_Id = @Modelo_Id

select 'success' as SQLResultType,'Model deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Pago_Add	@Pago_Id uniqueidentifier, @Factura_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Monto float, @Detalle varchar(255) , @DVH char(64) as

insert into Pago(Pago_Id, Factura_Id, TipoPago_Id, Monto, Detalle,DVH)
values (@Pago_Id, @Factura_Id, @TipoPago_Id, @Monto, @Detalle,@DVH)

select 'success' as SQLResultType,'Payment added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Pago_Delete	@Pago_Id uniqueidentifier as

delete Pago
where Pago_Id = @Pago_Id

select 'success' as SQLResultType,'Payment deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_DeleteFamily @Permiso_Id uniqueidentifier as

delete from Permiso_Permiso where Padre_Id=@Permiso_Id;
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetAllByUser @User_Id uniqueidentifier as

select p.* from Users_Permissions up inner join Permiso p on up.Permission_Id=p.Permiso_Id where up.User_Id=@User_Id
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetFamilies as

select	* 
from	Permiso p 
where	p.Tipo is  null
order by p.Nombre asc;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetPatents  as

select	* 
from	Permiso p 
where	p.Tipo is not null
order by p.Nombre asc;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_Insert_Permission_Permission @Padre_Id uniqueidentifier, @Hijo_Id uniqueidentifier , @DVH char(64) as

insert into Permiso_Permiso(Padre_Id,Hijo_Id,DVH) values (@Padre_Id,@Hijo_Id,@DVH);
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_InsertComponent @Permiso_Id uniqueidentifier, @Nombre varchar(50), @Tipo varchar(50) , @DVH char(64) as

insert into Permiso(Permiso_Id,Nombre,Tipo,DVH) values (@Permiso_Id,@Nombre,@Tipo,@DVH);
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Persona_Add	@Persona_Id uniqueidentifier, @DNI int, @Nombre varchar(max), @Apellido varchar(max), @Direccion varchar(max),
									@TelefonoPersonal varchar(max), @TelefonoCasa varchar(max), @Email varchar(max), @FechaNacimiento datetime , @DVH char(64) as

insert into Persona(Persona_Id,DNI,Nombre,Apellido,Direccion,TelefonoPersonal,TelefonoCasa,Email,FechaNacimiento,DVH)
values (@Persona_Id,@DNI,@Nombre,@Apellido,@Direccion,@TelefonoPersonal,@TelefonoCasa,@Email,@FechaNacimiento,@DVH)

select 'success' as SQLResultType,'Person added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Persona_GetByDni @DNI int as

select	u.Persona_Id,u.DNI,u.Nombre,u.Apellido,u.Direccion,u.TelefonoPersonal,u.TelefonoCasa,u.Email,u.FechaNacimiento
from	Persona  u
where	u.DNI = @DNI
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Persona_GetById @Persona_Id uniqueidentifier as

select	Persona_Id,DNI,Nombre,Apellido,Direccion,TelefonoPersonal,TelefonoCasa,Email,FechaNacimiento
from	Persona  
where	Persona_Id = @Persona_Id
GO
SET ANSI_NULLS ON
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.Reporte_HechosCompras
    @Desde datetime, @Hasta datetime
AS
BEGIN
    SET NOCOUNT ON;
    -- Compras (PN2) iniciadas antes de @Hasta: las del periodo [@Desde, @Hasta) y las que siguen abiertas aunque sean anteriores.
    -- Una fila por compra con la fecha en que llego a cada etapa. No calcula nada: la etapa a cada fecha de corte,
    -- las tasas y la proyeccion las resuelve ProyeccionComprasCalculator.
    -- La compra no guarda el historial de estados, asi que cada etapa se fecha con el registro que la produjo:
    --   Inspeccionada   = inspeccion realizada       Tasada        = primera tasacion
    --   Formalizada     = firma del contrato         Doc. completa = ultimo documento de la etapa B cargado
    --   Pagada          = comprobante de compra      Transferida   = ultimo documento de la etapa C (o la cita)
    --   Finalizada      = cierre
    -- Nombre y apellido salen encriptados: los desencripta el mapper.
    SELECT a.Compra_Id, a.Numero, a.Estado, a.MotivoRechazo, a.Fecha,
        i.FechaRealizada AS FechaInspeccion,
        t.PrimeraFecha AS FechaTasacion, t.UltimaFecha AS FechaUltimaTasacion, t.UltimoValor AS ValorTasacion,
        a.PrecioAcordado, a.FechaFirmaContrato,
        d.FechaEtapaB AS FechaDocumentacion,
        cc.Fecha AS FechaPago,
        ISNULL(d.FechaEtapaC, a.FechaCitaTransferencia) AS FechaTransferencia,
        a.FechaCierre,
        ISNULL(p.MontoPagado, 0) AS MontoPagado,
        f.Descripcion AS Marca,
        (CAST(b.Anio AS varchar) + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + e.Descripcion + ' ' + b.Patente) AS Vehiculo_Descripcion,
        g.Nombre AS Persona_Nombre, g.Apellido AS Persona_Apellido, g.DNI AS Persona_DNI
    FROM Compra a
        JOIN VehiculoProspecto b ON (b.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        JOIN Modelo c ON (c.Modelo_Id = b.Modelo_Id)
        JOIN Version e ON (e.Version_Id = b.Version_Id)
        JOIN Marca f ON (f.Marca_Id = b.Marca_Id)
        JOIN Persona g ON (g.Persona_Id = a.Persona_Id)
        LEFT JOIN Inspeccion i ON (i.Compra_Id = a.Compra_Id)
        LEFT JOIN ComprobanteCompra cc ON (cc.Compra_Id = a.Compra_Id)
        OUTER APPLY (
            SELECT MIN(x.Fecha) AS PrimeraFecha, MAX(x.Fecha) AS UltimaFecha,
                (SELECT TOP 1 y.Valor FROM Tasacion y WHERE y.Compra_Id = a.Compra_Id ORDER BY y.Fecha DESC) AS UltimoValor
            FROM Tasacion x
            WHERE x.Compra_Id = a.Compra_Id
        ) t
        OUTER APPLY (
            SELECT MAX(CASE WHEN y.Etapa = 1 THEN x.FechaCarga END) AS FechaEtapaB,
                MAX(CASE WHEN y.Etapa = 2 THEN x.FechaCarga END) AS FechaEtapaC
            FROM DocumentoCompra x
                JOIN TipoDocumentoCompra y ON (y.TipoDocumentoCompra_Id = x.TipoDocumentoCompra_Id)
            WHERE x.Compra_Id = a.Compra_Id
        ) d
        OUTER APPLY (
            SELECT SUM(x.Monto) AS MontoPagado FROM PagoCompra x WHERE x.Compra_Id = a.Compra_Id
        ) p
    WHERE a.Fecha < @Hasta AND (a.Fecha >= @Desde OR a.Estado < 8)
    ORDER BY a.Fecha;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.Reporte_HechosVentas
    @Desde datetime, @Hasta datetime
AS
BEGIN
    SET NOCOUNT ON;
    -- Hechos del embudo comercial (PN1) en el periodo [@Desde, @Hasta): cotizaciones, reservas y facturas.
    -- No calcula nada: las tasas, la vigencia y la proyeccion las resuelve ProyeccionVentasCalculator,
    -- asi la misma carga sirve para proyectar desde cualquier fecha de corte (y validar el modelo hacia atras).
    -- Tipo: 1 = Cotizacion, 2 = Reserva, 3 = Factura. Nombre y apellido salen encriptados: los desencripta el mapper.
    -- Solo cuentan las facturas pagadas (EstadoPago = 1), igual que el inventario: la factura nace impaga y si no se paga no hay venta.
    SELECT 1 AS Tipo, a.Cotizacion_Id AS Id, a.Fecha, CAST(NULL AS datetime) AS FechaVencimiento,
        a.Persona_Id, a.Vehiculo_Id, CAST(NULL AS uniqueidentifier) AS Reserva_Id, ISNULL(a.Precio, 0) AS Precio,
        f.Descripcion AS Marca,
        (CAST(b.Anio AS varchar) + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) AS Vehiculo_Descripcion,
        g.Nombre AS Persona_Nombre, g.Apellido AS Persona_Apellido, g.DNI AS Persona_DNI
    FROM Cotizacion a
        JOIN Vehiculo b ON (b.Vehiculo_Id = a.Vehiculo_Id)
        JOIN Modelo c ON (c.Modelo_Id = b.Modelo_Id)
        JOIN Version d ON (d.Version_Id = b.Version_Id)
        JOIN Marca f ON (f.Marca_Id = b.Marca_Id)
        LEFT JOIN Persona g ON (g.Persona_Id = a.Persona_Id)
    WHERE a.Fecha >= @Desde AND a.Fecha < @Hasta

    UNION ALL

    SELECT 2, a.Reserva_Id, a.Fecha, a.FechaVencimiento,
        a.Persona_Id, a.Vehiculo_Id, a.Reserva_Id, a.Precio,
        f.Descripcion,
        (CAST(b.Anio AS varchar) + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente),
        g.Nombre, g.Apellido, g.DNI
    FROM Reserva a
        JOIN Vehiculo b ON (b.Vehiculo_Id = a.Vehiculo_Id)
        JOIN Modelo c ON (c.Modelo_Id = b.Modelo_Id)
        JOIN Version d ON (d.Version_Id = b.Version_Id)
        JOIN Marca f ON (f.Marca_Id = b.Marca_Id)
        LEFT JOIN Persona g ON (g.Persona_Id = a.Persona_Id)
    WHERE a.Fecha >= @Desde AND a.Fecha < @Hasta

    UNION ALL

    SELECT 3, a.Factura_Id, a.Fecha, NULL,
        a.Persona_Id, a.Vehiculo_Id, a.Reserva_Id, a.Valor,
        f.Descripcion,
        (CAST(b.Anio AS varchar) + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente),
        g.Nombre, g.Apellido, g.DNI
    FROM Factura a
        JOIN Vehiculo b ON (b.Vehiculo_Id = a.Vehiculo_Id)
        JOIN Modelo c ON (c.Modelo_Id = b.Modelo_Id)
        JOIN Version d ON (d.Version_Id = b.Version_Id)
        JOIN Marca f ON (f.Marca_Id = b.Marca_Id)
        LEFT JOIN Persona g ON (g.Persona_Id = a.Persona_Id)
    WHERE a.Fecha >= @Desde AND a.Fecha < @Hasta AND a.EstadoPago = 1

    ORDER BY Fecha;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Reserva_Add	@Reserva_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Precio float, @Fecha datetime, @FechaVencimiento datetime , @DVH char(64) as

insert into Reserva(Reserva_Id, Vehiculo_Id, Persona_Id, Precio, Fecha,FechaVencimiento,DVH)
values (@Reserva_Id, @Vehiculo_Id, @Persona_Id, @Precio, @Fecha,@FechaVencimiento,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tasacion_Add	@Tasacion_Id uniqueidentifier, @Compra_Id uniqueidentifier, @Inspeccion_Id uniqueidentifier, @Tasador_Id uniqueidentifier,
									@Valor float, @Justificacion varchar(max), @Fecha datetime, @Vigencia datetime, @DVH char(64) as

insert into Tasacion(Tasacion_Id, Compra_Id, Inspeccion_Id, Tasador_Id, Valor, Justificacion, Fecha, Vigencia, DVH)
values (@Tasacion_Id, @Compra_Id, @Inspeccion_Id, @Tasador_Id, @Valor, @Justificacion, @Fecha, @Vigencia, @DVH)

select 'success' as SQLResultType,'Appraisal added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc TipoPago_GetAll as

select TipoPago_Id, Descripcion 
from TipoPago 
order by Descripcion asc
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_Add	@Tramite_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Factura_Id uniqueidentifier, @CodigoPrecarga08 varchar(50),
									@FechaTransferencia datetime, @Observaciones varchar(max), @Finalizado bit , @DVH char(64) as

insert into Tramite(Tramite_Id, Vehiculo_Id, Persona_Id, Factura_Id, CodigoPrecarga08, FechaTransferencia, Observaciones, Finalizado,DVH)
values (@Tramite_Id, @Vehiculo_Id, @Persona_Id, @Factura_Id, @CodigoPrecarga08, @FechaTransferencia, @Observaciones, @Finalizado,@DVH)

select 'success' as SQLResultType,'Paperwork added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_Delete	@Tramite_Id uniqueidentifier as

delete Tramite
where Tramite_Id = @Tramite_Id

select 'success' as SQLResultType,'Paperwork deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_Add	@Usuario_Id uniqueidentifier, @DNI int, @NombreUsuario varchar(50), @Clave varchar(max), @Nombre nvarchar(max), @Apellido nvarchar(max),
											@Rol_Id uniqueidentifier, @Bloqueado bit, @Activo bit, @IntentosDisponibles int, @CodigoIdioma varchar(2) , @DVH char(64) as

insert into Usuario(Usuario_Id,DNI,NombreUsuario,Clave,Nombre,Apellido,Rol_Id,Bloqueado,Activo,IntentosDisponibles,CodigoIdioma,DVH)
values (@Usuario_Id,@DNI,@NombreUsuario,@Clave,@Nombre,@Apellido,@Rol_Id,@Bloqueado,@Activo,@IntentosDisponibles,@CodigoIdioma,@DVH)

select 'success' as SQLResultType,'User added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_Delete	@Usuario_Id uniqueidentifier as

delete Usuario
where Usuario_Id = @Usuario_Id

select 'success' as SQLResultType,'User deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_DeleteAllPermissions @Usuario_Id varchar(50) as

delete from Users_Permissions where User_id=@Usuario_Id;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetAll as

select	u.Usuario_Id,u.NombreUsuario
from	Usuario u 
order by u.NombreUsuario asc	
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetAllByState @Activo bit as

select	u.Usuario_Id, u.DNI, u.NombreUsuario, Clave='' , u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	(@Activo = 1 and u.Activo = @Activo) or @Activo = 0 --Si active es 0 muestro todos los usuarios. 
order by u.NombreUsuario asc	
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetUserByDni @DNI int as

select	u.Usuario_Id, u.DNI, u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.DNI = @DNI
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetUserById @Usuario_Id uniqueidentifier as

select	u.Usuario_Id, u.DNI, u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.Usuario_Id = @Usuario_Id
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetUserByUsername @NombreUsuario varchar(50) as

select	u.Usuario_Id, u.DNI,u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.NombreUsuario = @NombreUsuario
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_InsertPermission @User_Id UniqueIdentifier, @Permission_Id UniqueIdentifier as

insert into Users_Permissions(User_id,Permission_id) values (@User_Id,@Permission_Id)
GO
SET ANSI_NULLS ON
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_Add	@Vehiculo_Id uniqueidentifier, @Patente varchar(255), @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier, @Version_Id uniqueidentifier, @Color_Id uniqueidentifier,
									@Precio float, @Opcionales varchar(255), @Observaciones varchar(max), @Puertas int, @Anio int, @Kilometraje int, @LinkImagen nvarchar(max) , @DVH char(64) as

insert into Vehiculo(Vehiculo_Id,Patente,Marca_Id,Modelo_Id,Version_Id,Color_Id,Precio,Opcionales,Observaciones,Puertas,Anio,Kilometraje,LinkImagen,DVH)
values (@Vehiculo_Id,@Patente,@Marca_Id,@Modelo_Id,@Version_Id,@Color_Id,@Precio,@Opcionales,@Observaciones,@Puertas,@Anio,@Kilometraje,@LinkImagen,@DVH)

select 'success' as SQLResultType,'Vehicle added succesfully!' as message
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_Delete	@Vehiculo_Id uniqueidentifier as

delete Vehiculo
where Vehiculo_Id = @Vehiculo_Id

select 'success' as SQLResultType,'Vehicle deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Version_Delete	@Version_Id uniqueidentifier as

delete Version
where Version_Id = @Version_Id

select 'success' as SQLResultType,'Version deleted succesfully!' as message
GO
SET ANSI_NULLS ON
GO
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
GO
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
GO
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
GO
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
GO
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
GO
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
GO
SET QUOTED_IDENTIFIER ON
GO
-- Venta: la venta se confirma cuando el tramite pasa a finalizado (CUF05).
CREATE   TRIGGER [dbo].[TR_Tramite_Venta] ON [dbo].[Tramite] AFTER INSERT, UPDATE AS
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
ALTER TABLE [dbo].[Tramite] ENABLE TRIGGER [TR_Tramite_Venta]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Alta: cualquier insert en Vehiculo (Compra_Finish de CUF10 o el ABM de vehiculos).
CREATE   TRIGGER [dbo].[TR_Vehiculo_Alta] ON [dbo].[Vehiculo] AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Usuario_Id uniqueidentifier = TRY_CAST(SESSION_CONTEXT(N'Usuario_Id') AS uniqueidentifier);

    INSERT INTO dbo.BitacoraCambios (Vehiculo_Id, Patente, Operacion, Usuario_Id)
    SELECT i.Vehiculo_Id, i.Patente, 1, @Usuario_Id
    FROM inserted i;
END
GO
ALTER TABLE [dbo].[Vehiculo] ENABLE TRIGGER [TR_Vehiculo_Alta]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[TR_Vehiculo_Modificacion] ON [dbo].[Vehiculo] AFTER UPDATE AS
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
ALTER TABLE [dbo].[Vehiculo] ENABLE TRIGGER [TR_Vehiculo_Modificacion]
GO
