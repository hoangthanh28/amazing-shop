IF NOT EXISTS (SELECT 1 FROM [Users] WHERE username = 'user-sample1') INSERT INTO [Users] (UserName, Email, Firstname, Lastname, IsMustChangePassword) values ('user-sample1','adminsample@gmail.com','Sample','Admin',0)
GO
-- default password is Pass123$
UPDATE [Users] SET Password = N'AQAAAAEAACcQAAAAEIUM1lt/SY8ndgLBoKoed94BMlktWsDs6EvlH/vZyJkm5YDYcZpoNgXfauCi5p5o/A==' WHERE UserName = 'user-sample1'
GO