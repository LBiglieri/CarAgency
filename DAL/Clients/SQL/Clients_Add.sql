CREATE or ALTER proc Clients_Add	@Id uniqueidentifier, @Dni int, @Name varchar(max), @Surname varchar(max), @Address varchar(max),
									@Phone_Number_Personal varchar(max), @Phone_Number_House varchar(max), @Email varchar(max), @Date_Of_Birth datetime , @DVH char(64) as

insert into Clients(Id,Dni,Name,Surname,Address,Phone_Number_Personal,Phone_Number_House,Email,Date_Of_Birth,DVH)
values (@Id,@Dni,@Name,@Surname,@Address,@Phone_Number_Personal,@Phone_Number_House,@Email,@Date_Of_Birth,@DVH)

select 'success' as SQLResultType,'Client added succesfully!' as message
go
