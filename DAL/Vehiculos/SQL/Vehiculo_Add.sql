CREATE or ALTER proc Vehiculo_Add	@Vehiculo_Id uniqueidentifier, @Patente varchar(255), @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier, @Version_Id uniqueidentifier, @Color_Id uniqueidentifier,
									@Precio float, @Opcionales varchar(255), @Observaciones varchar(max), @Puertas int, @Anio int, @Kilometraje int, @LinkImagen nvarchar(max) , @DVH char(64) as

insert into Vehiculo(Vehiculo_Id,Patente,Marca_Id,Modelo_Id,Version_Id,Color_Id,Precio,Opcionales,Observaciones,Puertas,Anio,Kilometraje,LinkImagen,DVH)
values (@Vehiculo_Id,@Patente,@Marca_Id,@Modelo_Id,@Version_Id,@Color_Id,@Precio,@Opcionales,@Observaciones,@Puertas,@Anio,@Kilometraje,@LinkImagen,@DVH)

select 'success' as SQLResultType,'Vehicle added succesfully!' as message
go

