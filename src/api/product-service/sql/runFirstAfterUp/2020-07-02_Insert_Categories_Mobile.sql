insert into Categories(ResourceId, Name)
select Id, N'Smartphones'
from Resources with(nolock) where name = N'Mobile'
GO
insert into Categories(ResourceId, Name)
select Id, N'Tablets'
from Resources with(nolock) where name = N'Mobile'
GO
insert into Categories(ResourceId, Name)
select Id, N'Watches'
from Resources with(nolock) where name =N'Mobile'
GO
insert into Categories(ResourceId, Name)
select Id, N'Audio'
from Resources with(nolock) where name = N'Mobile'
GO
insert into Categories(ResourceId, Name)
select Id, N'Accessories'
from Resources with(nolock) where name = N'Mobile'
GO
insert into Categories(ResourceId, Name)
select Id, N'Apps & Services'
from Resources with(nolock) where name = N'Mobile'
GO
insert into Categories(ResourceId, Name)
select Id, N'Offer'
from Resources with(nolock) where name = N'Mobile'
GO