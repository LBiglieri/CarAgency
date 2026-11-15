CREATE or ALTER proc Persona_GetByDni @DNI int as

select	u.Persona_Id,u.DNI,u.Nombre,u.Apellido,u.Direccion,u.TelefonoPersonal,u.TelefonoCasa,u.Email,u.FechaNacimiento
from	Persona  u
where	u.DNI = @DNI

go