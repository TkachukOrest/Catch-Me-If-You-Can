CREATE PROCEDURE [dbo].[GetMapPointsByTripId]
	@TripId int	
AS
BEGIN
	SELECT  
	MP.[Id] as Id,
	MP.[Latitude] as Latitude,
	MP.[Longitude] as Longitude,
	MP.[FormattedLongAddress] as FormattedLongAddress,
	MP.[FormattedShortAddress] as FormattedShortAddress,
	MP.[Sequence] as [Sequence],

	AD.[City] AS City,
	AD.[District] AS District,
	AD.[Region] AS Region,
	AD.[Country] AS Country,
	AD.[StreetName] AS StreetName,
	AD.[StreetNumber] AS StreetNumber
	FROM MapPoint MP
	INNER JOIN AddressDetail AD on AD.MapPointId = MP.Id
	WHERE TripId = @TripId
	ORDER BY MP.[Sequence]
END	