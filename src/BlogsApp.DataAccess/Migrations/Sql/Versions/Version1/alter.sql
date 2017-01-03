IF NOT EXISTS (SELECT 1 FROM information_schema.Tables WHERE table_schema = 'dbo' AND TABLE_NAME = 'User' )
BEGIN
	CREATE TABLE [User]
	(
		UserId int NOT NULL Identity(1,1),
		Email varchar(255) NOT NULL,
		PasswordHash nvarchar(MAX) NOT NULL,
		RegisteredOnUtc datetime NOT NULL
		CONSTRAINT PK_User PRIMARY KEY CLUSTERED 
		(
			UserId ASC
		),
		CONSTRAINT [UC_User_Email] UNIQUE NONCLUSTERED 
		(
		[Email] ASC
		),
		CONSTRAINT FK_User_UserRoleUser FOREIGN KEY (UserId) REFERENCES [User](UserId)
	)
END

IF NOT EXISTS (SELECT 1 FROM information_schema.Tables WHERE table_schema = 'dbo' AND TABLE_NAME = 'UserRole' )
BEGIN
	CREATE TABLE UserRole
	(
		UserRoleId int NOT NULL,
		Name nvarchar(MAX) NOT NULL
		CONSTRAINT PK_UserRole PRIMARY KEY CLUSTERED 
		(
			UserRoleId ASC
		),
		CONSTRAINT FK_UserRole_UserRoleUser FOREIGN KEY (UserRoleId) REFERENCES UserRole(UserRoleId)
	)
END

IF NOT EXISTS (SELECT 1 FROM information_schema.Tables WHERE table_schema = 'dbo' AND TABLE_NAME = 'UserRoleUser' )
BEGIN
	CREATE TABLE UserRoleUser
	(
		UserId int NOT NULL,
		UserRoleId int NOT NULL
		CONSTRAINT PK_UserRoleUser PRIMARY KEY CLUSTERED 
		(
			UserId ASC, UserRoleId ASC
		),
		CONSTRAINT FK_UserRoleUser_User FOREIGN KEY (UserId) REFERENCES [User](UserId) ON DELETE CASCADE,
		CONSTRAINT FK_UserRoleUser_UserRole FOREIGN KEY (UserRoleId) REFERENCES UserRole(UserRoleId) ON DELETE CASCADE
	)
END
