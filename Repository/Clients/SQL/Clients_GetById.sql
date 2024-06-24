CREATE or ALTER proc Clients_GetById @Id uniqueidentifier as

select	Id,Dni,Name,Surname,Address,Phone_Number_Personal,Phone_Number_House,Email,Date_Of_Birth
from	Clients  
where	Id = @Id

go