create procedure TicketHistoryList
@TicketID int
as
begin
	begin transaction
		select EntryDate, ChangeDescription
		from TicketHistory
		where TicketID = @TicketID
	commit transaction
end;

exec TicketHistoryList @TicketID = 9
