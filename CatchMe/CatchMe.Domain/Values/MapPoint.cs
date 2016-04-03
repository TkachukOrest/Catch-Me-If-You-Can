namespace CatchMe.Domain.Values
{
    public class MapPoint
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }

        public MapPoint(double latitude, double longitude, string address)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
        }        
    }
}
