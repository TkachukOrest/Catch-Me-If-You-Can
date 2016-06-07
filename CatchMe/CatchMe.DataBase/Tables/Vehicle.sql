CREATE TABLE [Vehicle] (
	[Id]		    INT IDENTITY(1,1) NOT NULL,			
	[Manufacturer]	NVARCHAR(50)     NOT NULL,
	[Model]         NVARCHAR(50)     NOT NULL,
	[Color]			NVARCHAR(50)     NOT NULL,
	[Year]			INT               NOT NULL,
	CONSTRAINT [PK_Vehicle] PRIMARY KEY (Id),
);