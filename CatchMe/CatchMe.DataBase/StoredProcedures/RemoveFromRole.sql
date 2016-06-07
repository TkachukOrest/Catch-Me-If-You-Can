CREATE PROCEDURE [dbo].[RemoveFromRole]
	@UserId INT,
	@RoleName NVARCHAR(256)
AS
BEGIN
	DECLARE @RoleId INT;

	SELECT @RoleId = Id 
	FROM [Role]
	WHERE Name = @RoleName;

	DELETE FROM [UserRole] 
	WHERE UserId = @UserId AND RoleId = @RoleId;
END
