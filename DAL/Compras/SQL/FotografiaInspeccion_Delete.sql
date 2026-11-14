CREATE or ALTER proc FotografiaInspeccion_Delete @FotografiaInspeccion_Id uniqueidentifier as

delete from FotografiaInspeccion
where FotografiaInspeccion_Id = @FotografiaInspeccion_Id

select 'success' as SQLResultType,'Inspection photo deleted succesfully!' as message
go
