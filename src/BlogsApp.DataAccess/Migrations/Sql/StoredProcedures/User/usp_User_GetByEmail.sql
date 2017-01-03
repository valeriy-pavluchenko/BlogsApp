IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_User_GetByEmail' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_User_GetByEmail as BEGIN SET NOCOUNT ON; END')
END
GO 

ALTER PROCEDURE usp_User_GetByEmail
	@Email nvarchar(MAX)
AS
BEGIN
	SELECT
		UserId,
		Email,
		PasswordHash,
		RegisteredOnUtc
	FROM 
		[User]
	WHERE 
		Email = @Email
END
