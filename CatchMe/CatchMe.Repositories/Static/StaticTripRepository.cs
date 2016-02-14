using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatchMe.Entities;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Static
{
    public class StaticTripRepository : ITripRepository
    {
        private static List<Trip> _trips = new List<Trip>()
        {
            new Trip()
            {
                TripInfo = new TripInfo()
                {
                    DateTime = DateTime.Today,
                    Price = 100,
                    Seats = 5
                },
                VehicleInfo = new VehicleInfo()
                {
                    Color = "red",
                    Manufacturer = "Audi",
                    Model = "a6",
                    Year = 2015
                },
                WayInfo = new WayInfo()
                {
                    Origin = new MapPoint(49.7946898, 24.0647954),
                    Destination = new MapPoint(49.842582, 24.003351),
                    WayPoints = new List<MapPoint>() {new MapPoint(49.835327, 24.0144097)}
                }
            },
            new Trip()
            {
                TripInfo = new TripInfo()
                {
                    DateTime = DateTime.Today,
                    Price = 100,
                    Seats = 5
                },
                VehicleInfo = new VehicleInfo()
                {
                    Color = "red",
                    Manufacturer = "Audi",
                    Model = "a6",
                    Year = 2015
                },
                WayInfo = new WayInfo()
                {
                    Origin = new MapPoint(49.7946898, 24.0647954),
                    Destination = new MapPoint(49.842582, 24.003351),
                    WayPoints = new List<MapPoint>() {new MapPoint(49.835327, 24.0144097)}
                }
            }
        };

        public IEnumerable<Trip> GetAll()
        {
            return _trips;
        }

        public bool Add(Trip trip)
        {
            _trips.Add(trip);

            return true;
        }
    }
}
