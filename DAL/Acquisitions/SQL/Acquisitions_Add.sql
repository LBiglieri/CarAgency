CREATE or ALTER proc Acquisitions_Add	@Id uniqueidentifier, @Number int, @Client_Id uniqueidentifier, @ProspectVehicle_Id uniqueidentifier, @Salesman_Id uniqueidentifier,
									@Status int, @Rejection_Reason int, @Creation_Date datetime, @Accepted_Appraisal_Id uniqueidentifier, @Agreed_Price float,
									@Owner_CUIL_CUIT varchar(13), @Contract_Signed_Date datetime, @Precharge_Code_08 varchar(50), @Transfer_Appointment_Date datetime,
									@Vehicle_Id uniqueidentifier, @Closing_Date datetime, @DVH char(64) as

insert into Acquisitions(Id, Number, Client_Id, ProspectVehicle_Id, Salesman_Id, Status, Rejection_Reason, Creation_Date, Accepted_Appraisal_Id, Agreed_Price,
						 Owner_CUIL_CUIT, Contract_Signed_Date, Precharge_Code_08, Transfer_Appointment_Date, Vehicle_Id, Closing_Date, DVH)
values (@Id, @Number, @Client_Id, @ProspectVehicle_Id, @Salesman_Id, @Status, @Rejection_Reason, @Creation_Date, @Accepted_Appraisal_Id, @Agreed_Price,
		@Owner_CUIL_CUIT, @Contract_Signed_Date, @Precharge_Code_08, @Transfer_Appointment_Date, @Vehicle_Id, @Closing_Date, @DVH)

select 'success' as SQLResultType,'Acquisition added succesfully!' as message
go
