CREATE or ALTER proc Permissions_GetAll @Family uniqueidentifier as

with recursive as	(
						select	sp2.Father_Id, sp2.Child_Id  
						from	Permission_Permission SP2
						where	(sp2.Father_Id = @Family or @Family is null)
						UNION ALL 
						select sp.Father_Id, sp.Child_Id 
						from	Permission_Permission sp 
								inner join recursive r on (r.Child_Id=sp.Father_Id)
					)
select	r.Father_Id,
		r.Child_Id,
		p.Id,
		p.Name, 
		p.Type
from	recursive r 
		inner join Permissions p on (r.Child_Id = p.id)

go