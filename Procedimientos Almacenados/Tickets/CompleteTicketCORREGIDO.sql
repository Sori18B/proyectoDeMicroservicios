CREATE PROCEDURE CompleteTicket
    @PreliminaryID INT,
    @StatusID      INT,
    @PriorityID    INT,
    @RepID         INT
AS
BEGIN
    BEGIN TRANSACTION;

    -- Declarar variable para almacenar el ClientID
    DECLARE @ClientID INT;
    DECLARE @NewTicketID INT;

    -- Obtener el ClientID desde PreliminaryTickets
    SELECT @ClientID = ClientID
    FROM PreliminaryTickets
    WHERE PreliminaryID = @PreliminaryID;

    -- Insertar los datos completos en la tabla Tickets
    INSERT INTO Tickets (Title, [Description], CreationDate, StatusID, PriorityID, ClientID, RepID)
    SELECT 
        Title,
        [Description],
        CreationDate,
        @StatusID,
        @PriorityID,
        ClientID,
        @RepID
    FROM PreliminaryTickets
    WHERE PreliminaryID = @PreliminaryID;

    -- Obtener el ID del nuevo ticket
    SET @NewTicketID = SCOPE_IDENTITY();

    -- Insertar una entrada en TicketHistory para registrar la creación del ticket 
    INSERT INTO TicketHistory (TicketID, EntryDate, ChangeDescription, UserID) 
    VALUES (@NewTicketID, GETDATE(), 'Ticket creado', @ClientID);

    -- Insertar una entrada en Assignments
    INSERT INTO Assignments (TicketID, RepID, AssignmentDate) 
    VALUES (@NewTicketID, @RepID, GETDATE());

    -- Eliminar el registro de la tabla PreliminaryTickets
    DELETE FROM PreliminaryTickets
    WHERE PreliminaryID = @PreliminaryID;

    COMMIT TRANSACTION;
END;

EXEC CompleteTicket
    @PreliminaryID = 2,
    @StatusID      = 1,
    @PriorityID    = 1,
    @RepID         = 1;


	select * from Tickets;