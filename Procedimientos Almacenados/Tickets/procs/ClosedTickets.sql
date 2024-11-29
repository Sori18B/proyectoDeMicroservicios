create procedure ClosedTickets
as
begin
	begin transaction
		select count(*) AS ClosedTickets
		from Tickets
		where StatusID =3;
	commit transaction;
end;

exec ClosedTickets