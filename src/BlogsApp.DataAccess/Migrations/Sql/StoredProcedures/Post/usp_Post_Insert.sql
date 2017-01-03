IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Post_Insert' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Post_Insert as BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE usp_Post_Insert
	@UserId int,
	@Title nvarchar(MAX),
	@Content nvarchar(MAX),
	@AddedOnUtc datetime
AS
BEGIN
	INSERT INTO Post
	(
		UserId,
		Title,
		Content,
		AddedOnUtc
	)
	VALUES
	(
		@UserId,
		@Title,
		@Content,
		@AddedOnUtc
	)

	SELECT SCOPE_IDENTITY()
END
