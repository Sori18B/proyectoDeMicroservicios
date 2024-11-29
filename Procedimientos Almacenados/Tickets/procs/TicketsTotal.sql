create procedure TicketsTotal
as
begin
	begin transaction
		select count(*) AS TicketsTotal
		from Tickets;
	commit transaction;
end;

exec TicketsTotal;