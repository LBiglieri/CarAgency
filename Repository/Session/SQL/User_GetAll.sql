CREATE or ALTER proc User_GetAll as

select	u.Id,u.Username
from	Users u 
order by u.Username asc	

go