USE CatchMeDb;

GO

CREATE TABLE [Role] (
    [Id]	 INT IDENTITY (1,1)  NOT NULL,
    [Name]   NVARCHAR (256)      NOT NULL,
    CONSTRAINT [PK_Role_ID] PRIMARY KEY (Id)
);

GO

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

GO

CREATE TABLE [UserProfile] (
    [Id]		  INT IDENTITY(1,1) NOT NULL,
	[UserId]      INT				NOT NULL,
    [FirstName]   NVARCHAR (128)    NOT NULL,    
    [LastName]    NVARCHAR (128)    NOT NULL,
	[PhoneNumber] NVARCHAR (128)    NOT NULL   
    CONSTRAINT [PK_UserProfile_Id] PRIMARY KEY (Id),
    CONSTRAINT [FK_UserProfile_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [User](Id) 
);

GO

CREATE TABLE [UserRole] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_UserRole_UserId_RoleId] PRIMARY KEY ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_UserRole_RoleId_Role_Id] FOREIGN KEY ([RoleId]) REFERENCES [Role] (Id),
    CONSTRAINT [FK_UserRole_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [User] (Id)
);

GO

CREATE TABLE [Vehicle] (
	[Id]		    INT IDENTITY(1,1) NOT NULL,			
	[Manufacturer]	NVARCHAR(256)     NOT NULL,
	[Model]         NVARCHAR(256)     NOT NULL,
	[Color]			NVARCHAR(256)     NOT NULL,
	[Year]			NVARCHAR(256)     NOT NULL,
	CONSTRAINT [PK_Vehicle] PRIMARY KEY (Id),
);

--GO

--CREATE TABLE [UserVehicle] (
--	[UserId]    INT NOT NULL,
--    [VehicleId] INT NOT NULL,
--    CONSTRAINT [PK_UserVehicle_UserId_VehicleId] PRIMARY KEY ([UserId] ASC, [VehicleId] ASC),
--    CONSTRAINT [FK_UserVehicle_VehicleId_Vehicle_Id] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicle] (Id),
--    CONSTRAINT [FK_UserVehicle_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [User] (Id)
--);

GO

CREATE TABLE [Trip] (
	[Id]		         INT IDENTITY(1,1) NOT NULL,
	[VehicleId]          INT               NOT NULL,
	[UserId]             INT			   NOT NULL, 	
	[Seats]		         INT			   NOT NULL,	
	[Price]		         NUMERIC(12,4)     NOT NULL,
	[StaticMapUrl]       NVARCHAR(MAX)     NOT NULL,	
	[StartDateTime]      DATETIME          NOT NULL, 
	CONSTRAINT [PK_Trip] PRIMARY KEY (Id),
	CONSTRAINT [FK_Trip_VehicleId_Vehicle_Id] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicle] (Id),	
	CONSTRAINT [FK_Trip_UserId_User_Id] FOREIGN KEY (UserId) REFERENCES [User](Id),	
);

GO

CREATE TABLE [MapPoint]
(
	[Id]					 INT IDENTITY(1, 1)      NOT NULL,
	[TripId]			     INT				     NOT NULL ,
	[Longitude]			     FLOAT				     NOT NULL,
	[Latitude]			     FLOAT				     NOT NULL,
	[FormattedLongAddress]   NVARCHAR(MAX)		     NOT NULL,
	[FormattedShortAddress]  NVARCHAR(MAX)		     NOT NULL,
	[Sequence]				 INT				     NOT NULL,
	CONSTRAINT [PK_MapPoint_Id] PRIMARY KEY (Id),
	CONSTRAINT [FK_MapPoint_TripId_Trip_Id] FOREIGN KEY (TripId) REFERENCES Trip(Id),
);

GO

CREATE TABLE [AddressDetail]
(
	[Id]					 INT IDENTITY(1, 1)      NOT NULL,
	[MapPointId]			 INT				     NOT NULL ,
	[StreetNumber]			 NVARCHAR(30)			 NULL,
	[StreetName]			 NVARCHAR(100)			 NULL,
	[District]				 NVARCHAR(100)		     NULL,
	[City]					 NVARCHAR(100)		     NULL,	
	[Region]				 NVARCHAR(100)		     NULL,	
	[Country]				 NVARCHAR(100)		     NULL,	
	CONSTRAINT [PK_AddressDetail_Id] PRIMARY KEY (Id),
	CONSTRAINT [FK_AddressDetail_MapPointId_MapPoint_Id] FOREIGN KEY (MapPointId) REFERENCES MapPoint(Id),
);

GO

CREATE TABLE Passenger
(
	Id       INT IDENTITY(1, 1)  NOT NULL,
	TripId   INT				 NOT NULL,	
	UserId   INT				 NOT NULL, 				
	CONSTRAINT PK_Passenger_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Passenger_TripId_Trip_Id FOREIGN KEY (TripId) REFERENCES Trip(Id),
	CONSTRAINT FK_Passenger_UserId_User_Id FOREIGN KEY (UserId) REFERENCES [User](Id),
	CONSTRAINT UQ_Passenger_TripId_UserId UNIQUE (TripId, UserId),
);

GO

