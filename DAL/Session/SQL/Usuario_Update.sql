CREATE or ALTER proc Usuario_Update	@Usuario_Id uniqueidentifier, @DNI int, @NombreUsuario varchar(50), @Clave varchar(max), @Nombre nvarchar(max), @Apellido nvarchar(max),
											@Rol_Id uniqueidentifier, @Bloqueado bit, @Activo bit, @IntentosDisponibles int, @CodigoIdioma varchar(2) , @DVH char(64) as

update Usuario
set DVH = @DVH,
	DNI = @DNI,
	NombreUsuario = @NombreUsuario,
	Clave = @Clave,
	Nombre = @Nombre,
	Apellido = @Apellido,
	Rol_Id = @Rol_Id,
	Bloqueado = @Bloqueado,
	Activo = @Activo,
	IntentosDisponibles = @IntentosDisponibles,
	CodigoIdioma = @CodigoIdioma
where Usuario_Id = @Usuario_Id

select 'success' as SQLResultType,'User updated succesfully!' as message
go

