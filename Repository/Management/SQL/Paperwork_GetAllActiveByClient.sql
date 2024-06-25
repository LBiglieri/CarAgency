CREATE or ALTER proc Paperwork_GetAllActiveByClient @Client_Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id,  (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Client_Id,
        a.Invoice_Id, 
        a.Paperwork_Precharge_Code, 
        a.Transfer_Date,
        a.Observations, 
        a.IsFinished
from	Paperwork a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
where	(a.Client_Id = @Client_Id or (@Client_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
		and a.IsFinished = 0

go


