CREATE or ALTER proc User_GetUserByUsername @Username varchar(50) as

select	u.Id,u.Username,u.Password 
from	Users u 
where	u.Username = @Username

go