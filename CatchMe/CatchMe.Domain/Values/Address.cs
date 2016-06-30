using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CatchMe.Domain.Values
{
    [Table("AddressDetail")]
    public class Address
    {
        public int Id { get; set; }

        public int MapPointId { get; set; }

        public string StreetNumber { get; set; }

        public string StreetName { get; set; }        

        public string District { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        #region Navigation properties 
        [JsonIgnore]
        public virtual MapPoint MapPoint { get; set; }
        #endregion
    }
}
