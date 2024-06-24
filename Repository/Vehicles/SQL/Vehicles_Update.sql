CREATE or ALTER proc Vehicles_Update	@Id uniqueidentifier, @License_Plate varchar(max), @Make_Id uniqueidentifier, @Model_Id uniqueidentifier, @Version_Id uniqueidentifier, @Colour_Id uniqueidentifier, 
									@Price float, @Opcionals varchar(255), @Observations varchar(max), @Doors tinyint, @Year smallint, @Kilometers int, @ImageLink varchar(max) as

update Vehicles
set License_Plate = @License_Plate,
	Make_Id = @Make_Id,
	Model_Id = @Model_Id,
	Version_Id = @Version_Id,
	Colour_Id = @Colour_Id,
	Price = @Price,
	Opcionals = @Opcionals,
	Observations = @Observations,
	Doors = @Doors,
	Year = @Year,
	Kilometers = @Kilometers,
	ImageLink = @ImageLink 
where Id = @Id

select 'success' as SQLResultType,'Vehicle updated succesfully!' as message
go

