using CatchMe.Domain.Entities;
using CatchMe.MapService;

namespace CatchMe.WebUI.Models
{
    public class TripPostRequestModel
    {
        public TripEntity Trip { get; set; } 

        public StaticMapConfiguration StaticMapConfiguration { get; set; }
    }
}