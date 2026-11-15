CREATE or ALTER proc Persona_GetById @Persona_Id uniqueidentifier as

select	Persona_Id,DNI,Nombre,Apellido,Direccion,TelefonoPersonal,TelefonoCasa,Email,FechaNacimiento
from	Persona  
where	Persona_Id = @Persona_Id

go