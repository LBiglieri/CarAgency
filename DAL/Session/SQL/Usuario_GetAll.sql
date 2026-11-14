CREATE or ALTER proc Usuario_GetAll as

select	u.Usuario_Id,u.NombreUsuario
from	Usuario u 
order by u.NombreUsuario asc	

go