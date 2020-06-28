-- product table
Create table Products (
    Id int identity(1,1),
    Name nvarchar(255) not null,
    CategoryId int not null,
    Barcode nvarchar(255) null,
CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
-- Category table
Create table Categories(
    Id int identity(1,1),
    Name nvarchar(255) not null,
    ResourceId int,
CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
-- Resources
Create table Resources(
    Id int identity(1,1),
    Name nvarchar(255) not null,
CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Product Class
Create table Classes(
    Id int identity(1,1),
    Name nvarchar(255) not null,
CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- add contraint
ALTER TABLE [dbo].[Products]  WITH CHECK ADD CONSTRAINT [FK_Products_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
-- check constraint
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_CategoryId]
GO

-- category and resource
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD CONSTRAINT [FK_Categories_ResourceId] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([Id])
ON DELETE CASCADE
GO
-- check constraint
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_ResourceId]
GO
