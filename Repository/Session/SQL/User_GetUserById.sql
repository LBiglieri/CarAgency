CREATE or ALTER proc User_GetUserById @Id uniqueidentifier as

select	u.Id,u.Username,u.Password 
from	Users u 
where	u.Id = @Id

go