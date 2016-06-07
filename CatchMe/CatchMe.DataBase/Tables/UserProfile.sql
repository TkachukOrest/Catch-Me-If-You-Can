CREATE TABLE [UserProfile] (
    [Id]		  INT IDENTITY(1,1) NOT NULL,
	[UserId]      INT				NOT NULL,
    [FirstName]   NVARCHAR (128)    NOT NULL,    
    [LastName]    NVARCHAR (128)    NOT NULL,
	[PhoneNumber] NVARCHAR (128)    NOT NULL   
    CONSTRAINT [PK_UserProfile_Id] PRIMARY KEY (Id),
    CONSTRAINT [FK_UserProfile_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [User](Id) 
);