create procedure UpdateRepAssign
@RepID int,
@TicketID int
as
begin
	begin transaction
		update Tickets
		set RepID = @RepID
		where TicketID = @TicketID;

		update Assignments
		set RepID = @RepID
		where TicketID = @TicketID;
	commit transaction
end;
