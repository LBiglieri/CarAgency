-- Reportes inteligentes: permisos de las pantallas.
-- Los procedimientos que leen los datos estan en DAL\Reportes\SQL (Reporte_HechosVentas.sql y Reporte_HechosCompras.sql) y se ejecutan aparte con sqlcmd -I.
-- Las filas se insertan con el DVH pendiente (64 ceros): despues hay que sellarlas y rehacer el DVV
-- de Permiso y Permiso_Permiso, igual que con la bitacora de cambios.
SET QUOTED_IDENTIFIER ON
GO

BEGIN TRAN

DECLARE @PendingDvh char(64) = REPLICATE('0', 64)

-- PN1: proyeccion de ventas. Tipo es el nombre del formulario. La recibe Admin.
IF NOT EXISTS (SELECT * FROM dbo.Permiso WHERE Permiso_Id = '445d3d41-2082-485d-8738-308d1563ac74')
    INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES
        ('445d3d41-2082-485d-8738-308d1563ac74', 'Proyeccion de ventas', 'ProyeccionVentasForm', @PendingDvh)

IF NOT EXISTS (SELECT * FROM dbo.Permiso_Permiso WHERE Padre_Id = '890db080-56b1-42fa-9f6d-2c3dd685ac3e'
                                                  AND Hijo_Id = '445d3d41-2082-485d-8738-308d1563ac74')
    INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES
        ('890db080-56b1-42fa-9f6d-2c3dd685ac3e', '445d3d41-2082-485d-8738-308d1563ac74', @PendingDvh) -- Admin

-- PN2: proyeccion de compras. Tipo es el nombre del formulario. La recibe Admin.
IF NOT EXISTS (SELECT * FROM dbo.Permiso WHERE Permiso_Id = '8269844e-36db-46e3-8d66-bcdc3514639c')
    INSERT [dbo].[Permiso] ([Permiso_Id], [Nombre], [Tipo], [DVH]) VALUES
        ('8269844e-36db-46e3-8d66-bcdc3514639c', 'Proyeccion de compras', 'ProyeccionComprasForm', @PendingDvh)

IF NOT EXISTS (SELECT * FROM dbo.Permiso_Permiso WHERE Padre_Id = '890db080-56b1-42fa-9f6d-2c3dd685ac3e'
                                                  AND Hijo_Id = '8269844e-36db-46e3-8d66-bcdc3514639c')
    INSERT [dbo].[Permiso_Permiso] ([Padre_Id], [Hijo_Id], [DVH]) VALUES
        ('890db080-56b1-42fa-9f6d-2c3dd685ac3e', '8269844e-36db-46e3-8d66-bcdc3514639c', @PendingDvh) -- Admin

COMMIT
GO
