CREATE or ALTER proc Permiso_GetAll @Family uniqueidentifier as

with recursive as	(
						select	sp2.Padre_Id, sp2.Hijo_Id  
						from	Permiso_Permiso SP2
						where	(sp2.Padre_Id = @Family or @Family is null)
						UNION ALL 
						select sp.Padre_Id, sp.Hijo_Id 
						from	Permiso_Permiso sp 
								inner join recursive r on (r.Hijo_Id=sp.Padre_Id)
					)
select	r.Padre_Id,
		r.Hijo_Id,
		p.Permiso_Id,
		p.Nombre, 
		p.Tipo
from	recursive r 
		inner join Permiso p on (r.Hijo_Id = p.Permiso_Id)

go