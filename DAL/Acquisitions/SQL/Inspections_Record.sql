-- Calificaciones con el DVH ya calculado en .NET. Viajan juntas para grabarse en la misma transaccion.
if type_id('dbo.InspectionScoreRows') is null
	CREATE TYPE [dbo].[InspectionScoreRows] AS TABLE(
		[InspectionItem_Id] [uniqueidentifier] NOT NULL,
		[Score] [int] NOT NULL,
		[Observations] [varchar](max) NULL,
		[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
		PRIMARY KEY CLUSTERED ([InspectionItem_Id] ASC),
		CHECK ((len([DVH])=(64) AND NOT [DVH] like '%[^0-9A-F]%'))
	)
go

CREATE or ALTER proc Inspections_Record	@Id uniqueidentifier, @Acquisition_Id uniqueidentifier, @Performed_Date datetime, @Verified_Kilometers int,
									@Result int, @Observations varchar(max), @DVH char(64), @Scores dbo.InspectionScoreRows READONLY,
									@Status int, @Rejection_Reason int, @Acquisition_DVH char(64) as

-- CUF08: la inspeccion, sus calificaciones y el estado de la adquisicion se graban juntos o no se graba nada.
-- .NET calcula cada DVH con la fila completa; aca solo se escriben las columnas que cambian.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Acquisitions where Id = @Acquisition_Id and Status = 1)
		throw 51010, 'La adquisicion ya no esta en estado Inspeccion programada.', 1;

	update Inspections
	set DVH = @DVH,
		Performed_Date = @Performed_Date,
		Verified_Kilometers = @Verified_Kilometers,
		Result = @Result,
		Observations = @Observations
	where Id = @Id
		and Acquisition_Id = @Acquisition_Id

	insert into InspectionScores(Inspection_Id, InspectionItem_Id, Score, Observations, DVH)
	select @Id, a.InspectionItem_Id, a.Score, a.Observations, a.DVH
	from @Scores a

	update Acquisitions
	set DVH = @Acquisition_DVH,
		Status = @Status,
		Rejection_Reason = @Rejection_Reason
	where Id = @Acquisition_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Inspection recorded succesfully!' as message
go
