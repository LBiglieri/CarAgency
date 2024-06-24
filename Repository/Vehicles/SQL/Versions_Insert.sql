CREATE or ALTER proc Versions_Insert @Id uniqueidentifier, @Make_Id uniqueidentifier, @Model_Id uniqueidentifier, @Description varchar(50) as

if not exists (select * from Versions where Make_Id = @Make_Id and Model_Id = @Model_Id and Description = @Description)
begin
	insert into Versions(Id,Make_Id,Model_Id,Description) values (@Id,@Make_Id,@Model_Id,@Description);
end

go