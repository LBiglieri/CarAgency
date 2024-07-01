CREATE or ALTER proc User_GetAllByState @Active bit as

select	u.Id, u.Dni, u.Username, Password='' , u.Name, u.Surname, u.Role_Id, u.Blocked, u.Active, u.Available_Login_Attempts
from	Users u 
where	(@Active = 1 and u.Active = @Active) or @Active = 0 --Si active es 0 muestro todos los usuarios. 
order by u.Username asc	

go