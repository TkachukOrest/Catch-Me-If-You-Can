using System;
using System.Collections.Generic;
using CatchMe.Domain.Entities;
using CatchMe.Domain.Values;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Static
{
    public class StaticTripRepository : ITripRepository
    {
        private static List<TripEntity> _trips = new List<TripEntity>()
        {
            new TripEntity()
            {
                Price = 100,
                Seats = 5,
                Origin = new MapPoint(49.7946898, 24.0647954),
                Destination = new MapPoint(49.842582, 24.003351),
                WayPoints = new List<MapPoint>() {new MapPoint(49.835327, 24.0144097)},
                StartDateTime = DateTime.Today,
                Vehicle = new VehicleEntity()
                {
                    Color = "red",
                    Manufacturer = "Audi",
                    Model = "a6",
                    Year = 2015
                }
            },
            new TripEntity()
            {
                Price = 100,
                Seats = 5,
                Origin = new MapPoint(49.7946898, 24.0647954),
                Destination = new MapPoint(49.842582, 24.003351),
                WayPoints = new List<MapPoint>() {new MapPoint(49.835327, 24.0144097)},
                StartDateTime = DateTime.Today,
                Vehicle = new VehicleEntity()
                {
                    Color = "red",
                    Manufacturer = "Audi",
                    Model = "a6",
                    Year = 2015
                }
            }
        };

        public IEnumerable<TripEntity> GetAll()
        {
            return _trips;
        }

        public bool Add(TripEntity trip)
        {
            _trips.Add(trip);

            return true;
        }
    }
}
