CREATE or ALTER proc Usuario_GetUserByDni @DNI int as

select	u.Usuario_Id, u.DNI, u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.DNI = @DNI

go