CREATE or ALTER proc Usuario_GetUserById @Usuario_Id uniqueidentifier as

select	u.Usuario_Id, u.DNI, u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.Usuario_Id = @Usuario_Id

go