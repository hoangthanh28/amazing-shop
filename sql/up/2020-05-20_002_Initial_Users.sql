Create table Users(
Id uniqueidentifier default newsequentialid() primary key,
UserName nvarchar(255) not null,
Email nvarchar(255) not null, 
Password nvarchar(2048),
FirstName nvarchar(255),
LastName nvarchar(255),
IsMustChangePassword bit not null default 1
)
GO
