alter procedure UpdateUser
@UserID int,
@Name nvarchar(255),
@Email nvarchar(150),
@Password nvarchar(255),
@PhoneNumber nvarchar(15)
AS
BEGIN
	Update Users
	set 
	[Name] = @Name, 
	Email = @Email, 
	[Password] = @Password, 
	PhoneNumber = @PhoneNumber 
	WHERE UserID = @UserID;
END;

EXEC UpdateUser 
    @UserID = 2, 
    @Name = 'Carolina Pedraza', 
    @Email = 'carito@example.com', 
    @Password = 'newSecurePassword', 
    @PhoneNumber = '987-654-3210';

	select * from users