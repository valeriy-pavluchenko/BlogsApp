IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Comment_Insert' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Comment_Insert as BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE usp_Comment_GetById
	@PostId int,
	@UserId int,
	@Message nvarchar(MAX),
	@AddedOnUtc datetime
AS
BEGIN
	INSERT INTO Comment
	(
		PostId,
		UserId,
		[Message],
		AddedOnUtc
	)
	VALUES
	(
		@PostId,
		@UserId,
		@Message,
		@AddedOnUtc
	)

	SELECT SCOPE_IDENTITY()
END
