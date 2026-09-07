CREATE or ALTER proc Vehicles_Add	@Id uniqueidentifier, @License_Plate varchar(255), @Make_Id uniqueidentifier, @Model_Id uniqueidentifier, @Version_Id uniqueidentifier, @Colour_Id uniqueidentifier,
									@Price float, @Opcionals varchar(255), @Observations varchar(max), @Doors int, @Year int, @Kilometers int, @ImageLink nvarchar(max) , @DVH char(64) as

insert into Vehicles(Id,License_Plate,Make_Id,Model_Id,Version_Id,Colour_Id,Price,Opcionals,Observations,Doors,Year,Kilometers,ImageLink,DVH)
values (@Id,@License_Plate,@Make_Id,@Model_Id,@Version_Id,@Colour_Id,@Price,@Opcionals,@Observations,@Doors,@Year,@Kilometers,@ImageLink,@DVH)

select 'success' as SQLResultType,'Vehicle added succesfully!' as message
go

