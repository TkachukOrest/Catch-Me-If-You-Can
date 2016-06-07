CREATE PROCEDURE [dbo].[AddUser]
	@Email  NVARCHAR (256),
	@EmailConfirmed BIT,
	@PasswordHash 	NVARCHAR (MAX),
	@SecurityStamp NVARCHAR (MAX),
	@UserName NVARCHAR (256),
	@CreationTime DATETIME,
	@FirstName  NVARCHAR (128), 
	@LastName  NVARCHAR (128),
	@PhoneNumber  NVARCHAR (128)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION	
			INSERT INTO [User](Email, EmailConfirmed, PasswordHash, SecurityStamp, UserName, CreationTime)
			VALUES (@Email, @EmailConfirmed, @PasswordHash, @SecurityStamp, @UserName, @CreationTime);

			DECLARE @UserId INT;
			SELECT @UserId = SCOPE_IDENTITY();

			INSERT INTO [UserProfile](UserId, FirstName, LastName, PhoneNumber)
			VALUES (@UserId, @FirstName, @LastName, @PhoneNumber);

			SELECT @UserId;
		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		IF @@ERROR<>0 AND @@TRANCOUNT > 0
        ROLLBACK TRANSACTION

		THROW;
	END CATCH
END
