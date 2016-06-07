CREATE PROCEDURE [dbo].[GetRoles]
AS
BEGIN
	SELECT 
		Id as RoleId,
		Name as RoleName
	FROM [Role];
END
