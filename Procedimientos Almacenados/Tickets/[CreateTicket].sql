create procedure CreateTicket
--Parametros
@Title			nvarchar(255),
@Description	nvarchar(MAX),
@StatusID		int,
@PriorityID		int,
@ClientID		int,
@RepID			int
as
begin
	-- Comienza una transacción 
	begin transaction

	--Crear el ticket
	declare @NewTicketID int
	INSERT INTO Tickets (Title, Description, CreationDate, UpdateDate, StatusID, PriorityID, ClientID, RepID) VALUES
    (@Title, @Description, GETDATE(), GETDATE(), @StatusID, @PriorityID, @ClientID, @RepID);

	-- Obtiene el ID del ticket
	set @NewTicketID = scope_identity()

	-- Inserta una entrada en TicketHistory para registrar la creación del ticket 
	insert into TicketHistory (TicketID, EntryDate, ChangeDescription, UserID) values 
	(@NewTicketID, getdate(), 'Ticket creado', @ClientID);
	
	-- Inserta una entrada en Assignments
	insert into Assignments (TicketID, RepID, AssignmentDate) values 
	(@NewTicketID, @RepID,  getdate());

	-- Confirma la transacción 
	commit transaction
end;