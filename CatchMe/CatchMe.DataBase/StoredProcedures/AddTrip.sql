CREATE PROCEDURE [dbo].[AddTrip]
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
			INSERT INTO Vehicle([Manufacturer], [Model], [Color], [Year])
			VALUES(@Manufacturer, @Model, @Color, @Year)

			DECLARE @VehicleId INT;
			SELECT @VehicleId = SCOPE_IDENTITY();

			INSERT INTO Trip([VehicleId], [UserId], [Seats], [Price], [StaticMapUrl], [StartDateTime]) 
			VALUES(@VehicleId, @UserId, @Seats, @Price, @StaticMapUrl, @StartDateTime)    

			SELECT SCOPE_IDENTITY();
		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		IF @@ERROR<>0 AND @@TRANCOUNT > 0
        ROLLBACK TRANSACTION

		THROW;
	END CATCH
END
