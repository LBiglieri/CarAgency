CREATE or ALTER proc User_GetAllByState @Active bit as

select	u.Id,u.Username, Password='' , u.Name, u.Surname, u.Role, u.Blocked, u.Active
from	Users u 
where	(@Active = 1 and u.Active = @Active) or @Active = 0 --Si active es 0 muestro todos los usuarios. 
order by u.Username asc	

go