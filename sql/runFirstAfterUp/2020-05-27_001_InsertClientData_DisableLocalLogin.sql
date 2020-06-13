update clients set EnableLocalLogin = 1 where description = N'Mvc application'
GO
delete from ClientIdPRestrictions
GO
-- insert into ClientIdPRestrictions( ClientId, Provider)
-- select id, 'aad' from clients where Description = N'Mvc application'
-- GO
-- insert into ClientIdPRestrictions( ClientId, Provider)
-- select id, 'Google' from clients where Description = N'Mvc application'
-- GO
-- insert into ClientIdPRestrictions( ClientId, Provider)
-- select id, 'Facebook' from clients where Description = N'Mvc application'
-- GO