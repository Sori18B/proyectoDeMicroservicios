-- Deshabilitar las restricciones FOREIGN KEY
ALTER TABLE TicketHistory DROP CONSTRAINT FK_TicketHistory_Tickets;
ALTER TABLE Assignments DROP CONSTRAINT FK_Assignments_Tickets;
ALTER TABLE Tickets DROP CONSTRAINT FK_Tickets_Statuses;
ALTER TABLE Tickets DROP CONSTRAINT FK_Tickets_Priorities;

DROP TABLE TicketHistory;
DROP TABLE Assignments;
DROP TABLE Tickets;
DROP TABLE Priorities;
DROP TABLE Statuses;
DROP TABLE PreliminaryTickets;
