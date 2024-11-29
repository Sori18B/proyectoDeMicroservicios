use UsersMicroService

select * from  Users
select * from Roles

INSERT INTO Roles (RoleName) VALUES
('Administrador'),
('Representante'),
('Cliente');

INSERT INTO Users ([Name], Email, [Password], PhoneNumber, RoleID) VALUES
    -- Administrador
    ('Juan Pérez Hernandez', 'juan.perez@admin.com', 'admin123', '5551000001', 1),
    
    -- Representantes
    ('Alison Espinosa Prado', 'ali@example.com', 'ali123', '5552000001', 2),
    ('Luis Hernández', 'luis.hernandez@empresa.com', 'pass456', '5552000002', 2),
    ('María González', 'maria.gonzalez@empresa.com', 'pass789', '5552000003', 2),
    ('Carlos Martínez', 'carlos.martinez@empresa.com', 'password1', '5552000004', 2),
    ('Paula Sánchez', 'paula.sanchez@empresa.com', 'password2', '5552000005', 2),
    
    -- Clientes
    ('Carolina Pedraza Ramirez', 'carito@example.com', 'caro123', '5553000001', 3),
    ('Fernanda Jiménez', 'fernanda.jimenez@cliente.com', 'cliente456', '5553000002', 3),
    ('Jorge Castillo', 'jorge.castillo@cliente.com', 'cliente789', '5553000003', 3),
    ('Laura Morales', 'laura.morales@cliente.com', 'cliente000', '5553000004', 3),
    ('Andrea Torres', 'andrea.torres@cliente.com', 'mi_contra', '5553000005', 3),
    ('José Flores', 'jose.flores@cliente.com', 'clave123', '5553000006', 3),
    ('Camila Cruz', 'camila.cruz@cliente.com', 'prueba123', '5553000007', 3),
    ('Diego Navarro', 'diego.navarro@cliente.com', 'clave456', '5553000008', 3),
    ('Sofía Vargas', 'sofia.vargas@cliente.com', 'clave789', '5553000009', 3);


delete from Roles
delete from Users

drop table Roles
drop table Users


select COLUMN_NAME, DATA_TYPE
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = 'Users'