CREATE PROCEDURE [dbo].[UpdateRoleById]
	@RoleId INT,
	@RoleName NVARCHAR(256)
AS
BEGIN
	UPDATE [Role]
	SET Name = @RoleName
	WHERE Id = @RoleId
END
