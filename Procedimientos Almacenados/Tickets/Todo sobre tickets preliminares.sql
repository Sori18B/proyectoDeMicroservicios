

drop table PreliminaryTickets

create procedure CreatePreliminaryTicket
@Title			nvarchar(255),
@Description	NVARCHAR(MAX),
@ClientID		int
as
begin
	begin transaction
		insert into PreliminaryTickets(Title, Description, ClientID) VALUES
		(@Title, @Description, @ClientID);
	commit transaction;
end;

exec CreatePreliminaryTicket
    @Title = 'Se cayó la pagina',
    @Description = '¡No puedo acceder a la página principal! Que desastre',
    @ClientID = 4;

select * from PreliminaryTickets
select * from Tickets

delete from PreliminaryTickets where PreliminaryID = 26

alter procedure PreliminaryTicketsList
as
begin
	begin transaction
		select Title, Description, ClientID, CreationDate, PreliminaryID
		from PreliminaryTickets
	commit transaction;
end;

exec PreliminaryTicketsList

alter procedure PreliminaryTicketById
@PreliminaryID int
as
begin
	begin transaction
		select PreliminaryID, Title, Description, ClientID, CreationDate
		from PreliminaryTickets
		where PreliminaryID = @PreliminaryID
	commit transaction;
end;

exec PreliminaryTicketById @PreliminaryID = 4;