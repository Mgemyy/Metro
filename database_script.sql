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
CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [FullName] nvarchar(100) NOT NULL,
    [NationalId] nvarchar(14) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [Stations] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [LineNumber] int NOT NULL,
    CONSTRAINT [PK_Stations] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Subscriptions] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [StartStationId] int NOT NULL,
    [EndStationId] int NOT NULL,
    [Type] int NOT NULL,
    [Status] int NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [RequestDate] datetime2 NOT NULL,
    [StartDate] datetime2 NULL,
    [EndDate] datetime2 NULL,
    [PersonalPhotoPath] nvarchar(max) NOT NULL,
    [NationalIdPhotoPath] nvarchar(max) NOT NULL,
    [AdditionalDocumentPath] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Subscriptions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Subscriptions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Subscriptions_Stations_EndStationId] FOREIGN KEY ([EndStationId]) REFERENCES [Stations] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Subscriptions_Stations_StartStationId] FOREIGN KEY ([StartStationId]) REFERENCES [Stations] ([Id]) ON DELETE NO ACTION
);

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE INDEX [IX_Subscriptions_EndStationId] ON [Subscriptions] ([EndStationId]);

CREATE INDEX [IX_Subscriptions_StartStationId] ON [Subscriptions] ([StartStationId]);

CREATE INDEX [IX_Subscriptions_UserId] ON [Subscriptions] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260907180216_InitialCreate', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'LineNumber', N'Name') AND [object_id] = OBJECT_ID(N'[Stations]'))
    SET IDENTITY_INSERT [Stations] ON;
INSERT INTO [Stations] ([Id], [LineNumber], [Name])
VALUES (1, 1, N'Helwan'),
(2, 1, N'Maadi'),
(3, 1, N'Sadat'),
(4, 1, N'Shohadaa'),
(5, 1, N'New El-Marg'),
(6, 2, N'Shubra El-Kheima'),
(7, 2, N'Cairo University'),
(8, 2, N'Giza'),
(9, 2, N'El-Mounib'),
(10, 3, N'Adly Mansour'),
(11, 3, N'Abbassia'),
(12, 3, N'Attaba'),
(13, 3, N'Kit Kat');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'LineNumber', N'Name') AND [object_id] = OBJECT_ID(N'[Stations]'))
    SET IDENTITY_INSERT [Stations] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260907180522_SeedStations', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Subscriptions] ADD [EmployerName] nvarchar(max) NULL;

ALTER TABLE [Subscriptions] ADD [HrLetterPath] nvarchar(max) NULL;

ALTER TABLE [Subscriptions] ADD [IsFirstTime] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Subscriptions] ADD [IsUniversityStudent] bit NULL;

ALTER TABLE [Subscriptions] ADD [SchoolOrUniversityName] nvarchar(max) NULL;

ALTER TABLE [Subscriptions] ADD [StudentProofPath] nvarchar(max) NULL;

ALTER TABLE [Subscriptions] ADD [UserCategory] nvarchar(max) NOT NULL DEFAULT N'';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260908161419_AddSubscriptionDetailedFields', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260908161929_MakeCategoryFieldsNullable', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Subscriptions]') AND [c].[name] = N'AdditionalDocumentPath');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Subscriptions] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Subscriptions] ALTER COLUMN [AdditionalDocumentPath] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260908162430_MakeAdditionalDocumentPathNullable', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Subscriptions] ADD [AdminNotes] nvarchar(max) NULL;

ALTER TABLE [Subscriptions] ADD [ReviewedAt] datetime2 NULL;

ALTER TABLE [Subscriptions] ADD [ReviewedByAdminId] nvarchar(450) NULL;

CREATE TABLE [EmployeeProfiles] (
    [UserId] nvarchar(450) NOT NULL,
    [EmployeeCode] nvarchar(50) NOT NULL,
    [Department] nvarchar(100) NOT NULL,
    [OfficeLocation] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_EmployeeProfiles] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_EmployeeProfiles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Subscriptions_ReviewedByAdminId] ON [Subscriptions] ([ReviewedByAdminId]);

ALTER TABLE [Subscriptions] ADD CONSTRAINT [FK_Subscriptions_AspNetUsers_ReviewedByAdminId] FOREIGN KEY ([ReviewedByAdminId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260908175154_AddEmployeeProfileAndAuditFields', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [AspNetUsers] ADD [ProfilePicturePath] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260909151820_AddProfilePictureToUser', N'10.0.11');

COMMIT;
GO

