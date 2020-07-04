Delete from ProductImages
GO
alter table ProductImages add Name nvarchar(255) not null
GO
alter table ProductImages add ContentType nvarchar(255) not null
GO