alter procedure TicketHistoryList
@TicketID int
as
begin
	begin transaction
		select EntryDate, ChangeDescription
		from TicketHistory
		where TicketID = @TicketID
	commit transaction
end;

execute TicketHistoryList @TicketID=6

select * from TicketHistory