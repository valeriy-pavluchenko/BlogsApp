IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_User_Insert' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_User_Insert as BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE usp_User_Insert
	@Email nvarchar(MAX),
	@PasswordHash nvarchar(MAX),
	@RegisteredOnUtc datetime
AS
BEGIN
	INSERT INTO [User]
	(
		Email,
		PasswordHash,
		RegisteredOnUtc
	)
	VALUES
	(
		@Email,
		@PasswordHash,
		@RegisteredOnUtc
	)

	SELECT SCOPE_IDENTITY()
END
