insert into Categories(ResourceId, Name)
select Id, N'TV'
from Resources with(nolock) where name = N'TV & Audio'
GO
insert into Categories(ResourceId, Name)
select Id, N'Lifestyle TV'
from Resources with(nolock) where name = N'TV & Audio'
GO
insert into Categories(ResourceId, Name)
select Id, N'TV by Size'
from Resources with(nolock) where name = N'TV & Audio'
GO
insert into Categories(ResourceId, Name)
select Id, N'Sound Device'
from Resources with(nolock) where name = N'TV & Audio'
GO
insert into Categories(ResourceId, Name)
select Id, N'Offer'
from Resources with(nolock) where name = N'TV & Audio'
GO