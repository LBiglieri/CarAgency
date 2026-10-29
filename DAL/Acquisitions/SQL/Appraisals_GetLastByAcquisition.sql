CREATE or ALTER proc Appraisals_GetLastByAcquisition @Acquisition_Id uniqueidentifier as

-- La tasacion vigente de una adquisicion es la ultima que se registro.
select	top 1
        a.Id,
        a.Acquisition_Id,
        a.Inspection_Id,
        a.User_Id,
        a.Amount,
        a.Justification,
        a.Creation_Date,
        a.Expiration_Date,
        (b.Name + ' ' + b.Surname) as Appraiser
from	Appraisals a
        join Users b on (b.Id = a.User_Id)
where	a.Acquisition_Id = @Acquisition_Id
order by a.Creation_Date desc

go
