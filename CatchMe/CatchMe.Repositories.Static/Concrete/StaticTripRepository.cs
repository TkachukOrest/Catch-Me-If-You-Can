using System;
using System.Collections.Generic;
using System.Linq;
using CatchMe.Domain.Entities;
using CatchMe.Domain.Values;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Static.Concrete
{
    public class StaticTripRepository : ITripRepository
    {
        #region Fields
        private static List<TripEntity> _trips = new List<TripEntity>()
        {
            new TripEntity()
            {
                Id = 1,
                Price = 100,
                Seats = 5,
                Origin = new MapPoint(49.7946898, 24.0647954, string.Empty, "вулиця Виговського"),
                Destination = new MapPoint(49.842582, 24.003351, string.Empty, "вулиця Єфремова"),
                WayPoints = new List<MapPoint>() { new MapPoint(49.842582, 24.003351, string.Empty, "вулиця Валова") },
                StartDateTime = DateTime.Today,
                Vehicle = new VehicleEntity()
                {
                    Color = "red",
                    Manufacturer = "Audi",
                    Model = "a6",
                    Year = 2015
                },
                StaticMapUrl = @"https://maps.googleapis.com/maps/api/staticmap?center=49,8399150068689,24,0314&zoom=16&size=640x640&maptype=roadmap&markers=color:blue%7C|weight:5%7C|49,840034,24,0336112|49,8408445,24,0289195&path=weight:3%7Ccolor:blue%7Cenc:ekeoHaauqClBcAZIf@G]lCEz@BhEf@dFCTG\OR_@L]JUfBc@~AKRy@n@aBv@Ik@",
                Driver = new UserEntity() { UserName="omanko51@gmail.com"}
            },
            new TripEntity()
            {
                Id = 2,
                Price = 22,
                Seats = 5,
                Origin = new MapPoint(49.7946898, 24.0647954, string.Empty, "вулиця Університетська"),
                Destination = new MapPoint(49.842582, 24.003351, string.Empty, "вулиця Сихівська"),
                WayPoints = new List<MapPoint>() { new MapPoint(49.842582, 24.003351, string.Empty, "вулиця Бойка") },
                StartDateTime = DateTime.Today,
                Vehicle = new VehicleEntity()
                {
                    Color = "red",
                    Manufacturer = "Audi",
                    Model = "a6",
                    Year = 2015
                },
                StaticMapUrl = @"https://maps.googleapis.com/maps/api/staticmap?center=49,8399150068689,24,0314&zoom=16&size=640x640&maptype=roadmap&markers=color:blue%7C|weight:5%7C|49,840034,24,0336112|49,8408445,24,0289195&path=weight:3%7Ccolor:blue%7Cenc:ekeoHaauqClBcAZIf@G]lCEz@BhEf@dFCTG\OR_@L]JUfBc@~AKRy@n@aBv@Ik@",
                Driver = new UserEntity() { UserName="orcoss36@gmail.com"}                
            }
        };
        #endregion        

        #region ITripRepository
        public IEnumerable<TripEntity> GetAll()
        {
            return _trips;
        }

        public TripEntity GetById(int id)
        {
            var trip = _trips.First(x => x.Id == id);

            return trip;
        }

        public void Delete(int id)
        {
            var tripToRemoveIndex = _trips.FindIndex(x => x.Id == id);

            _trips.RemoveAt(tripToRemoveIndex);
        }
       
        public void Save(TripEntity trip)
        {
            if (trip.Id == 0)
            {
                Add(trip);
            }
            else
            {                
                Update(trip);
            }
        }

        public void AddPassenger(int tripId, int passengerId)
        {
            var trip = this.GetById(tripId);
            trip.SeatsTaken++;
        }
        #endregion

        #region Helpers
        private void Add(TripEntity trip)
        {
            trip.Id = GenerateUniqueId();

            _trips.Add(trip);
        }

        private void Update(TripEntity trip)
        {
            var indexToUpdate = _trips.FindIndex(x => x.Id == trip.Id);

            _trips[indexToUpdate] = trip;
        }

        private int GenerateUniqueId()
        {
            var currentId = _trips.Select(t => t.Id).Max();

            return ++currentId;
        }
        #endregion        
    }
}
