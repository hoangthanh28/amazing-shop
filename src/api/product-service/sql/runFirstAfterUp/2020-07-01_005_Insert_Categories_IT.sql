insert into Categories(ResourceId, Name)
select Id, N'Galaxy Book'
from Resources with(nolock) where name = N'IT'
GO
insert into Categories(ResourceId, Name)
select Id, N'Displays'
from Resources with(nolock) where name = N'IT'
GO
insert into Categories(ResourceId, Name)
select Id, N'Memory & Storage'
from Resources with(nolock) where name = N'IT'
GO
insert into Categories(ResourceId, Name)
select Id, N'Smart ways to shop'
from Resources with(nolock) where name = N'IT'
GO