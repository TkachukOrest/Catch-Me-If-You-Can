CREATE PROCEDURE [dbo].[UpdateTripById]
	@TripId INT,
	@UserId INT,
	@Seats INT,
	@Price  NUMERIC(12,4),
	@StaticMapUrl NVARCHAR(MAX),
	@StartDateTime DATETIME,

	@Manufacturer NVARCHAR(50),
	@Model NVARCHAR(50),	
	@Color NVARCHAR(50),	
	@Year INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION	
			UPDATE Trip SET			 
			 [UserId] = @UserId, 
			 [Seats] = @Seats, 
			 [Price] = @Price,
			 [StaticMapUrl] = @StaticMapUrl,
			 [StartDateTime] = @StartDateTime 
			WHERE Id = @TripId;
						 
			UPDATE V SET
			 V.[Manufacturer] = @Manufacturer,
			 V.[Model] = @Model,
			 V.[Color] = @Color,
			 V.[Year] = @Year
			FROM Vehicle V
			INNER JOIN Trip T on T.VehicleId = V.Id
			WHERE T.Id = @TripId;
			
			DELETE FROM AddressDetail WHERE MapPointId in
			(SELECT [Id] FROM MapPoint WHERE TripId = @TripId)							

			DELETE FROM MapPoint WHERE TripId = @TripId;							
		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		IF @@ERROR<>0 AND @@TRANCOUNT > 0
        ROLLBACK TRANSACTION

		THROW;
	END CATCH
END
