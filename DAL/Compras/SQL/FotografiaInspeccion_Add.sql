CREATE or ALTER proc FotografiaInspeccion_Add	@FotografiaInspeccion_Id uniqueidentifier, @Inspeccion_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max), @FechaCarga datetime as

insert into FotografiaInspeccion(FotografiaInspeccion_Id, Inspeccion_Id, NombreArchivo, Contenido, FechaCarga)
values (@FotografiaInspeccion_Id, @Inspeccion_Id, @NombreArchivo, @Contenido, @FechaCarga)

select 'success' as SQLResultType,'Inspection photo added succesfully!' as message
go
