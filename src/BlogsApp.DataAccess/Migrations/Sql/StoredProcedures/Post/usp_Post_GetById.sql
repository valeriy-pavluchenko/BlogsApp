IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Post_GetById' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Post_GetById as BEGIN SET NOCOUNT ON; END')
END
GO 

ALTER PROCEDURE usp_Post_GetById
	@PostId int
AS
BEGIN
	SELECT
		PostId,
		UserId,
		Title,
		Content,
		AddedOnUtc
	FROM 
		Post
	WHERE 
		PostId = @PostId
END
