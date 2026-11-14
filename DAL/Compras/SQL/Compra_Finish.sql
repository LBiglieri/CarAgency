CREATE or ALTER proc Compra_Finish	@Vehiculo_Id uniqueidentifier, @Patente varchar(255), @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier,
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
go
