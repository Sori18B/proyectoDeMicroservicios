Create procedure RegisterUser
@Name nvarchar(255),
@Email nvarchar(150),
@Password nvarchar(255),
@PhoneNumber nvarchar(15),
@RoleID int
AS
BEGIN
	Insert into Users(Name, Email, Password, PhoneNumber, RoleID) VALUES
	(@Name, @Email, @Password, @PhoneNumber, @RoleID)
END;

exec RegisterUser @Name='assaas', @Email='assa', @Password='asa', @PhoneNumber='7946132582', @RoleID=1;
