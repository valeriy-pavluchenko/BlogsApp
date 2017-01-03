IF NOT EXISTS (SELECT 1 FROM information_schema.Tables WHERE table_schema = 'dbo' AND TABLE_NAME = 'Post' )
BEGIN
	CREATE TABLE Post
	(
		PostId int NOT NULL Identity(1,1),
		UserId int NOT NULL,
		Title nvarchar(MAX) NOT NULL,
		Content nvarchar(MAX) NOT NULL,
		AddedOnUtc datetime NOT NULL
		CONSTRAINT PK_Post PRIMARY KEY CLUSTERED 
		(
			PostId ASC
		),
		CONSTRAINT FK_Post_User FOREIGN KEY (UserId) REFERENCES [User](UserId) ON DELETE CASCADE
	)
END

IF NOT EXISTS (SELECT 1 FROM information_schema.Tables WHERE table_schema = 'dbo' AND TABLE_NAME = 'Comment' )
BEGIN
	CREATE TABLE Comment
	(
		CommentId int NOT NULL Identity(1,1),
		PostId int NOT NULL,
		UserId int NOT NULL,
		[Message] nvarchar(MAX) NOT NULL,
		AddedOnUtc datetime NOT NULL
		CONSTRAINT PK_Comment PRIMARY KEY CLUSTERED 
		(
			CommentId ASC
		),
		CONSTRAINT FK_Comment_Post FOREIGN KEY (PostId) REFERENCES Post(PostId) ON DELETE CASCADE,
		CONSTRAINT FK_Comment_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
	)
END
