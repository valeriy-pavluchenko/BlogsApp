IF NOT EXISTS (SELECT 1 FROM information_schema.Tables WHERE table_schema = 'dbo' AND TABLE_NAME = 'Migration' )
BEGIN
	CREATE TABLE Migration
	(
		MigrationId int NOT NULL,
		Name nvarchar(MAX) NOT NULL,
		AppliedOnUtc datetime NOT NULL
		CONSTRAINT PK_Migration PRIMARY KEY CLUSTERED 
		(
			MigrationId ASC
		)
	)
END
