create procedure TicketsInProcess
as
begin
	begin transaction
		select count(*) AS TicketsInProcess
		from Tickets
		where StatusID =2;
	commit transaction;
end;

exec TicketsInProcess;