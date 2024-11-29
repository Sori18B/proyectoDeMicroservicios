alter procedure UpdateRepAssign
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

EXEC UpdateRepAssign 
    @RepID = 18,  -- del nuevo representante asignado
    @TicketID = 2;  -- del ticket a actualizar

select * from Assignments
select * from Tickets