IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Comment_Update' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Comment_Update as BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE usp_Comment_Update
	@CommentId int,
	@Message nvarchar(MAX)
AS
BEGIN
	UPDATE
		Comment
	SET
		[Message] = @Message
	WHERE
		CommentId = @CommentId
END
