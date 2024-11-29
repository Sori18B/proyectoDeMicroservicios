create database UsersMicroService

use UsersMicroService

Create table Roles(
RoleID int identity(1,1) PRIMARY KEY not null,
RoleName nvarchar(150)
);

create table Users(
UserID int identity(1,1) PRIMARY KEY not null, 
[Name] nvarchar(255),
Email nvarchar(150) UNIQUE,
[Password] nvarchar(255),
PhoneNumber nvarchar (15),
RoleID int
CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);


drop table Roles