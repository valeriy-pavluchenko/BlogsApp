IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Migration_GetList' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Migration_GetList as BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE usp_Migration_GetList
AS
BEGIN
	SELECT
		MigrationId,
		Name
	FROM
		Migration
END
