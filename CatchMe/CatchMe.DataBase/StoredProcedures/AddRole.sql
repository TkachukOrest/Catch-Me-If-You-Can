﻿CREATE PROCEDURE [dbo].[AddRole]
   @RoleName NVARCHAR(256)
AS
BEGIN
	INSERT INTO [Role](Name)
	VALUES (@RoleName)

	SELECT SCOPE_IDENTITY();
END
