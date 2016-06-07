CREATE PROCEDURE [dbo].[AddUserToRole]
	@UserId INT,
	@RoleName NVARCHAR(256)
AS
BEGIN
	DECLARE @RoleId INT;

	SELECT @RoleId = Id 
	FROM [Role]
	WHERE Name = @RoleName;

	INSERT INTO [UserRole](UserId, RoleId)
	VALUES (@UserId, @RoleId)
END
