CREATE or ALTER proc Permissions_DeleteCompleteFamily @Id uniqueidentifier as

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

if exists (select * from Users  a where  a.Role_Id=@Id)
begin
	declare @baserole uniqueidentifier
	select @baserole = Id from Permissions a where a.Name = 'Base User' 

	update Users
	set Role_Id = @baserole
	where Role_Id = @Id

	select @errn = @@ERROR

	if @errn <> 0
	begin
		rollback tran
		select @errn
		return
	end
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