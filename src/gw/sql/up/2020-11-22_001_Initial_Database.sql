CREATE TABLE [dbo].[Tenants]
(
	[Id] [uniqueidentifier] NOT NULL DEFAULT (NEWSEQUENTIALID()) PRIMARY KEY,
	[Name] [nvarchar](255) NOT NULL,
	[Domain] nvarchar(20) NOT NULL,
	[CreatedUtc] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
	[UpdatedUtc] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
	[Deleted] BIT NOT NULL DEFAULT 0
)
GO
CREATE TABLE [dbo].[Shardings]
(
	[Id] [int] Identity(1,1) PRIMARY KEY,
	[TenantId] [uniqueidentifier] NOT NULL,
	[ServiceName] [nvarchar](255) NOT NULL,
	[EnvironmentName] [nvarchar](255) NOT NULL,
	[ServerName] [nvarchar](255) NOT NULL,
	[DatabaseName] [nvarchar](255) NOT NULL,
	[CreatedUtc] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
	[UpdatedUtc] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
	[Deleted] BIT NOT NULL DEFAULT 0
)

GO
ALTER TABLE [dbo].[Shardings]  WITH CHECK ADD  CONSTRAINT [FK_Shardings_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([Id])
GO
ALTER TABLE [dbo].[Shardings] CHECK CONSTRAINT [FK_Shardings_TenantId]
GO
