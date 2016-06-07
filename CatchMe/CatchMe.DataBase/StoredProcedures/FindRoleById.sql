CREATE PROCEDURE [dbo].[FindRoleById]
	@RoleId INT
AS
BEGIN
	SELECT 
		Id as RoleId,
		Name as RoleName
	FROM [Role]
	WHERE Id = @RoleId
END
