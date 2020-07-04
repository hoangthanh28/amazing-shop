-- product image table
Create table ProductImages (
    Id int identity(1,1),
    ProductId int not null,
    Name nvarchar(255) not null,
    Url nvarchar(255) null,
    ContentType nvarchar(255) not null,
    CreatedUtc datetime2 not null default getutcdate(),
    UpdatedUtc datetime2 not null default getutcdate(),
    Deleted bit not null default 0
CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- add contraint
ALTER TABLE [dbo].[ProductImages]  WITH CHECK ADD CONSTRAINT [FK_ProductImages_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
-- check constraint
ALTER TABLE [dbo].[ProductImages] CHECK CONSTRAINT [FK_ProductImages_ProductId]
GO