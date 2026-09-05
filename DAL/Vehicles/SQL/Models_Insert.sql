CREATE or ALTER proc Models_Insert @Id uniqueidentifier, @Make_Id uniqueidentifier, @Description varchar(50) as

if not exists (select * from Models where Make_Id = @Make_Id and Description = @Description)
begin
	insert into Models(Id,Make_Id,Description) values (@Id,@Make_Id,@Description);
end

select 'success' as SQLResultType,'Make added succesfully!' as message


go