CREATE or ALTER proc Inspeccion_GetNextFreeSlot	@Mecanico_Id uniqueidentifier, @From datetime, @Slot_Minutes int, @Day_Start_Hour int,
												@Day_End_Hour int, @Max_Days int as

-- Turnos candidatos desde el dia de @From. El 01/01/1900 fue lunes: datediff(day, 0, fecha) % 7
-- da 0 a 4 de lunes a viernes sin depender de SET DATEFIRST.
with slots as	(
					select	dateadd(hour, @Day_Start_Hour, cast(cast(@From as date) as datetime)) as Turno
					UNION ALL
					select	dateadd(minute, @Slot_Minutes, s.Turno)
					from	slots s
					where	s.Turno < dateadd(day, @Max_Days, @From)
				)
select	top 1 s.Turno as FechaProgramada
from	slots s
where	s.Turno >= @From
		and datediff(day, 0, s.Turno) % 7 < 5
		and datepart(hour, s.Turno) >= @Day_Start_Hour
		and dateadd(minute, @Slot_Minutes, s.Turno) <= dateadd(hour, @Day_End_Hour, cast(cast(s.Turno as date) as datetime))
		and not exists	(
							select	*
							from	Inspeccion a
									join Compra b on (b.Compra_Id = a.Compra_Id)
							where	a.Mecanico_Id = @Mecanico_Id
									and b.Estado = 1
									and a.FechaProgramada > dateadd(minute, -@Slot_Minutes, s.Turno)
									and a.FechaProgramada < dateadd(minute, @Slot_Minutes, s.Turno)
						)
order by s.Turno
option (maxrecursion 0)

go
