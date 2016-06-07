CREATE PROCEDURE [dbo].[DeleteTripById]
	@TripId int	
AS
BEGIN
	DELETE FROM Vehicle 
	WHERE Id = @TripId;

	DELETE FROM Trip 
	WHERE Id = @TripId;
END	

