CREATE TABLE [UserRole] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_UserRole_UserId_RoleId] PRIMARY KEY ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_UserRole_RoleId_Role_Id] FOREIGN KEY ([RoleId]) REFERENCES [Role] (Id),
    CONSTRAINT [FK_UserRole_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [User] (Id),
	CONSTRAINT [UK_UserRole_UserId_RoleId] UNIQUE ([UserId], [RoleId])
);