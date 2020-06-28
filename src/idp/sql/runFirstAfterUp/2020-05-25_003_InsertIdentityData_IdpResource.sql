INSERT INTO [dbo].[IdentityResources](
             [Enabled]
            ,[Name]
            ,[DisplayName]
            ,[Description]
            ,[Required]
            ,[Emphasize]
            ,[ShowInDiscoveryDocument]
            ,[Created]
            ,[Updated]
            ,[NonEditable])
     VALUES
     (
            1, --[Enabled]
            'openid', --[Name]
            'Your user identifier', --[DisplayName]
            'Your user identifier', --[Description]
            1, --[Required]
            1, --[Emphasize]
            1, --[ShowInDiscoveryDocument]
            getutcdate(), --[Created]
            getutcdate(), --[Updated]
            1 --[NonEditable]
     ),
     (
            1, --[Enabled]
            'profile', --[Name]
            'User profile', --[DisplayName]
            'Your user profile information (first name, last name, etc.)', --[Description]
            1, --[Required]
            1, --[Emphasize]
            1, --[ShowInDiscoveryDocument]
            getutcdate(), --[Created]
            getutcdate(), --[Updated]
            1 --[NonEditable]
     )
GO
INSERT INTO [dbo].[IdentityClaims]
           ([Type]
           ,[IdentityResourceId])
SELECT [Type], Id
FROM (            
    SELECT 'sub' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'openid'
    
    UNION ALL

    SELECT 'name' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'
    
    UNION ALL

    SELECT 'family_name' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'

    UNION ALL

    SELECT 'given_name' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'

    
    UNION ALL

    SELECT 'middle_name' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'

    
    UNION ALL

    SELECT 'preferred_username' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'

    
    UNION ALL

    SELECT 'profile' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'

    
    UNION ALL

    SELECT 'picture' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'
    
    UNION ALL

    SELECT 'website' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'
    
    UNION ALL

    SELECT 'gender' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'
    
    UNION ALL

    SELECT 'birthdate' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'
    
    UNION ALL

    SELECT 'zoneinfo' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'
    
    UNION ALL

    SELECT 'locale' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'
    
    UNION ALL

    SELECT 'updated_at' as [Type], Id 
    FROM  [dbo].[IdentityResources]    
    WHERE Name = 'profile'
    
) sources
GO
