insert into Products(CategoryId, Name)
select Id, N'Galaxy Z Flip Thom Browne Edition'
from Categories with(nolock) where name = N'Smartphones'
GO
insert into Products(CategoryId, Name)
select Id, N'Galaxy Z Flip'
from Categories with(nolock) where name = N'Smartphones'
GO
insert into Products(CategoryId, Name)
select Id, N'Galaxy Fold 5G'
from Categories with(nolock) where name = N'Smartphones'
GO