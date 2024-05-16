CREATE or ALTER proc Permissions_DeleteFamily @Id uniqueidentifier as

delete from Permission_Permission where Father_Id=@Id;

go