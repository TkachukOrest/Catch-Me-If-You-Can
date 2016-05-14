namespace CatchMe.Domain.Values
{
    public class MapPoint
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string FormattedLongAddress { get; set; }

        public string FormattedShortAddress { get; set; }

        public Address AddressDetails { get; set; }

        public MapPoint(double latitude,
            double longitude,
            string formattedAddress,
            string formattedShortAddress,
            Address addressDetails = null)
        {
            Latitude = latitude;
            Longitude = longitude;
            FormattedLongAddress = formattedAddress;
            FormattedShortAddress = formattedShortAddress;
            AddressDetails = addressDetails;
        }
    }
}
