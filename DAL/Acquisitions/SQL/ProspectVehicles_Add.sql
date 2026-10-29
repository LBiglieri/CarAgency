CREATE or ALTER proc ProspectVehicles_Add	@Id uniqueidentifier, @License_Plate varchar(10), @Chassis_Number varchar(17), @Engine_Number varchar(20), @Make_Id uniqueidentifier,
										@Model_Id uniqueidentifier, @Version_Id uniqueidentifier, @Colour_Id uniqueidentifier, @Year int, @Doors int,
										@Declared_Kilometers int, @DVH char(64) as

insert into ProspectVehicles(Id, License_Plate, Chassis_Number, Engine_Number, Make_Id, Model_Id, Version_Id, Colour_Id, Year, Doors, Declared_Kilometers, DVH)
values (@Id, @License_Plate, @Chassis_Number, @Engine_Number, @Make_Id, @Model_Id, @Version_Id, @Colour_Id, @Year, @Doors, @Declared_Kilometers, @DVH)

select 'success' as SQLResultType,'Prospect vehicle added succesfully!' as message
go
