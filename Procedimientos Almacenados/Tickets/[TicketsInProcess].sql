alter proc TicketsInProcess
@RepID int
as
begin
	begin transaction
		select count(*) AS TicketsInProcess
		from Tickets
		where StatusID =2
		and RepID = @RepID
	commit transaction;
end;

exec TicketsInProcess @RepID = 2