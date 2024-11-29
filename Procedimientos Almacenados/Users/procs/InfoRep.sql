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