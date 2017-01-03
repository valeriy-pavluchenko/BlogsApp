IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Post_Update' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Post_Update as BEGIN SET NOCOUNT ON; END')
END
GO 

ALTER PROCEDURE usp_Post_Update
	@Title nvarchar(MAX),
	@Content nvarchar(MAX)
AS
BEGIN
	UPDATE
		Post
	SET
		Title = @Title,
		Content = @Content
END
