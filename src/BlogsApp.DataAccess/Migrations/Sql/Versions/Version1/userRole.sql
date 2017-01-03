IF (NOT EXISTS(SELECT 1 FROM UserRole WHERE UserRoleId = 1))
BEGIN
	INSERT INTO UserRole (UserRoleId, [Name]) VALUES (1, 'Admin')
END
IF (NOT EXISTS(SELECT 1 FROM UserRole WHERE UserRoleId = 2))
BEGIN
	INSERT INTO UserRole (UserRoleId, [Name]) VALUES (2, 'User')
END
