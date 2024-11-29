alter procedure UserLogIn 
@Email nvarchar(150),	
@Password nvarchar(255)
as
begin
    SET NOCOUNT ON;

	select U.UserID, U.Name, U.Email, R.RoleName
	from users U 
	inner join Roles R
	on U.RoleID = R.RoleID
	where Email= @Email
	AND Password = @Password;
end;

exec UserLogIn @Email='ali@example.com', @Password='contraseña123'

select * from users

--Nuevooooooo

ALTER PROCEDURE UserLogIn 
@Email NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT U.UserID, U.Email, R.RoleID, R.RoleName, U.Password
    FROM users U
    INNER JOIN Roles R ON U.RoleID = R.RoleID
    WHERE U.Email = @Email;
END;
