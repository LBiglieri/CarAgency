CREATE or ALTER proc Vehiculo_Update	@Vehiculo_Id uniqueidentifier, @Patente varchar(255), @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier, @Version_Id uniqueidentifier, @Color_Id uniqueidentifier,
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
go

