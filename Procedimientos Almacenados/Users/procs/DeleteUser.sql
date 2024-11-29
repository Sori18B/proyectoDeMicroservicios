Create procedure DeleteUser
@UserID int
AS
BEGIN
	begin Transaction; --Nueva transaccion

	delete from Users
	where UserID = @UserID

	if @@ROWCOUNT = 0
	begin
		rollback TRANSACTION;
		RAISERROR('Usuario con ID %d no encontrado.', 16, 1, @UserID);
    END
    ELSE
    BEGIN
        COMMIT TRANSACTION;
    END;
END;

exec DeleteUser @UserID=11

select COLUMN_NAME, DATA_TYPE
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = 'Users';

select COLUMN_NAME, DATA_TYPE
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = 'Roles';

select * from Users;
select * from Roles;