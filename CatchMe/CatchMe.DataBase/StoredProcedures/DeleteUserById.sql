CREATE PROCEDURE [dbo].[DeleteUserById]
	@UserId INT	
AS
BEGIN
	DELETE FROM [UserProfile] 
	WHERE UserId = @UserId

	DELETE FROM [User] 
	WHERE Id = @UserId
END	