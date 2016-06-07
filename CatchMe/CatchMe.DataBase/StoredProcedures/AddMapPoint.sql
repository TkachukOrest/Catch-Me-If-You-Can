CREATE PROCEDURE [dbo].[AddMapPoint]
	@TripId INT,
	@Latitude FLOAT,
	@Longitude FLOAT,
	@FormattedLongAddress NVARCHAR(MAX),
	@FormattedShortAddress NVARCHAR(MAX), 
	@Sequence INT,

	@City NVARCHAR(100),
	@District NVARCHAR(100),
	@Region NVARCHAR(100),
	@Country NVARCHAR(100),
	@StreetName NVARCHAR(100),
	@StreetNumber NVARCHAR(30)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION	
			INSERT INTO MapPoint(TripId, Longitude, Latitude, FormattedLongAddress, FormattedShortAddress, [Sequence])
			VALUES (@TripId, @Longitude, @Latitude, @FormattedLongAddress, @FormattedShortAddress, @Sequence);

			DECLARE @MapPointId INT;
			SELECT @MapPointId = SCOPE_IDENTITY();
			
			INSERT INTO AddressDetail(MapPointId, City, District, Region, Country, StreetName, StreetNumber)
			VALUES (@MapPointId, @City, @District, @Region, @Country, @StreetName, @StreetNumber)
			
			SELECT @MapPointId;
		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		IF @@ERROR<>0 AND @@TRANCOUNT > 0
        ROLLBACK TRANSACTION

		THROW;
	END CATCH
END
