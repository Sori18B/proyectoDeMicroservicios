Alter procedure UsersListById
@UserID int
AS
BEGIN
	select U.[Name],  U.Email,  U.[Password],  U.PhoneNumber,  R.RoleName 
	from users U
	inner join Roles R
	ON R.RoleID = U.RoleID
	where U.UserID = @UserID;
End;

exec UsersListById @UserID = 3;