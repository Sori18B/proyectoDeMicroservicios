alter procedure TicketList
    @RepID int
as
begin
	select  
	T.Title, 
	T.Description, 
	T.CreationDate, 
	T.UpdateDate, 
	S.StatusName, 
	P.PriorityName,
	TH.ChangeDescription,
	T.ClientID, 
	T.RepID,
	T.TicketID
	
	from Tickets T
	inner join Statuses S on T.StatusID = S.StatusID
	inner join Priorities P on T.PriorityID = P.PriorityID
	inner join TicketHistory TH on T.TicketID = TH.TicketID
	where T.RepID = @RepID ;
end;

exec TicketList     @RepID = 6

alter procedure TicketList
    @RepID int
as
begin
    with LatestHistory as (
        select 
            TH.TicketID,
            TH.ChangeDescription,
            ROW_NUMBER() over (partition by TH.TicketID order by TH.EntryDate desc) as RowNum
        from TicketHistory TH
    )
    select  
        T.Title, 
        T.Description, 
        T.CreationDate, 
        T.UpdateDate, 
        S.StatusName, 
        P.PriorityName,
        LH.ChangeDescription,
        T.ClientID, 
        T.RepID,
        T.TicketID
    from Tickets T
    inner join Statuses S on T.StatusID = S.StatusID
    inner join Priorities P on T.PriorityID = P.PriorityID
    inner join LatestHistory LH on T.TicketID = LH.TicketID
    where T.RepID = @RepID 
      and LH.RowNum = 1; -- Seleccionar solo el registro más reciente
end;
