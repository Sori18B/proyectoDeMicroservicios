create procedure InfoClient
@ClientID int
as 
begin
	begin transaction
		select Name, Email
		from Users
		Where UserID = @ClientID
	commit transaction;
end;

exec InfoClient @ClientID =2

create procedure InfoRep
@RepID int
as 
begin
	begin transaction
		select Name, Email
		from Users
		Where UserID = @RepID
	commit transaction;
end;

exec InfoRep @RepID =1
