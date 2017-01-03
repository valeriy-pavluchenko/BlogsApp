﻿IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Comment_Delete' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Comment_Delete as BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE usp_Comment_Delete
	@CommentId int
AS
BEGIN
	DELETE FROM
		Comment
	WHERE
		CommentId = @CommentId
END
