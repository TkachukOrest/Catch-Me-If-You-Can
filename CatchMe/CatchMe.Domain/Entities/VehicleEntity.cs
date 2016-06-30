using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CatchMe.Domain.Entities
{
    [Table("Vehicle")]
    public class VehicleEntity
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int Year { get; set; }              

        public override string ToString()
        {
            return string.Format("Manufacturer: {0},\n Model {1},\n Year {2} in color {3}",
                Manufacturer,
                Model,
                Year,
                Color);
        }

        #region Navigation properties 
        [JsonIgnore]
        public virtual ICollection<TripEntity> Trip { get; set; }
        #endregion
    }
}
