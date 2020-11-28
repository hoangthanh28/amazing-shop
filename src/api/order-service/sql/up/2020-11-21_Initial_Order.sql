-- product table
Create table Orders
(
    Id int identity(1,1),
    CustomerName nvarchar(255) not null,
    CustomerEmail nvarchar(255) not null,
    TotalAmount DECIMAL(13,2) not null,
    CreatedUtc datetime2 not null default getutcdate(),
    UpdatedUtc datetime2 not null default getutcdate(),
    Deleted bit not null default 0
        CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE table OrderDetails
(
    Id int identity(1,1),
    OrderId int not null,
    ProductId int not null,
    ProductName NVARCHAR(255) not null,
    Qty int not null,
    UnitPrice DECIMAL(13,2),
    CreatedUtc datetime2 not null default getutcdate(),
    UpdatedUtc datetime2 not null default getutcdate(),
    Deleted bit not null default 0
        CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
-- add contraint
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD CONSTRAINT [FK_OrderDetails_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
-- check constraint
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_OrderId]
GO