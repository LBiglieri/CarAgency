CREATE or ALTER proc Inspections_GetNextFreeSlot	@Mechanic_Id uniqueidentifier, @From datetime, @Slot_Minutes int, @Day_Start_Hour int,
												@Day_End_Hour int, @Max_Days int as

-- Turnos candidatos desde el dia de @From. El 01/01/1900 fue lunes: datediff(day, 0, fecha) % 7
-- da 0 a 4 de lunes a viernes sin depender de SET DATEFIRST.
with slots as	(
					select	dateadd(hour, @Day_Start_Hour, cast(cast(@From as date) as datetime)) as Slot
					UNION ALL
					select	dateadd(minute, @Slot_Minutes, s.Slot)
					from	slots s
					where	s.Slot < dateadd(day, @Max_Days, @From)
				)
select	top 1 s.Slot as Scheduled_Date
from	slots s
where	s.Slot >= @From
		and datediff(day, 0, s.Slot) % 7 < 5
		and datepart(hour, s.Slot) >= @Day_Start_Hour
		and dateadd(minute, @Slot_Minutes, s.Slot) <= dateadd(hour, @Day_End_Hour, cast(cast(s.Slot as date) as datetime))
		and not exists	(
							select	*
							from	Inspections a
									join Acquisitions b on (b.Id = a.Acquisition_Id)
							where	a.Mechanic_Id = @Mechanic_Id
									and b.Status = 1
									and a.Scheduled_Date > dateadd(minute, -@Slot_Minutes, s.Slot)
									and a.Scheduled_Date < dateadd(minute, @Slot_Minutes, s.Slot)
						)
order by s.Slot
option (maxrecursion 0)

go
