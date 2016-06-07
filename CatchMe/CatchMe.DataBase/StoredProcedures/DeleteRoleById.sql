CREATE PROCEDURE [dbo].[DeleteRoleById]
   @RoleId INT
AS
BEGIN
	DELETE FROM [Role]
	WHERE Id= @RoleId	
END
