ALTER procedure UsersByRole
@RoleID int
as
begin
	select u.UserID ,U.[Name],  U.Email,  U.[Password],  U.PhoneNumber,  R.RoleName 
	from users U
	inner join Roles R
	ON U.RoleID = R.RoleID
	where U.RoleID = @RoleID
end;

exec UsersByRole @RoleID=1

select * from users

