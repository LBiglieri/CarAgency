-- Calificaciones con el DVH ya calculado en .NET. Viajan juntas para grabarse en la misma transaccion.
if type_id('dbo.PuntajeInspeccionRows') is null
	CREATE TYPE [dbo].[PuntajeInspeccionRows] AS TABLE(
		[ItemInspeccion_Id] [uniqueidentifier] NOT NULL,
		[Puntaje] [int] NOT NULL,
		[Observaciones] [varchar](max) NULL,
		[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
		PRIMARY KEY CLUSTERED ([ItemInspeccion_Id] ASC),
		CHECK ((len([DVH])=(64) AND NOT [DVH] like '%[^0-9A-F]%'))
	)
go

CREATE or ALTER proc Inspeccion_Record	@Inspeccion_Id uniqueidentifier, @Compra_Id uniqueidentifier, @FechaRealizada datetime, @KilometrajeVerificado int,
									@Resultado int, @Observaciones varchar(max), @DVH char(64), @Scores dbo.PuntajeInspeccionRows READONLY,
									@Estado int, @MotivoRechazo int, @Acquisition_DVH char(64) as

-- CUF08: la inspeccion, sus calificaciones y el estado de la adquisicion se graban juntos o no se graba nada.
-- .NET calcula cada DVH con la fila completa; aca solo se escriben las columnas que cambian.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Compra where Compra_Id = @Compra_Id and Estado = 1)
		throw 51010, 'La adquisicion ya no esta en estado Inspeccion programada.', 1;

	update Inspeccion
	set DVH = @DVH,
		FechaRealizada = @FechaRealizada,
		KilometrajeVerificado = @KilometrajeVerificado,
		Resultado = @Resultado,
		Observaciones = @Observaciones
	where Inspeccion_Id = @Inspeccion_Id
		and Compra_Id = @Compra_Id

	insert into PuntajeInspeccion(Inspeccion_Id, ItemInspeccion_Id, Puntaje, Observaciones, DVH)
	select @Inspeccion_Id, a.ItemInspeccion_Id, a.Puntaje, a.Observaciones, a.DVH
	from @Scores a

	update Compra
	set DVH = @Acquisition_DVH,
		Estado = @Estado,
		MotivoRechazo = @MotivoRechazo
	where Compra_Id = @Compra_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Inspection recorded succesfully!' as message
go
