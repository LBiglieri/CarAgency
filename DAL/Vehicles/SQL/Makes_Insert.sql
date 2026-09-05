CREATE or ALTER proc Makes_Insert @Id uniqueidentifier, @Description varchar(50) as

if not exists (select * from Makes where Description = @Description)
begin
	insert into Makes(Id,Description) values (@Id,@Description);
end

select 'success' as SQLResultType,'Make added succesfully!' as message

go