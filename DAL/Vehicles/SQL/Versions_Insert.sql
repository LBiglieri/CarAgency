CREATE or ALTER proc Versions_Insert @Id uniqueidentifier, @Make_Id uniqueidentifier, @Model_Id uniqueidentifier, @Description varchar(255) , @DVH char(64) as

if not exists (select * from Versions where Make_Id = @Make_Id and Model_Id = @Model_Id and Description = @Description)
begin
	insert into Versions(Id,Make_Id,Model_Id,Description,DVH) values (@Id,@Make_Id,@Model_Id,@Description,@DVH);
end

select 'success' as SQLResultType, 'Operation completed.' as message
go
