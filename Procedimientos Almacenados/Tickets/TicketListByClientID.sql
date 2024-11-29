ALTER PROCEDURE TicketListByClientID
    @ClientID INT
AS
BEGIN
    SELECT  
        T.Title, 
        T.Description, 
        T.CreationDate, 
        T.UpdateDate, 
        S.StatusName, 
        P.PriorityName,
        TH.ChangeDescription
    FROM Tickets T
    INNER JOIN Statuses S ON T.StatusID = S.StatusID
    INNER JOIN Priorities P ON T.PriorityID = P.PriorityID
    LEFT JOIN TicketHistory TH ON T.TicketID = TH.TicketID
    WHERE T.ClientID = @ClientID
    ORDER BY T.CreationDate DESC;
END;


exec TicketListByClientID @ClientID = 4;