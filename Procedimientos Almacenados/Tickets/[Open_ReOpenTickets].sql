create procedure Open_ReOpenTickets
as
begin
	begin transaction
		select count(*) AS OpenTickets
		from Tickets
		where StatusID =1
		or StatusID =4;
	commit transaction;
end;