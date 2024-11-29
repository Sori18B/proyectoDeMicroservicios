alter procedure TicketListById
@TicketID int
as
begin
	select 
	T.TicketID, 
	T.Title, 
	T.Description, 
	T.CreationDate, 
	T.UpdateDate, 
	S.StatusName, 
	P.PriorityName,
	TH.ChangeDescription,
	T.ClientID, 
	T.RepID
	
	from Tickets T
	inner join Statuses S on T.StatusID = S.StatusID
	inner join Priorities P on T.PriorityID = P.PriorityID
	inner join TicketHistory TH on T.TicketID = TH.TicketID
	where T.TicketID = @TicketID;
end;

Exec TicketListById @TicketID=1;

select * from Tickets where TicketID = 2;
select * from TicketHistory;
select * from Assignments;

select COLUMN_NAME, DATA_TYPE
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = 'Tickets';
