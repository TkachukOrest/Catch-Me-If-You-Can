CREATE PROCEDURE [dbo].[GetUserRoles]
	@UserId INT
AS	
BEGIN
	SELECT 		
		R.Name as RoleName
	FROM [Role] R
	INNER JOIN [UserRole] UR on R.Id = UR.RoleId;
END
