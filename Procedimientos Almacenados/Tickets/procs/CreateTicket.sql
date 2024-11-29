alter procedure CreateTicket
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

EXEC CreateTicket
    @Title = 'Error',
    @Description = 'La página de inicio no carga correctamente.',
    @StatusID = 1,
    @PriorityID = 2,
    @ClientID = 5,
    @RepID = 3;


select * from Tickets;
select * from TicketHistory;
select * from Assignments

select COLUMN_NAME, DATA_TYPE
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = 'Assignments';

select * from Statuses
select * from Priorities

select * from Assignments