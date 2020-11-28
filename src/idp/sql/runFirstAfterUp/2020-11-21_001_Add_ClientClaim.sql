insert into ClientClaims
    ([Type],[Value], ClientId)
select
    'tenants'
    , '[{"tenantId":"304D47F1-B262-4BD9-A2FD-04210AC230B7","domain":"ms"}]'
    , id
from clients
where clientId='58EC0B4B-FCBF-49AF-B1A3-BD8C47D78DB3'    