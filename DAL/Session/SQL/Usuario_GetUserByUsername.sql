CREATE or ALTER proc Usuario_GetUserByUsername @NombreUsuario varchar(50) as

select	u.Usuario_Id, u.DNI,u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.NombreUsuario = @NombreUsuario

go