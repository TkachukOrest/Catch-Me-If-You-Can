CREATE PROCEDURE [dbo].[AddPassenger]
	@TripId INT,
	@PassengerId INT,
	@BookedSeats INT
AS
BEGIN
	DECLARE @PassengerInfoId INT = 0;
	DECLARE @AlreadyBookedSeats INT = 0;

	SELECT @PassengerInfoId = Id, @AlreadyBookedSeats = BookedSeats
	FROM [Passenger]
	WHERE TripId = @TripId AND UserId = @PassengerId

	IF(@PassengerInfoId = 0)
	BEGIN
		INSERT INTO [Passenger](TripId, UserId, BookedSeats)
	    VALUES (@TripId, @PassengerId, @BookedSeats)
	END
	ELSE
	BEGIN	 	
		UPDATE[Passenger]
		SET BookedSeats = @AlreadyBookedSeats + @BookedSeats
		WHERE TripId = @TripId AND UserId = @PassengerId
	END
END
