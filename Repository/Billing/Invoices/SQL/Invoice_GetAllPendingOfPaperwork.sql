CREATE or ALTER proc Invoice_GetAllPendingOfPaperwork as

select	a.Id,
        a.Vehicle_Id,  (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Client_Id,
        a.Reservation_Id, 
        a.Detail, 
        a.CUIL_CUIT_Client,
        a.Razon_Social, 
        a.Amount,
        a.Payment_Status, 
        a.Creation_Date
from	Invoice a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)

go


