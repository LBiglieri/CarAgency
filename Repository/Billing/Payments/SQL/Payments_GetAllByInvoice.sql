CREATE or ALTER proc Payments_GetAllByInvoice @Invoice_Id uniqueidentifier as

select	a.Id,
        a.Invoice_Id,
        a.PaymentType_Id, b.Description as PaymentType_Description,
        a.Amount,
        a.Detail
from	Payments a
		join PaymentTypes b on (b.Id = a.PaymentType_Id)
where	a.Invoice_Id = @Invoice_Id

go


