alter procedure ReopenTicket
@TicketID int,
@UserID int -- para registrar quién realizó la acción
as
begin
	declare @StatusID int = 4; -- Estado "Reabierto"

	begin transaction

		update 
			Tickets
		set 
			StatusID = @StatusID, 
			UpdateDate = getdate()
		where 
			TicketID = @TicketID
			and StatusID = 3

		if @@ROWCOUNT > 0
		begin
			insert into 
				TicketHistory (TicketID, EntryDate, ChangeDescription, UserID)
			values 
				(@TicketID, getdate(), 'Ticket reabierto', @UserID);
		end

	commit transaction;
end;

Exec ReopenTicket @TicketID=1, @UserID = 3;


select * from Tickets
select * from TicketHistory