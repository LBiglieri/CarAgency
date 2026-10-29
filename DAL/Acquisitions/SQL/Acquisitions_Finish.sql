CREATE or ALTER proc Acquisitions_Finish	@Vehicle_Id uniqueidentifier, @License_Plate varchar(255), @Make_Id uniqueidentifier, @Model_Id uniqueidentifier,
										@Version_Id uniqueidentifier, @Colour_Id uniqueidentifier, @Price float, @Opcionals varchar(255),
										@Observations varchar(max), @Doors int, @Year int, @Kilometers int, @ImageLink nvarchar(max), @DVH char(64),
										@Acquisition_Id uniqueidentifier, @Status int, @Closing_Date datetime, @Acquisition_DVH char(64) as

-- CUF10 pasos 23 y 24: el vehiculo entra al inventario y la compra se finaliza juntos o no se graba nada.
-- Si quedara solo el vehiculo, apareceria disponible en CUF01 con la compra sin cerrar.
-- .NET calcula cada DVH con la fila completa; en Acquisitions solo se escriben las columnas que cambian.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Acquisitions where Id = @Acquisition_Id and Status = 7)
		throw 51011, 'La adquisicion ya no esta en estado Transferida.', 1;

	insert into Vehicles(Id,License_Plate,Make_Id,Model_Id,Version_Id,Colour_Id,Price,Opcionals,Observations,Doors,Year,Kilometers,ImageLink,DVH)
	values (@Vehicle_Id,@License_Plate,@Make_Id,@Model_Id,@Version_Id,@Colour_Id,@Price,@Opcionals,@Observations,@Doors,@Year,@Kilometers,@ImageLink,@DVH)

	update Acquisitions
	set DVH = @Acquisition_DVH,
		Status = @Status,
		Vehicle_Id = @Vehicle_Id,
		Closing_Date = @Closing_Date
	where Id = @Acquisition_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Acquisition finished succesfully!' as message
go
