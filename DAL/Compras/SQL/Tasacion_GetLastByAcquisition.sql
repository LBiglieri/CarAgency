CREATE or ALTER proc Tasacion_GetLastByAcquisition @Compra_Id uniqueidentifier as

-- La tasacion vigente de una adquisicion es la ultima que se registro.
select	top 1
        a.Tasacion_Id,
        a.Compra_Id,
        a.Inspeccion_Id,
        a.Tasador_Id,
        a.Valor,
        a.Justificacion,
        a.Fecha,
        a.Vigencia,
        (b.Nombre + ' ' + b.Apellido) as Tasador
from	Tasacion a
        join Usuario b on (b.Usuario_Id = a.Tasador_Id)
where	a.Compra_Id = @Compra_Id
order by a.Fecha desc

go
