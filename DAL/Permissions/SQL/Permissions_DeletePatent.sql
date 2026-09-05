CREATE or ALTER proc Permissions_DeletePatent @Id uniqueidentifier as

declare @errn int =-1

begin tran 

delete from Permission_Permission where Father_Id=@Id;

select @errn = @@ERROR

if @errn <> 0
begin
	rollback tran
	select @errn
	return
end

delete from Permission_Permission where Child_Id=@Id;

select @errn = @@ERROR

if @errn <> 0
begin
	rollback tran
	select @errn
	return
end

delete from Permissions where Id=@Id;

select @errn = @@ERROR

if @errn <> 0
begin
	rollback tran
	select @errn
	return
end

commit tran

go