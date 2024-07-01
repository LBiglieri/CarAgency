﻿CREATE or ALTER proc User_GetUserById @Id uniqueidentifier as

select	u.Id, u.Dni, u.Username,u.Password, u.Name, u.Surname, u.Role_Id, u.Blocked, u.Active, u.Available_Login_Attempts
from	Users u 
where	u.Id = @Id

go