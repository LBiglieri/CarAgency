CREATE OR ALTER PROCEDURE dbo.DV_ValidateTable @TableName nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    IF @TableName IS NULL OR @TableName COLLATE Latin1_General_100_BIN2 NOT IN
        ('Clients','Colours','Invoice','Makes','Models','Paperwork','Payments','PaymentTypes',
         'Permission_Permission','Permissions','Quotations','Reservation','Users','Vehicles','Versions')
        THROW 51003, 'Tabla fuera del alcance de digitos verificadores.', 1;
END
GO
