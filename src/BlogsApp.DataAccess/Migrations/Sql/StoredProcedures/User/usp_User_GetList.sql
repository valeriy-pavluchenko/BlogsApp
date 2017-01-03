IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_User_GetList' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_User_GetList as BEGIN SET NOCOUNT ON; END')
END
GO 

ALTER PROCEDURE usp_User_GetList
	@Limit int,
	@Offset int
AS
BEGIN
	SELECT
		UserId,
		Email,
		PasswordHash,
		RegisteredOnUtc
	FROM
		[User]
	ORDER BY
		Email ASC
	OFFSET
		@Offset ROWS
	FETCH NEXT
		@Limit ROWS ONLY
END
