CREATE or ALTER proc Inspeccion_GetMechanics @Tipo varchar(50) as

-- Mecanico: usuario activo y no bloqueado cuyo rol tiene la patente @Type en cualquier nivel.
with recursive as	(
						select	sp2.Padre_Id as Rol_Id, sp2.Hijo_Id
						from	Permiso_Permiso SP2
						UNION ALL
						select	r.Rol_Id, sp.Hijo_Id
						from	Permiso_Permiso sp
								inner join recursive r on (r.Hijo_Id=sp.Padre_Id)
					)
select	u.Usuario_Id, u.DNI, u.NombreUsuario, Clave='' , u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u
where	u.Activo = 1
		and u.Bloqueado = 0
		and exists	(
						select	*
						from	recursive r
								inner join Permiso p on (r.Hijo_Id = p.Permiso_Id)
						where	r.Rol_Id = u.Rol_Id
								and p.Tipo = @Tipo
					)
order by u.NombreUsuario asc

go
