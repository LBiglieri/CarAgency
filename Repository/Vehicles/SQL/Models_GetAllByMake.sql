CREATE or ALTER proc Models_GetAllByMake @Make_Id uniqueidentifier as

select	a.Id,a.Make_Id, b.Description as Make_Description, a.Description 
from	Models a
		join Makes b on (b.Id = Make_Id)
where	a.Make_Id = @Make_Id
order by a.Description asc
go