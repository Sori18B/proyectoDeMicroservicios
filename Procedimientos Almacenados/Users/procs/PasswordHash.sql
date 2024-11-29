create procedure PasswordHash
@Email nvarchar(155)
as
begin
	begin transaction
		select [Password]
		from Users
		where Email = @Email
	commit transaction;
end;

exec PasswordHash @Email= 'altex@gmail.com';