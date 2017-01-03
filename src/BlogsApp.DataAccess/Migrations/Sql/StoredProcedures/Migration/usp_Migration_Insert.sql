IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Migration_Insert' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Migration_Insert as BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE usp_Migration_Insert
	@MigrationId int,
	@Name int,
	@AppliedOnUtc datetime
AS
BEGIN
	INSERT INTO Migration
	(
		MigrationId,
		Name,
		AppliedOnUtc
	)
	VALUES
	(
		@MigrationId,
		@Name,
		@AppliedOnUtc
	)

	SELECT SCOPE_IDENTITY()
END
