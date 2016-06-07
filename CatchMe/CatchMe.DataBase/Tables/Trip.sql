
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