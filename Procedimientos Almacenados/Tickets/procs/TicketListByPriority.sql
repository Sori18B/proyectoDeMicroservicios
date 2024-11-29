create procedure TicketListByPriority
@PriorityID int
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
	where P.PriorityID = @PriorityID;
end;

exec TicketListByPriority @PriorityID= 1
exec TicketListByPriority @PriorityID= 2
exec TicketListByPriority @PriorityID= 3
exec TicketListByPriority @PriorityID= 4



--1	Baja
--2	Media
--3	Alta
--4	Critica

select * from Priorities