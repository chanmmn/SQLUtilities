CREATE DATABASE [backupdemo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'backupdemo', FILENAME = N'c:\database\backupdemo.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'backupdemo_log', FILENAME = N'c:\database\backupdemo_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
GO

USE backupdemo

CREATE TABLE [Test](
   [c1] int identity,
   [c2] varchar(100)
)

Insert INTO [Test] VALUES ('transcation 1')

BACKUP Database [backupdemo] TO DISK = N'c:\database\backup\backupdemo_Full.bak' WITH INIT

Insert INTO [Test] VALUES ('transcation 2')

Insert INTO [Test] VALUES ('transcation 3')

BACKUP LOG [backupdemo] TO DISK = N'c:\database\backup\backupdemo_Log.bak' WITH INIT

Insert INTO [Test] VALUES ('transcation 4')

Insert INTO [Test] VALUES ('transcation 5')

SELECT * FROM [backupdemo].[dbo].[Test]

-- simulate crash
USE MASTER
Go
Alter database [backupdemo] set single_user with rollback immediate  
Go
Alter database [backupdemo] SET OFFLINE
GO
-- Delete backupdemo mdf 
EXECUTE master.dbo.sp_configure N'show advanced options', 1; RECONFIGURE
EXECUTE master.dbo.sp_configure N'xp_cmdshell', 1; RECONFIGURE
EXECUTE master.dbo.xp_cmdshell N'Del c:\database\backupdemo.mdf'
EXECUTE master.dbo.sp_configure N'xp_cmdshell', 0; RECONFIGURE
EXECUTE master.dbo.sp_configure N'show advanced options', 0; RECONFIGURE

Alter database [backupdemo] SET ONLINE

BACKUP LOG [backupdemo] TO DISK = N'c:\database\backup\backupdemo_Log_Tail.bak' 
WITH NO_TRUNCATE, COPY_ONLY;

Restore database [backupdemo] 
FROM DISK = N'c:\database\backup\backupdemo_Full.bak' 
WITH
  MOVE N'backupdemo' TO N'c:\database\backupdemo_Copy.mdf',
  MOVE N'backupdemo_log' TO N'c:\database\backupdemo_Copy_log.ldf',
  REPLACE, NORECOVERY;
GO

Restore LOG [backupdemo] 
FROM DISK = N'c:\database\backup\backupdemo_Log.bak' 
WITH NORECOVERY;
GO

Restore database [backupdemo] WITH RECOVERY;
GO

SELECT * FROM [backupdemo].[dbo].[Test]

--BACKUP LOG [backupdemo] TO DISK = N'c:\database\backup\backupdemo_Log_Tail.bak' 
--WITH NO_TRUNCATE, COPY_ONLY;

Restore database [backupdemo] 
FROM DISK = N'c:\database\backup\backupdemo_Full.bak' 
WITH
  MOVE N'backupdemo' TO N'c:\database\backupdemo.mdf',
  MOVE N'backupdemo_log' TO N'c:\database\backupdemo_log.ldf',
  REPLACE, NORECOVERY;
GO

Restore LOG [backupdemo] 
FROM DISK = N'c:\database\backup\backupdemo_Log.bak' 
WITH NORECOVERY;
GO

Restore LOG [backupdemo] 
FROM DISK = N'c:\database\backup\backupdemo_Log_Tail.bak' 
WITH RECOVERY;
GO

SELECT * FROM [backupdemo].[dbo].[Test]