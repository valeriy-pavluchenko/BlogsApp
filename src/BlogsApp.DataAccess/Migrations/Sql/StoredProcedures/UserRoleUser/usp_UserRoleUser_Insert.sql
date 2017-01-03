IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_UserRoleUser_Insert' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_UserRoleUser_Insert as BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE usp_UserRoleUser_Insert
	@UserId int,
	@UserRoleId int
AS
BEGIN
	INSERT INTO UserRoleUser
	(
		UserId,
		UserRoleId
	)
	VALUES
	(
		@UserId,
		@UserRoleId
	)
END
