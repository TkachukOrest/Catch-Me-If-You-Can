using System;
using System.Collections.Generic;
using CatchMe.Domain.Values;

namespace CatchMe.Domain.Entities
{
    public class TripEntity
    {
        public int Id { get; set; }        

        public int Seats { get; set; }

        public decimal Price { get; set; }

        public MapPoint Origin { get; set; }

        public MapPoint Destination { get; set; }

        public List<MapPoint> WayPoints { get; set; }

        public DateTime StartDateTime { get; set; }

        public VehicleEntity Vehicle { get; set; }   
        
        public string StaticMapUrl { get; set; }                

        public string UserName { get; set; }

        public string FormattedRoute { get; set; }
    }
}
