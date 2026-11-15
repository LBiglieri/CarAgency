CREATE or ALTER proc VehiculoProspecto_Add	@VehiculoProspecto_Id uniqueidentifier, @Patente varchar(10), @NumeroChasis varchar(17), @NumeroMotor varchar(20), @Marca_Id uniqueidentifier,
										@Modelo_Id uniqueidentifier, @Version_Id uniqueidentifier, @Color_Id uniqueidentifier, @Anio int, @Puertas int,
										@Kilometraje int, @DVH char(64) as

insert into VehiculoProspecto(VehiculoProspecto_Id, Patente, NumeroChasis, NumeroMotor, Marca_Id, Modelo_Id, Version_Id, Color_Id, Anio, Puertas, Kilometraje, DVH)
values (@VehiculoProspecto_Id, @Patente, @NumeroChasis, @NumeroMotor, @Marca_Id, @Modelo_Id, @Version_Id, @Color_Id, @Anio, @Puertas, @Kilometraje, @DVH)

select 'success' as SQLResultType,'Prospect vehicle added succesfully!' as message
go
