CREATE PROCEDURE CompleteTicket
    @PreliminaryID INT,
    @StatusID	   INT,
    @PriorityID	   INT,
    @RepID		   INT
AS
BEGIN
    BEGIN TRANSACTION;
    
    -- Insertar los datos completos en la tabla Tickets
    INSERT INTO Tickets (Title, [Description], CreationDate, StatusID, PriorityID, ClientID, RepID)
    SELECT 
        Title,
        [Description],
        CreationDate,
        @StatusID, -- Asignar el StatusID recibido como parámetro
        @PriorityID, -- Asignar el PriorityID recibido como parámetro
        ClientID,
        @RepID -- Asignar el RepID recibido como parámetro
    FROM PreliminaryTickets
    WHERE PreliminaryID = @PreliminaryID;

    -- Eliminar el registro de la tabla PreliminaryTickets
    DELETE FROM PreliminaryTickets
    WHERE PreliminaryID = @PreliminaryID;

    COMMIT TRANSACTION;
END;

exec CompleteTicket
	@PreliminaryID = 1,
	@StatusID	   = 1,
	@PriorityID	   = 1,
	@RepID		   = 1;


select * from PreliminaryTickets

select * from Tickets