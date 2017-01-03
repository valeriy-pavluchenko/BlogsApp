IF NOT EXISTS (SELECT 1 FROM information_schema.Routines WHERE ROUTINE_NAME = 'usp_Post_GetList' AND ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo')
BEGIN
	EXEC('CREATE PROCEDURE usp_Post_GetList as BEGIN SET NOCOUNT ON; END')
END
GO 

ALTER PROCEDURE usp_Post_GetList
	@Limit int,
	@Offset int
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
	ORDER BY
		AddedOnUtc DESC
	OFFSET
		@Offset ROWS
	FETCH NEXT
		@Limit ROWS ONLY
END
