CREATE or ALTER proc Usuario_Add	@Usuario_Id uniqueidentifier, @DNI int, @NombreUsuario varchar(50), @Clave varchar(max), @Nombre nvarchar(max), @Apellido nvarchar(max),
											@Rol_Id uniqueidentifier, @Bloqueado bit, @Activo bit, @IntentosDisponibles int, @CodigoIdioma varchar(2) , @DVH char(64) as

insert into Usuario(Usuario_Id,DNI,NombreUsuario,Clave,Nombre,Apellido,Rol_Id,Bloqueado,Activo,IntentosDisponibles,CodigoIdioma,DVH)
values (@Usuario_Id,@DNI,@NombreUsuario,@Clave,@Nombre,@Apellido,@Rol_Id,@Bloqueado,@Activo,@IntentosDisponibles,@CodigoIdioma,@DVH)

select 'success' as SQLResultType,'User added succesfully!' as message
go

