ALTER procedure UpdateTicket
@TicketID		int,
@StatusID		int,
@PriorityID		int,

@UserID int, --ID del usuario que realiza la actualización,
@ChangeDescription nvarchar(255)
as
begin
    begin transaction
		update Tickets
		set	
		UpdateDate	= 	GETDATE(), --No pasa como parametro del form	
		StatusID	= 	@StatusID,	--Valor a obtener x medio del dropdown	
		PriorityID	= 	@PriorityID --Valor a obtener x medio del dropdown		
		where TicketID	= 	@TicketID; --obtenido y almacenado en el local storage

		insert into TicketHistory (TicketID, EntryDate, ChangeDescription, UserID) values 
		(@TicketID, getdate(), @ChangeDescription, @UserID);
    commit transaction
end;

EXEC UpdateTicket @TicketID = 2, 
                  @StatusID = 2,
                  @PriorityID = 1,
                  @UserID = 1,
                  @ChangeDescription = 'Se actualizo el estado a ''En proceso''';

select * from Tickets where RepID = 6;

exec TicketList @RepID = 6