using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CatchMe.Domain.Entities;
using Newtonsoft.Json;

namespace CatchMe.Domain.Values
{
    [Table("MapPoint")]
    public class MapPoint
    {
        public int Id { get; set; }
        
        public int TripId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string FormattedLongAddress { get; set; }

        public string FormattedShortAddress { get; set; }

        [NotMapped]
        public Address AddressDetails { get; set; }

        public int Sequence { get; set; }

        public MapPoint()
        {
            Latitude = 0;
            Longitude = 0;
            FormattedShortAddress = string.Empty;
            FormattedLongAddress = string.Empty;
            Sequence = 0;
            AddressDetails = new Address();
        }

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

        #region Navigation properties                
        [JsonIgnore]
        public virtual TripEntity Trip { get; set; }
        [JsonIgnore]
        public virtual ICollection<Address> Addresses { get; set; }                
        #endregion
    }
}
