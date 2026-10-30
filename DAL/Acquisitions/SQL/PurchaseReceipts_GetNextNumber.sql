CREATE or ALTER proc PurchaseReceipts_GetNextNumber as

-- Igual que el numero de adquisicion: entra en el DVH, asi que se pide antes del insert.
select NEXT VALUE FOR dbo.PurchaseReceiptNumbers as Number

go
