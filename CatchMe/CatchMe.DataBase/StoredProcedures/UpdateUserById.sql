CREATE PROCEDURE [dbo].[UpdateUserById]
	@UserId INT,
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
			UPDATE [User] SET
				Email = @Email, 
				EmailConfirmed = @EmailConfirmed, 
				PasswordHash= @PasswordHash, 
				SecurityStamp= @SecurityStamp,
				UserName= @UserName, 
				CreationTime = @CreationTime
			WHERE Id = @UserId

			UPDATE [UserProfile] SET
				FirstName = @FirstName,
				LastName = @LastName,
				PhoneNumber = @PhoneNumber
			WHERE UserId = @UserId									
		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		IF @@ERROR<>0 AND @@TRANCOUNT > 0
        ROLLBACK TRANSACTION

		THROW;
	END CATCH
END
