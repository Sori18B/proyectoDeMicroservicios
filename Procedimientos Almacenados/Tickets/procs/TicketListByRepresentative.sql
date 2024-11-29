create procedure TicketListByRepresentative
@RepID int
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
	where T.RepID = @RepID;
end;

exec TicketListByRepresentative @RepID= 1
exec TicketListByRepresentative @RepID= 2
exec TicketListByRepresentative @RepID= 3
exec TicketListByRepresentative @RepID= 4



--1	Baja
--2	Media
--3	Alta
--4	Critica

select * from Tickets