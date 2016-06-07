CREATE PROCEDURE [dbo].[GetTripById]
	@TripId int	
AS
BEGIN
	SELECT * FROM Trips 
	WHERE Id = @TripId
END	

