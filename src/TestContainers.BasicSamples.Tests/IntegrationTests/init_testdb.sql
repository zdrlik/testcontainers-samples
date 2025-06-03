USE master
GO

DROP DATABASE IF EXISTS [TestDb]
GO

CREATE DATABASE [TestDb]
GO 

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'test_dbuser')
BEGIN
    CREATE LOGIN [test_dbuser] WITH PASSWORD=N'SqlonLinux?!', CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
END
GO
USE TestDb;
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'test_dbuser')
BEGIN
    CREATE USER [test_dbuser] FOR LOGIN [test_dbuser];
    EXEC sp_addrolemember N'db_datareader', [test_dbuser];
    EXEC sp_addrolemember N'db_datawriter', [test_dbuser];
END
GO

USE TestDb;
GO

-- Create Users table
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Users (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL   
);
GO

-- Insert test data
INSERT INTO Users (Id, Name, Email) VALUES (1, 'Alice', 'alice@example.com');
INSERT INTO Users (Id, Name, Email) VALUES (2, 'Bob', 'bob@example.com');
INSERT INTO Users (Id, Name, Email) VALUES (3, 'Charlie', 'charlie@example.com');
GO