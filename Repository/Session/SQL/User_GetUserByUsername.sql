CREATE or ALTER proc User_GetUserByUsername @Username varchar(50) as

select	u.Id,u.Username,u.Password, u.Name, u.Surname, u.Role, u.Blocked, u.Active
from	Users u 
where	u.Username = @Username

go