CREATE TABLE [dbo].[Users]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL,
	[Password] [nvarchar](200) NULL,
	[Type] INT NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[ModifiedAt] DATETIME NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
)
