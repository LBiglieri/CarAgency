CREATE or ALTER proc Usuario_GetAllByState @Activo bit as

select	u.Usuario_Id, u.DNI, u.NombreUsuario, Clave='' , u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	(@Activo = 1 and u.Activo = @Activo) or @Activo = 0 --Si active es 0 muestro todos los usuarios. 
order by u.NombreUsuario asc	

go