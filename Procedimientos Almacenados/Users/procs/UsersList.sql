Alter procedure UsersList
--sin par�metros
AS
BEGIN
	select U.[Name],  U.Email,  U.[Password],  U.PhoneNumber,  R.RoleName 
	from users U
	inner join Roles R
	ON R.RoleID = U.RoleID;
END;

exec UsersList

