alter procedure DeleteTicket
@TicketID int
as
begin
	begin transaction
		delete
		from Tickets
		where TicketID = @TicketID
	commit transaction
end;

exec DeleteTicket @TicketID=10