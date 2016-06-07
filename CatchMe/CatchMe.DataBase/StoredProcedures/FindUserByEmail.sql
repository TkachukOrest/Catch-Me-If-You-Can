CREATE PROCEDURE [dbo].[FindUserByEmail]
	@UserEmail NVARCHAR(256)
AS
BEGIN
	SELECT
		U.Id AS UserId,
		U.Email AS Email,
		U.EmailConfirmed AS EmailConfirmed,
		U.CreationTime AS CreationTime,
		U.PasswordHash AS PasswordHash,
		U.SecurityStamp AS SecurityStamp,
		U.UserName AS UserName,

		UP.Id AS UserProfileId,
		UP.FirstName AS FirstName,
		UP.LastName AS LastName,
		UP.PhoneNumber AS PhoneNumber
	FROM [User] U
	INNER JOIN [UserProfile] UP ON UP.UserId = U.Id
	WHERE U.Email = @UserEmail;
END