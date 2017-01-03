IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_UserRole_GetListByUserId' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_UserRole_GetListByUserId as BEGIN SET NOCOUNT ON; END')
END
GO 

ALTER PROCEDURE usp_UserRole_GetListByUserId
	@UserId int
AS
BEGIN
	SELECT
		ur.UserRoleId,
		ur.Name
	FROM
		UserRole AS ur
	INNER JOIN
		UserRoleUser AS uru
	ON
		ur.UserRoleId = uru.UserRoleId
	WHERE
		uru.UserId = @UserId
END