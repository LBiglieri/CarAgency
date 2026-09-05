CREATE or ALTER proc Vehicles_Delete	@Id uniqueidentifier as

delete Vehicles
where Id = @Id

select 'success' as SQLResultType,'Vehicle deleted succesfully!' as message
go

