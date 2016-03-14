namespace CatchMe.Domain.Values
{
    public struct MapPoint
    {
        public readonly double Latitude;

        public readonly double Longitude;

        public MapPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
