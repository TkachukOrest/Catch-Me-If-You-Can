CREATE TABLE Passenger
(
	Id          INT IDENTITY(1, 1)  NOT NULL,
	TripId      INT				    NOT NULL,	
	UserId      INT				    NOT NULL, 				
	BookedSeats INT			        NOT NULL, 
    CONSTRAINT PK_Passenger_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Passenger_TripId_Trip_Id FOREIGN KEY (TripId) REFERENCES Trip(Id),
	CONSTRAINT FK_Passenger_UserId_User_Id FOREIGN KEY (UserId) REFERENCES [User](Id),
	CONSTRAINT UQ_Passenger_TripId_UserId UNIQUE (TripId, UserId),
);
