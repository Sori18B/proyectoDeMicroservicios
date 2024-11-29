create procedure TicketAssignmentList
@RepID int
as
begin
	begin transaction
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
	commit transaction
end;

execute TicketAssignmentList @RepID=3

select * from Assignments
select * from Tickets