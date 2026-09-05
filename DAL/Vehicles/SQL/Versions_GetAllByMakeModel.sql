CREATE or ALTER proc Versions_GetAllByMakeModel @Make_Id uniqueidentifier, @Model_Id uniqueidentifier as

select	a.Id, a.Make_Id, b.Description as Make_Description, a.Model_Id, c.Description as Model_Description, a.Description 
from	Versions a
		join Makes b on (b.Id = a.Make_Id)
		join Models c on (c.Id = a.Model_Id)
where	a.Make_Id = @Make_Id
		and a.Model_Id = @Model_Id
order by a.Description asc

go