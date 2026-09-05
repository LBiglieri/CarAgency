CREATE or ALTER proc Clients_GetByDni @Dni int as

select	u.Id,u.Dni,u.Name,u.Surname,u.Address,u.Phone_Number_Personal,u.Phone_Number_House,u.Email,u.Date_Of_Birth
from	Clients  u
where	u.Dni = @Dni

go