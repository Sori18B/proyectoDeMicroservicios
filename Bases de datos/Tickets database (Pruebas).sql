use TicketsMicroService


select * from Statuses
select * from Priorities
select * from Tickets

select * from TicketHistory
select * from Assignments


INSERT INTO Statuses (StatusName) VALUES
    ('Abierto'),
    ('En proceso'),
    ('Cerrado'),
	('Reabierto');

INSERT INTO Priorities (PriorityName) VALUES
    ('Baja'),
    ('Media'),
    ('Alta'),
    ('Critica');

INSERT INTO Tickets (Title, Description, CreationDate, UpdateDate, StatusID, PriorityID, ClientID, RepID) VALUES
    ('Sitio web ca�do', 'El sitio web principal no es accesible.', GETDATE(), GETDATE(), 1, 3, 1, NULL),
    ('Tiempos de respuesta lentos', 'La aplicaci�n responde lentamente.', GETDATE(), GETDATE(), 2, 2, 2, 3),
    ('Solicitud de funcionalidad', 'Agregar una nueva funcionalidad a la aplicaci�n m�vil.', GETDATE(), GETDATE(), 1, 1, 3, NULL),
    ('Error en el inicio de sesi�n', 'Los usuarios no pueden iniciar sesi�n.', GETDATE(), GETDATE(), 3, 4, 4, 2),
    ('Error del servidor', 'El servidor est� arrojando errores 500.', GETDATE(), GETDATE(), 2, 3, 5, NULL);

INSERT INTO TicketHistory (TicketID, EntryDate, ChangeDescription, UserID) VALUES
    (1, GETDATE(), 'Ticket creado.', 1),
    (2, GETDATE(), 'Ticket asignado al Representante 3.', 1),
    (3, GETDATE(), 'Ticket priorizado a Alto.', 1),
    (4, GETDATE(), 'Ticket resuelto.', 2),
    (5, GETDATE(), 'Ticket actualizado con una nueva descripci�n.', 3);

INSERT INTO Assignments (TicketID, RepID, AssignmentDate) VALUES
    (2, 3, GETDATE()),
    (4, 2, GETDATE());