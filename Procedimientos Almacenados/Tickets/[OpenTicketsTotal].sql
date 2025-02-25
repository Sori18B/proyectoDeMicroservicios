ALTER PROCEDURE OpenTicketsTotal
@RepID int
AS
BEGIN
    BEGIN TRANSACTION;
    
    SELECT COUNT(*) AS OpenTickets
    FROM Tickets
    WHERE (StatusID = 1 OR StatusID = 4)
    AND RepID = @RepID;
    
    COMMIT TRANSACTION;
END;


exec OpenTicketsTotal @RepID= 6