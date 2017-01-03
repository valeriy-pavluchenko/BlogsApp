IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_UserRole_GetById' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_UserRole_GetById as BEGIN SET NOCOUNT ON; END')
END
GO 

ALTER PROCEDURE usp_UserRole_GetById
	@UserRoleId int
AS
BEGIN
	SELECT
		UserRoleId,
		Name
	FROM 
		UserRole
	WHERE 
		UserRoleId = @UserRoleId
END
