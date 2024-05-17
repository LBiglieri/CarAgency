CREATE or ALTER proc User_GetUserById @Id uniqueidentifier as

select	u.Id,u.Username,u.Password, u.Name, u.Surname, u.Role, u.Blocked, u.Active
from	Users u 
where	u.Id = @Id

go