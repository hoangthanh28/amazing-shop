insert into Categories(ResourceId, Name)
select Id, N'Refrigerators'
from Resources with(nolock) where name = N'Home Appliances'
GO
insert into Categories(ResourceId, Name)
select Id, N'AirDresser'
from Resources with(nolock) where name = N'Home Appliances'
GO
insert into Categories(ResourceId, Name)
select Id, N'Washers & Dryers'
from Resources with(nolock) where name = N'Home Appliances'
GO
insert into Categories(ResourceId, Name)
select Id, N'Cooking Appliances'
from Resources with(nolock) where name = N'Home Appliances'
GO
insert into Categories(ResourceId, Name)
select Id, N'Dishwashers'
from Resources with(nolock) where name = N'Home Appliances'
GO
insert into Categories(ResourceId, Name)
select Id, N'Vacuum Cleaners'
from Resources with(nolock) where name = N'Home Appliances'
GO
insert into Categories(ResourceId, Name)
select Id, N'Smart Home'
from Resources with(nolock) where name = N'Home Appliances'
GO
insert into Categories(ResourceId, Name)
select Id, N'Climate Solutions'
from Resources with(nolock) where name = N'Home Appliances'
GO
insert into Categories(ResourceId, Name)
select Id, N'Offer'
from Resources with(nolock) where name = N'Home Appliances'
GO