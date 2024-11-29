create procedure TicketListByStatus
@StatusID int
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
	where S.StatusID = @StatusID;
end;

exec TicketListByStatus @StatusID= 1
exec TicketListByStatus @StatusID= 2
exec TicketListByStatus @StatusID= 3


--1	Abierto
--2	En proceso
--3	Resuelto

select * from Statuses