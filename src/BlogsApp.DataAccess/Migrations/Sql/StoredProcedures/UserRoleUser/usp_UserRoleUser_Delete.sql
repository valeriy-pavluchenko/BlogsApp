IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_UserRoleUser_Delete' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_UserRoleUser_Delete as BEGIN SET NOCOUNT ON; END')
END
GO 

ALTER PROCEDURE usp_UserRoleUser_Delete
	@UserId int,
	@UserRoleId int
AS
BEGIN
	DELETE FROM
		UserRoleUser
	WHERE
		UserId = @UserId AND UserRoleId = @UserRoleId
END
