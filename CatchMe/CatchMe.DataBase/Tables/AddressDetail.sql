
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