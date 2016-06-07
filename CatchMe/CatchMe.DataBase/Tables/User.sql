CREATE TABLE [User] (
    [Id]                   INT IDENTITY(1,1)   NOT NULL,
    [Email]                NVARCHAR (256)      NULL,
    [EmailConfirmed]       BIT                 NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)      NULL,
    [SecurityStamp]        NVARCHAR (MAX)      NULL,    
    [UserName]             NVARCHAR (256)      NOT NULL,    
    [CreationTime]         DATETIME            NOT NULL 
	CONSTRAINT [DF_User_CreationDate] DEFAULT (getutcdate())
	CONSTRAINT [PK_User_Id] PRIMARY KEY (Id),	
);
