CREATE PROCEDURE [dbo].[FindRoleByName]
	@RoleName NVARCHAR(256)
AS
BEGIN
	SELECT 
		Id as RoleId,
		Name as RoleName
	FROM [Role]
	WHERE Name = @RoleName
END

