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

CREATE TABLE [Role] (
    [Id] INT NOT NULL IDENTITY,
    [Name] VARCHAR(255) NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL,
    CONSTRAINT [PK_Role_Id] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [User] (
    [Id] INT NOT NULL IDENTITY,
    [Username] NVARCHAR(255) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL,
    [Slug] NVARCHAR(255) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    [IsEmailVerified] BIT NOT NULL DEFAULT CAST(0 AS BIT),
    [CreatedAt] DATETIME2 NOT NULL,
    [UpdateAt] DATETIME2 NULL,
    CONSTRAINT [PK_User_Id] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [VerificationCode] (
    [Id] INT NOT NULL IDENTITY,
    [UserEmail] NVARCHAR(255) NOT NULL,
    [Code] INT NOT NULL,
    [Duration] DATETIME2 NOT NULL,
    CONSTRAINT [PK_VerificationCode_Id] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AuthToken] (
    [Id] INT NOT NULL IDENTITY,
    [AccessToken] NVARCHAR(800) NOT NULL,
    [RefreshToken] NVARCHAR(800) NOT NULL,
    [AccessTokenExpiry] DATETIME2 NOT NULL,
    [RefreshTokenExpiry] DATETIME2 NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL,
    [UpdatedAt] DATETIME2 NULL,
    [UserId] INT NOT NULL,
    CONSTRAINT [PK_AuthToken_Id] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AuthToken_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Event] (
    [Id] INT NOT NULL IDENTITY,
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [Slug] NVARCHAR(255) NOT NULL,
    [DateAndTime] DATETIME2 NOT NULL,
    [Location] NVARCHAR(255) NOT NULL,
    [MaxCapacity] INT NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL,
    [UpdatedAt] DATETIME2 NULL,
    [CreatorId] INT NOT NULL,
    CONSTRAINT [PK_Event_Id] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Event_Creator] FOREIGN KEY ([CreatorId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [RoleUser] (
    [RoleId] INT NOT NULL,
    [UserId] INT NOT NULL,
    CONSTRAINT [PK_RoleUser] PRIMARY KEY ([RoleId], [UserId]),
    CONSTRAINT [FK_RoleUser_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RoleUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [EventUser] (
    [EventId] INT NOT NULL,
    [UserId] INT NOT NULL,
    CONSTRAINT [PK_EventUser] PRIMARY KEY ([EventId], [UserId]),
    CONSTRAINT [FK_EventUser_EventId] FOREIGN KEY ([EventId]) REFERENCES [Event] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_EventUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Name') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] ON;
INSERT INTO [Role] ([Id], [CreatedAt], [Name])
VALUES (1, '2025-02-15T13:41:50.9600638-04:00', 'Admin'),
(2, '2025-02-15T13:41:50.9600684-04:00', 'User');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Name') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] OFF;
GO

CREATE INDEX [IX_AuthToken_Token] ON [AuthToken] ([AccessToken]);
GO

CREATE UNIQUE INDEX [IX_AuthToken_UserId] ON [AuthToken] ([UserId]);
GO

CREATE INDEX [IX_Event_CreatorId] ON [Event] ([CreatorId]);
GO

CREATE UNIQUE INDEX [UQ_Event_Name] ON [Event] ([Name]);
GO

CREATE INDEX [IX_EventUser_UserId] ON [EventUser] ([UserId]);
GO

CREATE UNIQUE INDEX [UQ_Role_Name] ON [Role] ([Name]);
GO

CREATE INDEX [IX_RoleUser_UserId] ON [RoleUser] ([UserId]);
GO

CREATE UNIQUE INDEX [UQ_User_Email] ON [User] ([Email]);
GO

CREATE UNIQUE INDEX [UQ_VerificationCode_UserEmail] ON [VerificationCode] ([UserEmail]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250215174151_StartingDatabase', N'8.0.1');
GO

COMMIT;
GO

