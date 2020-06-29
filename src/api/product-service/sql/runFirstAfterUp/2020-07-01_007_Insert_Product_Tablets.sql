insert into Products(CategoryId, Name)
select Id, N'Galaxy Tab S6 Lite'
from Categories with(nolock) where name = N'Tablets'
GO
insert into Products(CategoryId, Name)
select Id, N'Tab S6'
from Categories with(nolock) where name = N'Tablets'
GO
insert into Products(CategoryId, Name)
select Id, N'Tab S5e'
from Categories with(nolock) where name = N'Tablets'
GO