alter procedure TicketsTotal
@RepID int
as
begin
	begin transaction
		select count(*) AS TicketsTotal
		from Tickets
		where RepID = @RepID
	commit transaction;
end;

exec TicketsTotal @RepID = 6