/****** Insert master data for SubTenants ******/
INSERT INTO dbo.Shardings
    (
    TenantId
    ,ServiceName
    ,EnvironmentName
    ,ServerName
    ,DatabaseName)
VALUES
    ('304d47f1-b262-4bd9-a2fd-04210ac230b7'
    , 'product-service'
    , 'Development'
    , 'db'
    , 'Product_MS')
,
    ('304d47f1-b262-4bd9-a2fd-04210ac230b7'
    , 'order-service'
    , 'Development'
    , 'db'
    , 'Order_MS')
,
    ('07adedf4-e546-434b-a9b3-1ab9f2acf28e'
    , 'product-service'
    , 'Development'
    , 'db'
    , 'Product_NT')
,
    ('07adedf4-e546-434b-a9b3-1ab9f2acf28e'
    , 'order-service'
    , 'Development'
    , 'db'
    , 'Order_NT')