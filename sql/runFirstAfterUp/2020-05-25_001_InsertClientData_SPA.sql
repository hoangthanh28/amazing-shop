declare @Id int
INSERT INTO [dbo].[Clients]
           ([Enabled]
           ,[ClientId]
           ,[ProtocolType]
           ,[RequireClientSecret]
           ,[ClientName]
           ,[Description]
           ,[ClientUri]
           ,[LogoUri]
           ,[RequireConsent]
           ,[AllowRememberConsent]
           ,[AlwaysIncludeUserClaimsInIdToken]
           ,[RequirePkce]
           ,[AllowPlainTextPkce]
           ,[AllowAccessTokensViaBrowser]
           ,[FrontChannelLogoutUri]
           ,[FrontChannelLogoutSessionRequired]
           ,[BackChannelLogoutUri]
           ,[BackChannelLogoutSessionRequired]
           ,[AllowOfflineAccess]
           ,[IdentityTokenLifetime]
           ,[AccessTokenLifetime]
           ,[AuthorizationCodeLifetime]
           ,[ConsentLifetime]
           ,[AbsoluteRefreshTokenLifetime]
           ,[SlidingRefreshTokenLifetime]
           ,[RefreshTokenUsage]
           ,[UpdateAccessTokenClaimsOnRefresh]
           ,[RefreshTokenExpiration]
           ,[AccessTokenType]
           ,[EnableLocalLogin]
           ,[IncludeJwtId]
           ,[AlwaysSendClientClaims]
           ,[ClientClaimsPrefix]
           ,[PairWiseSubjectSalt]
           ,[Created]
           ,[Updated]
           ,[LastAccessed]
           ,[UserSsoLifetime]
           ,[UserCodeType]
           ,[DeviceCodeLifetime]
           ,[NonEditable])
     VALUES
     (
           1, --[Enabled]
           newId(),--[ClientId]
           'oidc', --[ProtocolType]
           0,--[RequireClientSecret]
           'Frontend website', --[ClientName]
           'Single page application', --[Description]
           null, --[ClientUri]
           null, --[LogoUri]
           0, --[RequireConsent]
           1, --[AllowRememberConsent]
           0, --[AlwaysIncludeUserClaimsInIdToken]
           1, --[RequirePkce]
           0,--[AllowPlainTextPkce]
           1, --[AllowAccessTokensViaBrowser]
           null, --[FrontChannelLogoutUri]
           0, --[FrontChannelLogoutSessionRequired]
           null, --[BackChannelLogoutUri]
           0, --[BackChannelLogoutSessionRequired]
           1, --[AllowOfflineAccess]
           3600, --[IdentityTokenLifetime]
           3600, --[AccessTokenLifetime]
           3600, --[AuthorizationCodeLifetime]
           null, --[ConsentLifetime]
           3600, --[AbsoluteRefreshTokenLifetime]
           3600, --[SlidingRefreshTokenLifetime]
           2592000, --[RefreshTokenUsage]
           0, --[UpdateAccessTokenClaimsOnRefresh]
           2592000, --[RefreshTokenExpiration]
           0, --[AccessTokenType]
           1, --[EnableLocalLogin]
           1, --[IncludeJwtId]
           0,--[AlwaysSendClientClaims]
           null, --[ClientClaimsPrefix]
           null, --[PairWiseSubjectSalt]
           getutcdate(),--[Created]
           getutcdate(),--[Updated]
           null, --[LastAccessed]
           null, --[UserSsoLifetime]
           null, --[UserCodeType]
           3600, --[DeviceCodeLifetime]
           1 --[NonEditable]
     )
 SELECT @Id = Scope_Identity()

-- client grant type
INSERT INTO [dbo].[ClientGrantTypes]
           ([GrantType]
           ,[ClientId])
VALUES
('authorization_code', @Id),
('password', @Id)


INSERT INTO [dbo].[ClientRedirectUris]
           ([RedirectUri]
           ,[ClientId])
VALUES 
('http://localhost:3000/callback', @Id),
('http://localhost:3000/silent', @Id)


INSERT INTO [dbo].[ClientPostLogoutRedirectUris]
           ([PostLogoutRedirectUri]
           ,[ClientId])
VALUES 
('http://localhost:3000', @Id)


INSERT INTO [dbo].[ClientScopes]
           ([Scope]
           ,[ClientId])
VALUES 
('openid', @Id),
('profile', @Id)

