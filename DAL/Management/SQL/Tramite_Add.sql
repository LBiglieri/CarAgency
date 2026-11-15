CREATE or ALTER proc Tramite_Add	@Tramite_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Factura_Id uniqueidentifier, @CodigoPrecarga08 varchar(50),
									@FechaTransferencia datetime, @Observaciones varchar(max), @Finalizado bit , @DVH char(64) as

insert into Tramite(Tramite_Id, Vehiculo_Id, Persona_Id, Factura_Id, CodigoPrecarga08, FechaTransferencia, Observaciones, Finalizado,DVH)
values (@Tramite_Id, @Vehiculo_Id, @Persona_Id, @Factura_Id, @CodigoPrecarga08, @FechaTransferencia, @Observaciones, @Finalizado,@DVH)

select 'success' as SQLResultType,'Paperwork added succesfully!' as message
go

