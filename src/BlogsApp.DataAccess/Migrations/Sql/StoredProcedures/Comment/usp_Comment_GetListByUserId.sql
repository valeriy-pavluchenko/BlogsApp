﻿IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Comment_GetListByUserId' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Comment_GetListByUserId as BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE usp_Comment_GetListByUserId
	@UserId int,
	@Limit int,
	@Offset int
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
		UserId = @UserId
	ORDER BY
		AddedOnUtc DESC
	OFFSET
		@Offset ROWS
	FETCH NEXT
		@Limit ROWS ONLY
END
