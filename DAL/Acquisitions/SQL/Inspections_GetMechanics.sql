CREATE or ALTER proc Inspections_GetMechanics @Type varchar(50) as

-- Mecanico: usuario activo y no bloqueado cuyo rol tiene la patente @Type en cualquier nivel.
with recursive as	(
						select	sp2.Father_Id as Role_Id, sp2.Child_Id
						from	Permission_Permission SP2
						UNION ALL
						select	r.Role_Id, sp.Child_Id
						from	Permission_Permission sp
								inner join recursive r on (r.Child_Id=sp.Father_Id)
					)
select	u.Id, u.Dni, u.Username, Password='' , u.Name, u.Surname, u.Role_Id, u.Blocked, u.Active, u.Available_Login_Attempts, u.Language_Code
from	Users u
where	u.Active = 1
		and u.Blocked = 0
		and exists	(
						select	*
						from	recursive r
								inner join Permissions p on (r.Child_Id = p.Id)
						where	r.Role_Id = u.Role_Id
								and p.Type = @Type
					)
order by u.Username asc

go
