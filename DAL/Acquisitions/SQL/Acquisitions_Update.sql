CREATE or ALTER proc Acquisitions_Update	@Id uniqueidentifier, @Number int, @Client_Id uniqueidentifier, @ProspectVehicle_Id uniqueidentifier, @Salesman_Id uniqueidentifier,
											@Status int, @Rejection_Reason int, @Creation_Date datetime, @Accepted_Appraisal_Id uniqueidentifier, @Agreed_Price float,
											@Owner_CUIL_CUIT varchar(13), @Contract_Signed_Date datetime, @Precharge_Code_08 varchar(50), @Transfer_Appointment_Date datetime,
											@Vehicle_Id uniqueidentifier, @Closing_Date datetime, @DVH char(64) as

-- Fila completa: el DVH se recalcula con todas las columnas aunque solo cambie el estado.
update Acquisitions
set DVH = @DVH,
	Number = @Number,
	Client_Id = @Client_Id,
	ProspectVehicle_Id = @ProspectVehicle_Id,
	Salesman_Id = @Salesman_Id,
	Status = @Status,
	Rejection_Reason = @Rejection_Reason,
	Creation_Date = @Creation_Date,
	Accepted_Appraisal_Id = @Accepted_Appraisal_Id,
	Agreed_Price = @Agreed_Price,
	Owner_CUIL_CUIT = @Owner_CUIL_CUIT,
	Contract_Signed_Date = @Contract_Signed_Date,
	Precharge_Code_08 = @Precharge_Code_08,
	Transfer_Appointment_Date = @Transfer_Appointment_Date,
	Vehicle_Id = @Vehicle_Id,
	Closing_Date = @Closing_Date
where Id = @Id

select 'success' as SQLResultType,'Acquisition updated succesfully!' as message
go
