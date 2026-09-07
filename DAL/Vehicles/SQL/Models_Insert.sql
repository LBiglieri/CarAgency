CREATE or ALTER proc Models_Insert @Id uniqueidentifier, @Make_Id uniqueidentifier, @Description varchar(255) , @DVH char(64) as

if not exists (select * from Models where Make_Id = @Make_Id and Description = @Description)
begin
	insert into Models(Id,Make_Id,Description,DVH) values (@Id,@Make_Id,@Description,@DVH);
end

select 'success' as SQLResultType, 'Operation completed.' as message
go
