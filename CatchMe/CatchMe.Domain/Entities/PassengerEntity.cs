using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CatchMe.Domain.Entities
{
    [Table("Passenger")]
    public class PassengerEntity
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int UserId { get; set; }
        public int BookedSeats { get; set; }

        #region Navigation properties 
        [JsonIgnore]
        public virtual TripEntity Trip { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
        #endregion
    }
}
