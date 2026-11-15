CREATE or ALTER proc Inspeccion_Add	@Inspeccion_Id uniqueidentifier, @Compra_Id uniqueidentifier, @Mecanico_Id uniqueidentifier, @FechaProgramada datetime,
									@FechaRealizada datetime, @KilometrajeVerificado int, @Resultado int, @Observaciones varchar(max), @DVH char(64) as

insert into Inspeccion(Inspeccion_Id, Compra_Id, Mecanico_Id, FechaProgramada, FechaRealizada, KilometrajeVerificado, Resultado, Observaciones, DVH)
values (@Inspeccion_Id, @Compra_Id, @Mecanico_Id, @FechaProgramada, @FechaRealizada, @KilometrajeVerificado, @Resultado, @Observaciones, @DVH)

select 'success' as SQLResultType,'Inspection added succesfully!' as message
go
