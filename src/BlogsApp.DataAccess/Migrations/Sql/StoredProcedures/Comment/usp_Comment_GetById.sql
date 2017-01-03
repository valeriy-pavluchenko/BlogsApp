IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Comment_GetById' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Comment_GetById as BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE usp_Comment_GetById
	@CommentId int
AS
BEGIN
	SELECT
		CommentId,
		PostId,
		UserId,
		[Message],
		AddedOnUtc
	FROM 
		Comment
	WHERE 
		CommentId = @CommentId
END
