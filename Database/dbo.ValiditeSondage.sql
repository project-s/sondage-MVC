CREATE TABLE [dbo].[ValiditeSondage]
(
	[idValiditeSondage] CHAR(1) NOT NULL PRIMARY KEY, 
    [libValiditeSondage] CHAR(50) NOT NULL
)




INSERT INTO [dbo].[ValiditeSondage] VALUES ('a', 'Actif')

UPDATE [dbo].[ValiditeSondage]
SET libValiditeSondage = 'Activé'
WHERE idValiditeSondage = 'a'