CREATE or ALTER proc Persona_Add	@Persona_Id uniqueidentifier, @DNI int, @Nombre varchar(max), @Apellido varchar(max), @Direccion varchar(max),
									@TelefonoPersonal varchar(max), @TelefonoCasa varchar(max), @Email varchar(max), @FechaNacimiento datetime , @DVH char(64) as

insert into Persona(Persona_Id,DNI,Nombre,Apellido,Direccion,TelefonoPersonal,TelefonoCasa,Email,FechaNacimiento,DVH)
values (@Persona_Id,@DNI,@Nombre,@Apellido,@Direccion,@TelefonoPersonal,@TelefonoCasa,@Email,@FechaNacimiento,@DVH)

select 'success' as SQLResultType,'Person added succesfully!' as message
go
