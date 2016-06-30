using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CatchMe.Domain.Values;
using Newtonsoft.Json;

namespace CatchMe.Domain.Entities
{
    [Table("Trip")]
    public class TripEntity
    {   
        #region Properties        
        public int Id { get; set; }        

        public int Seats { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Price { get; set; }

        [NotMapped]
        public int SeatsTaken { get; set; }                

        [NotMapped]
        public MapPoint Origin { get; set; }

        [NotMapped]
        public MapPoint Destination { get; set; }

        [NotMapped]
        public List<MapPoint> WayPoints { get; set; }

        public DateTime StartDateTime { get; set; }        

        [Column("UserId")]
        public int DriverId { get; set; }

        public UserEntity Driver { get; set; }

        public int VehicleId { get; set; }

        public VehicleEntity Vehicle { get; set; }        

        public string StaticMapUrl { get; set; }
        #endregion

        #region Navigation properties                 
        [JsonIgnore]
        public virtual ICollection<MapPoint> MapPoints { get; set; }
        [JsonIgnore]
        public virtual ICollection<PassengerEntity> Passengers { get; set; }        
        #endregion
    }
}
