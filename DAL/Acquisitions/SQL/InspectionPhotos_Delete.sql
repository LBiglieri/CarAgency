CREATE or ALTER proc InspectionPhotos_Delete @Id uniqueidentifier as

delete from InspectionPhotos
where Id = @Id

select 'success' as SQLResultType,'Inspection photo deleted succesfully!' as message
go
