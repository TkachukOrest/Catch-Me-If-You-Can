CREATE PROCEDURE [dbo].[DeleteTripById]
	@TripId int	
AS
BEGIN
	DECLARE @VehicleId INT;
	
	SELECT @VehicleId = VehicleId
	FROM Trip where Id = @TripId		

	DELETE FROM Vehicle 
	WHERE Id = @VehicleId;

	DELETE FROM Passenger 
	WHERE TripId = @TripId;

	DELETE FROM Trip 
	WHERE Id = @TripId;
END	

