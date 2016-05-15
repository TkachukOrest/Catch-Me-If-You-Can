using CatchMe.Domain.Entities;

namespace CatchMe.WebUI.Models
{
    public class TripDetailsViewModel
    {
        public TripEntity Trip { get; set; }

        public string DriverName { get; set; }

        public UserProfileEntity DriverProfile { get; set; }
    }
}