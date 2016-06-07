CREATE VIEW [dbo].[Trips]
	AS SELECT T.Id AS Id, 
		   T.Seats AS Seats,
		   COALESCE((SELECT SUM(BookedSeats) FROM Passenger WHERE TripId = T.Id),0) AS SeatsTaken,
		   T.Price AS Price, 
		   T.StaticMapUrl AS StaticMapUrl, 
		   T.StartDateTime AS StartDateTime,
		   U.Id AS UserId,
		   U.UserName AS UserName,
		   U.Email AS UserEmail,
		   V.[Id] AS VehicleId,		    
		   V.[Manufacturer]	AS VehicleManufacturer,
		   V.[Model] AS VehicleModel,        
		   V.[Color] AS VehicleColor,			
		   V.[Year]	 AS VehicleYear		
	FROM Trip T	
	INNER JOIN [User] U on U.Id = T.UserId
	INNER JOIN [Vehicle] V on V.Id = T.VehicleId
