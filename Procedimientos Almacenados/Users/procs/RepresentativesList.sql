alter procedure RepresentativesList
as 
begin
	select U.Name, UserID
	from Users U
	inner join Roles R
	ON U.RoleID = R.RoleID
	where U.RoleID = 2 -- 2 es el ID de representantes
end;

exec RepresentativesList
exec ClientsList
select * from Users;
select * from Roles;
