alter procedure ClosedTickets
@RepID int
as
begin
	begin transaction
		select count(*) AS ClosedTickets
		from Tickets
		where StatusID =3
		and RepID = @RepID
	commit transaction;
end;

exec ClosedTickets @RepID = 2;