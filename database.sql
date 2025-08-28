-- This script is for SQL Server, but is easily adaptable for PostgreSQL, MySQL, etc.

-- Check if the table already exists and drop it to start fresh (for testing)
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
DROP TABLE dbo.Users;
GO

-- Create the Users table
CREATE TABLE dbo.Users (
    -- Id: Primary key to uniquely identify each user.
    -- INT: Integer data type.
    -- IDENTITY(1,1): Auto-increments the value, starting at 1 and incrementing by 1.
    -- PRIMARY KEY: Enforces uniqueness and creates a clustered index.
    Id INT IDENTITY(1,1) PRIMARY KEY,

    -- Name: Stores the user's full name.
    -- NVARCHAR(100): A variable-length string that supports Unicode characters, up to 100 characters long.
    -- NOT NULL: This column must have a value.
    Name NVARCHAR(100) NOT NULL,

    -- Email: Stores the user's email address.
    -- NVARCHAR(100): Using NVARCHAR for consistency.
    -- NOT NULL: This column must have a value.
    -- UNIQUE: Ensures that every email address in this table is unique.
    Email NVARCHAR(100) NOT NULL UNIQUE,

    -- CreatedAt: Timestamp for when the record was created.
    -- DATETIME2: A precise date and time data type.
    -- DEFAULT GETUTCDATE(): Automatically sets the value to the current UTC time when a new row is inserted.
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
GO

-- (Optional) Insert some initial data for testing
INSERT INTO dbo.Users (Name, Email) VALUES
('Alice Smith', 'alice@example.com'),
('Bob Johnson', 'bob@example.com'),
('Charlie Brown', 'charlie@example.com');
GO

-- Verify the data was inserted
SELECT * FROM dbo.Users;
GO

