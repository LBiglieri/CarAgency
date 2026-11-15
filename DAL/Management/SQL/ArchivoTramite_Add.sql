CREATE or ALTER proc ArchivoTramite_Add	@ArchivoTramite_Id uniqueidentifier, @Tramite_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max), @FechaCarga datetime as

insert into ArchivoTramite(ArchivoTramite_Id, Tramite_Id, NombreArchivo, Contenido, FechaCarga) 
values (@ArchivoTramite_Id, @Tramite_Id, @NombreArchivo, @Contenido, @FechaCarga)

select 'success' as SQLResultType,'Paperwork file added succesfully!' as message
go

