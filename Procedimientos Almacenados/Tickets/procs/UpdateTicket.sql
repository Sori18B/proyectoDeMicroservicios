alter procedure UpdateTicket
@TicketID		int,
@Title			nvarchar(255),
@Description	nvarchar(MAX),
--@CreationDate	datetime, --No se debería cambiar esta fecha en los registros
--@UpdateDate		datetime, --Usando getdate para guardar la fecha que se hizo el cambio
@StatusID		int,
@PriorityID		int,
--@ClientID		int, -- El cliente no debería cambiar 
--@RepID		int --Demasiado problema para actualizarlo aqui, que se quede el mismo rep

@UserID int, --ID del usuario que realiza la actualización,
@ChangeDescription nvarchar(255)
as
begin
-- Comienza una transacción
    begin transaction
		update Tickets
		set	
		Title		= 	@Title,		
		Description	= 	@Description,
		--CreationDate= 	@CreationDate,
		UpdateDate	= 	GETDATE(), --No pasa como parametro del form	
		StatusID	= 	@StatusID,	--Valor a obtener x medio del dropdown	
		PriorityID	= 	@PriorityID --Valor a obtener x medio del dropdown	
		--ClientID	= 	@ClientID,	
		--RepID		= 	@RepID			
		where TicketID	= 	@TicketID; --obtenido y almacenado en el local storage

		insert into TicketHistory (TicketID, EntryDate, ChangeDescription, UserID) values 
		(@TicketID, getdate(), @ChangeDescription, @UserID);

	-- Confirma la transacción
    commit transaction
end;


EXEC UpdateTicket 
    @TicketID = 6, 
    @Title = 'no', 
    @Description = 'no', 
    @StatusID = 3,  -- podría representar 'En Progreso'
    @PriorityID = 3,  -- podría representar 'Baja'
	@UserID = 3,  -- Usuario que realiza la actualización
	@ChangeDescription = 'Se actualizó tal cosa';



select * from Tickets
where TicketID = 6
select * from TicketHistory
where TicketID = 6


select * from Statuses
select * from Priorities


select COLUMN_NAME, DATA_TYPE
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = 'Tickets'

select COLUMN_NAME, DATA_TYPE
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = 'TicketHistory'