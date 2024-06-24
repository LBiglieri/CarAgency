CREATE or ALTER proc Vehicles_Add	@Id uniqueidentifier, @License_Plate varchar(max), @Make_Id uniqueidentifier, @Model_Id uniqueidentifier, @Version_Id uniqueidentifier, @Colour_Id uniqueidentifier, 
									@Price float, @Opcionals varchar(255), @Observations varchar(max), @Doors tinyint, @Year smallint, @Kilometers int, @ImageLink varchar(max) as

insert into Vehicles(Id,License_Plate,Make_Id,Model_Id,Version_Id,Colour_Id,Price,Opcionals,Observations,Doors,Year,Kilometers,ImageLink) 
values (@Id,@License_Plate,@Make_Id,@Model_Id,@Version_Id,@Colour_Id,@Price,@Opcionals,@Observations,@Doors,@Year,@Kilometers,@ImageLink)

select 'success' as SQLResultType,'Vehicle added succesfully!' as message
go

