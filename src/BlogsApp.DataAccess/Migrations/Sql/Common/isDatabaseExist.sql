DECLARE @isDatabaseExist bit
IF EXISTS(select * from sys.databases where name = @DatabaseName)
	SET @isDatabaseExist = 1
ELSE
	SET @isDatabaseExist = 0

SELECT @isDatabaseExist
