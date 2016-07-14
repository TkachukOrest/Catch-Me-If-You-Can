using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CatchMe.Domain.Entities;
using CatchMe.Domain.Values;
using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.EF.Abstract;

namespace CatchMe.Repositories.EF.Concrete
{
    public class TripRepository : EfRepository, ITripRepository
    {         
        public TripRepository(ICatchMeContext context) : base(context) { }        

        public TripEntity GetById(int id)
        {
            var trip = CatchMeContext.Trips
                .Where(x => x.Id == id)
                .Include(t => t.Passengers)//.Select(p => p.User)
                .Include(t => t.Driver.UserProfiles)
                .Include(t => t.Vehicle)
                .Include(t => t.MapPoints.Select(v => v.Addresses))
                .FirstOrDefault();

            this.PopulateUserProfile(trip);
            this.PopulateTripMapPoints(trip);

            trip.SeatsTaken = trip.Passengers.Sum(p => p.BookedSeats);

            return trip;
        }

        public IEnumerable<TripEntity> GetAll()
        {
            var trips = CatchMeContext.Trips
            .Include(t => t.Passengers)
            .Include(t => t.Driver)
            .Include(t => t.Vehicle)
            .Include(t => t.MapPoints)
            .ToList();

            trips.ForEach(trip =>
            {
                PopulateTripMapPoints(trip);

                trip.SeatsTaken = trip.Passengers.Count;
            });

            return trips;
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

        public void Delete(int id)
        {
            var trip = CatchMeContext.Trips.Find(id);

            if (trip != null)
            {
                CatchMeContext.Trips.Remove(trip);
                CatchMeContext.SaveChanges();
            }
        }

        public void AddPassenger(int tripId, int passengerId)
        {
            var passenger = CatchMeContext.Passengers.FirstOrDefault(x => x.TripId == tripId && x.UserId == passengerId);

            if (passenger != null)
            {
                passenger.BookedSeats++;
            }
            else
            {
                CatchMeContext.Passengers.Add(new PassengerEntity()
                {
                    TripId = tripId,
                    UserId = passengerId,
                    BookedSeats = 1
                });
            }

            CatchMeContext.SaveChanges();
        }

        private void PopulateTripMapPoints(TripEntity trip)
        {
            var mapPoints = trip.MapPoints.OrderBy(mp => mp.Sequence).ToList();
            trip.Origin = mapPoints.FirstOrDefault();
            trip.Destination = mapPoints.LastOrDefault();
            trip.WayPoints = mapPoints.Count > 2
                ? mapPoints.Skip(1).Take(mapPoints.Count - 2).ToList()
                : new List<MapPoint>();
        }

        private void Add(TripEntity trip)
        {
            CatchMeContext.Vehicles.Add(trip.Vehicle);
            CatchMeContext.Trips.Add(trip);

            CatchMeContext.SaveChanges();

            AddMapPoints(trip);
        }

        private void Update(TripEntity trip)
        {
            var tripToUpdate = this.GetById(trip.Id);

            if (tripToUpdate != null)
            {
                tripToUpdate.DriverId = trip.DriverId;
                tripToUpdate.Price = trip.Price;
                tripToUpdate.Seats = trip.Seats;
                tripToUpdate.StaticMapUrl = trip.StaticMapUrl;
                tripToUpdate.StartDateTime = trip.StartDateTime;

                if (tripToUpdate.Vehicle != null)
                {
                    tripToUpdate.Vehicle.Color = trip.Vehicle.Color;
                    tripToUpdate.Vehicle.Manufacturer = trip.Vehicle.Manufacturer;
                    tripToUpdate.Vehicle.Model = trip.Vehicle.Model;
                    tripToUpdate.Vehicle.Year = trip.Vehicle.Year;
                }

                CatchMeContext.MapPoints.RemoveRange(tripToUpdate.MapPoints);

                CatchMeContext.SaveChanges();

                AddMapPoints(trip);
            }
        }

        private void AddMapPoints(TripEntity trip)
        {
            int sequence = 0;
            this.AddMapPoint(trip.Id, trip.Origin, sequence++);

            foreach (var point in trip.WayPoints)
            {
                this.AddMapPoint(trip.Id, point, sequence++);
            }

            this.AddMapPoint(trip.Id, trip.Destination, sequence);
        }

        private void AddMapPoint(int tripId, MapPoint point, int sequence)
        {
            point.TripId = tripId;
            point.Sequence = sequence;

            CatchMeContext.MapPoints.Add(point);
            CatchMeContext.Addresses.Add(point.AddressDetails);

            CatchMeContext.SaveChanges();
        }

        private void PopulateUserProfile(TripEntity trip)
        {
            trip.Driver.Profile = trip.Driver.UserProfiles.FirstOrDefault();
        }
    }
}
