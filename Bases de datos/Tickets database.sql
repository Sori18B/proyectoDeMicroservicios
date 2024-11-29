--create database TicketsMicroServiceDB

--use TicketsMicroServiceDB

Create table Statuses(
StatusID int identity(1,1) PRIMARY KEY not null,
StatusName nvarchar(150)  not null
CONSTRAINT UQ_StatusName UNIQUE (StatusName)
);

Create table Priorities(
PriorityID int identity(1,1) PRIMARY KEY not null,
PriorityName nvarchar(150) not null
CONSTRAINT UQ_PriorityName UNIQUE (PriorityName)
);


Create table Tickets(
TicketID int identity(1,1) not null PRIMARY KEY,
Title		  nvarchar(255) not null,
[Description] nvarchar(MAX) not null,
CreationDate datetime not null,
UpdateDate datetime default GETDATE() not null,
StatusID int not null ,
PriorityID int not null ,
ClientID int not null ,
RepID int,
CONSTRAINT FK_Tickets_Statuses FOREIGN KEY (StatusID) REFERENCES Statuses(StatusID),
CONSTRAINT FK_Tickets_Priorities FOREIGN KEY (PriorityID) REFERENCES Priorities(PriorityID)
);

CREATE TABLE PreliminaryTickets (
    PreliminaryID INT IDENTITY(1,1) PRIMARY KEY,
    Title  NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    ClientID  INT NOT NULL,
    CreationDate  DATETIME DEFAULT GETDATE()
);

Create table TicketHistory(
HistoryID int identity(1,1) not null PRIMARY KEY,
TicketID int not null,
EntryDate datetime not null,
ChangeDescription nvarchar(255) not null,
UserID int not null,
CONSTRAINT FK_TicketHistory_Tickets FOREIGN KEY (TicketID) REFERENCES Tickets(TicketID)
);

Create table Assignments(
AssignmentID int identity(1,1) not null PRIMARY KEY,
TicketID int not null ,
RepID int not null ,
AssignmentDate datetime,
CONSTRAINT FK_Assignments_Tickets FOREIGN KEY (TicketID) REFERENCES Tickets(TicketID)
);

