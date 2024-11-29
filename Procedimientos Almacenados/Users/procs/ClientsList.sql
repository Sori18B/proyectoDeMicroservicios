create procedure ClientsList
as 
begin
	select U.Name, R.RoleName
	from Users U
	inner join Roles R
	ON U.RoleID = R.RoleID
	where U.RoleID = 3 -- 3 es el ID de clientes
end;

exec ClientsList