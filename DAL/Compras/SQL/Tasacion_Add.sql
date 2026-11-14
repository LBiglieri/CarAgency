CREATE or ALTER proc Tasacion_Add	@Tasacion_Id uniqueidentifier, @Compra_Id uniqueidentifier, @Inspeccion_Id uniqueidentifier, @Tasador_Id uniqueidentifier,
									@Valor float, @Justificacion varchar(max), @Fecha datetime, @Vigencia datetime, @DVH char(64) as

insert into Tasacion(Tasacion_Id, Compra_Id, Inspeccion_Id, Tasador_Id, Valor, Justificacion, Fecha, Vigencia, DVH)
values (@Tasacion_Id, @Compra_Id, @Inspeccion_Id, @Tasador_Id, @Valor, @Justificacion, @Fecha, @Vigencia, @DVH)

select 'success' as SQLResultType,'Appraisal added succesfully!' as message
go
