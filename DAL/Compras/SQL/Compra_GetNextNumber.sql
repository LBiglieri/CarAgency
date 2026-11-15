CREATE or ALTER proc Compra_GetNextNumber as

-- El numero entra en el DVH: se pide antes del insert y no puede salir de un IDENTITY.
select NEXT VALUE FOR dbo.CompraNumbers as Numero

go
