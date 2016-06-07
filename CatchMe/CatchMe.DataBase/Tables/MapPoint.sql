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
	CONSTRAINT [UK_MapPoint_TripId_Sequence] UNIQUE(TripId, Sequence)
);