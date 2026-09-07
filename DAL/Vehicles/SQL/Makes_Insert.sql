CREATE or ALTER proc Makes_Insert @Id uniqueidentifier, @Description varchar(50) , @DVH char(64) as

if not exists (select * from Makes where Description = @Description)
begin
	insert into Makes(Id,Description,DVH) values (@Id,@Description,@DVH);
end

select 'success' as SQLResultType,'Make added succesfully!' as message

go
