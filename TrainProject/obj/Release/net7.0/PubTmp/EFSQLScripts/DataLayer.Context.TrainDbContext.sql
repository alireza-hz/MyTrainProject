IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE TABLE [Categories] (
        [MyCategoryID] int NOT NULL IDENTITY,
        [description] nvarchar(50) NOT NULL,
        [Createdate] datetime2 NOT NULL,
        [ModifiedDate] datetime2 NOT NULL,
        [Isdelete] bit NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([MyCategoryID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE TABLE [Levels] (
        [MYlevelID] int NOT NULL IDENTITY,
        [Description] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Levels] PRIMARY KEY ([MYlevelID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE TABLE [MyRoles] (
        [MyRoleID] int NOT NULL IDENTITY,
        [RoleName] nvarchar(300) NOT NULL,
        [RoleTitle] nvarchar(300) NOT NULL,
        [IsDelete] bit NOT NULL,
        [CreateDate] datetime2 NULL,
        [ModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_MyRoles] PRIMARY KEY ([MyRoleID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE TABLE [Transactions] (
        [IdTransaction] int NOT NULL IDENTITY,
        [Price] int NOT NULL,
        [IsDelete] bit NOT NULL,
        [CreateDate] datetime2 NULL,
        CONSTRAINT [PK_Transactions] PRIMARY KEY ([IdTransaction])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE TABLE [MyProducts] (
        [MyProductId] int NOT NULL IDENTITY,
        [MyCategoryID] int NOT NULL,
        [MYlevelID] int NOT NULL,
        [ProductName] nvarchar(100) NOT NULL,
        [Price] int NOT NULL,
        [DurationData] nvarchar(max) NOT NULL,
        [CountVideo] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Createdate] datetime2 NOT NULL,
        [ModifiedDate] datetime2 NOT NULL,
        [Isdelete] bit NOT NULL,
        [Avatar] nvarchar(max) NULL,
        CONSTRAINT [PK_MyProducts] PRIMARY KEY ([MyProductId]),
        CONSTRAINT [FK_MyProducts_Categories_MyCategoryID] FOREIGN KEY ([MyCategoryID]) REFERENCES [Categories] ([MyCategoryID]) ON DELETE CASCADE,
        CONSTRAINT [FK_MyProducts_Levels_MYlevelID] FOREIGN KEY ([MYlevelID]) REFERENCES [Levels] ([MYlevelID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE TABLE [MyUsers] (
        [MYUserID] int NOT NULL IDENTITY,
        [UserName] nvarchar(300) NULL,
        [Mobile] nvarchar(100) NULL,
        [Avatar] nvarchar(50) NOT NULL,
        [Address] nvarchar(max) NULL,
        [ForMe] nvarchar(max) NULL,
        [IsDelete] bit NOT NULL,
        [CreateDate] datetime2 NOT NULL,
        [ModifiedDate] datetime2 NOT NULL,
        [MyRoleID] int NOT NULL,
        CONSTRAINT [PK_MyUsers] PRIMARY KEY ([MYUserID]),
        CONSTRAINT [FK_MyUsers_MyRoles_MyRoleID] FOREIGN KEY ([MyRoleID]) REFERENCES [MyRoles] ([MyRoleID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE TABLE [Items] (
        [ItemId] int NOT NULL IDENTITY,
        [MYUserID] int NOT NULL,
        [MyProductId] int NOT NULL,
        [IdTransaction] int NOT NULL,
        CONSTRAINT [PK_Items] PRIMARY KEY ([ItemId]),
        CONSTRAINT [FK_Items_MyProducts_MyProductId] FOREIGN KEY ([MyProductId]) REFERENCES [MyProducts] ([MyProductId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Items_MyUsers_MYUserID] FOREIGN KEY ([MYUserID]) REFERENCES [MyUsers] ([MYUserID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Items_Transactions_IdTransaction] FOREIGN KEY ([IdTransaction]) REFERENCES [Transactions] ([IdTransaction]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE INDEX [IX_Items_IdTransaction] ON [Items] ([IdTransaction]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE INDEX [IX_Items_MyProductId] ON [Items] ([MyProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE INDEX [IX_Items_MYUserID] ON [Items] ([MYUserID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE INDEX [IX_MyProducts_MyCategoryID] ON [MyProducts] ([MyCategoryID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE INDEX [IX_MyProducts_MYlevelID] ON [MyProducts] ([MYlevelID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    CREATE INDEX [IX_MyUsers_MyRoleID] ON [MyUsers] ([MyRoleID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327025748_create')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240327025748_create', N'7.0.14');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327030704_deletetransaction')
BEGIN
    ALTER TABLE [Items] DROP CONSTRAINT [FK_Items_Transactions_IdTransaction];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327030704_deletetransaction')
BEGIN
    DROP TABLE [Transactions];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327030704_deletetransaction')
BEGIN
    DROP INDEX [IX_Items_IdTransaction] ON [Items];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327030704_deletetransaction')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'IdTransaction');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Items] DROP COLUMN [IdTransaction];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327030704_deletetransaction')
BEGIN
    ALTER TABLE [Items] ADD [CreateDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327030704_deletetransaction')
BEGIN
    ALTER TABLE [Items] ADD [IsConfirm] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240327030704_deletetransaction')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240327030704_deletetransaction', N'7.0.14');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240330070712_createtblvisite')
BEGIN
    CREATE TABLE [visits] (
        [visitid] int NOT NULL IDENTITY,
        [IPAddress] nvarchar(max) NOT NULL,
        [timevisit] datetime2 NOT NULL,
        CONSTRAINT [PK_visits] PRIMARY KEY ([visitid])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240330070712_createtblvisite')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240330070712_createtblvisite', N'7.0.14');
END;
GO

COMMIT;
GO

