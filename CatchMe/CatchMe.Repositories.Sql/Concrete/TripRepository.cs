using System;
using System.Collections.Generic;
using System.Data;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;
using System.Data.SqlClient;
using System.Linq;
using CatchMe.Domain.Values;
using CatchMe.Infrastructure.Extensions;
using CatchMe.Repositories.Sql.Abstract;

namespace CatchMe.Repositories.Sql.Concrete
{
    public class TripRepository : SqlRepository, ITripRepository
    {
        #region Constructors
        public TripRepository(IRepositorySettings repositorySettings) : base(repositorySettings) { }
        #endregion

        #region ITripRepository
        public IEnumerable<TripEntity> GetAll()
        {
            var trips = new List<TripEntity>();

            ExecuteDataReaderProc(SpNames.GetTrips, null, (reader) =>
            {
                while (reader.Read())
                { 
                    trips.Add(this.PopulateTripEntity(reader));
                }
            });

            return trips;
        }

        public TripEntity GetById(int id)
        {
            TripEntity trip = null;

            var parameters = new[]
            {
                new SqlParameter(SpParams.TripCommon.TripId, id) {Direction = ParameterDirection.Input, DbType = DbType.Int32},
            };

            ExecuteDataReaderProc(SpNames.GetTripById, parameters, (reader) =>
            {
                if (reader.Read())
                { 
                    trip = this.PopulateTripEntity(reader);
                }
            });

            return trip;
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
            var parameters = new[]
            {
                new SqlParameter(SpParams.TripCommon.TripId, id) {Direction = ParameterDirection.Input, DbType = DbType.Int32},
            };

            ExecuteNonQueryProc(SpNames.DeleteTripById, parameters);
        }
        #endregion

        #region Helpers
        private void Add(TripEntity trip)
        {
            var parameters = new[]
            {
                new SqlParameter(SpParams.SaveTrip.UserId, trip.Driver.Id) { Direction = ParameterDirection.Input, DbType = DbType.Int32 },
                new SqlParameter(SpParams.SaveTrip.Seats, trip.Seats) { Direction = ParameterDirection.Input, DbType = DbType.Int32},
                new SqlParameter(SpParams.SaveTrip.Price, trip.Price) { Direction = ParameterDirection.Input, DbType = DbType.Decimal },
                new SqlParameter(SpParams.SaveTrip.StaticMapUrl, trip.StaticMapUrl) { Direction = ParameterDirection.Input, DbType = DbType.String },
                new SqlParameter(SpParams.SaveTrip.StartDateTime, trip.StartDateTime) { Direction = ParameterDirection.Input, DbType = DbType.DateTime },

                new SqlParameter(SpParams.SaveTrip.Manufacturer, trip.Vehicle.Manufacturer) { Direction = ParameterDirection.Input, DbType = DbType.String },
                new SqlParameter(SpParams.SaveTrip.Model, trip.Vehicle.Model) { Direction = ParameterDirection.Input, DbType = DbType.String},
                new SqlParameter(SpParams.SaveTrip.Color, trip.Vehicle.Color) { Direction = ParameterDirection.Input, DbType = DbType.String },
                new SqlParameter(SpParams.SaveTrip.Year, trip.Vehicle.Year) { Direction = ParameterDirection.Input, DbType = DbType.Int32 },
            };

            trip.Id = ExecuteScalar(SpNames.AddTrip, parameters);

            this.AddMapPoints(trip);
        }

        private void Update(TripEntity trip)
        {
            var parameters = new[]
            {
                new SqlParameter(SpParams.SaveTrip.TripId, trip.Id) { Direction = ParameterDirection.Input, DbType = DbType.Int32 },
                new SqlParameter(SpParams.SaveTrip.UserId, trip.Driver.Id) { Direction = ParameterDirection.Input, DbType = DbType.Int32 },
                new SqlParameter(SpParams.SaveTrip.Seats, trip.Seats) { Direction = ParameterDirection.Input, DbType = DbType.Int32},
                new SqlParameter(SpParams.SaveTrip.Price, trip.Price) { Direction = ParameterDirection.Input, DbType = DbType.Decimal },
                new SqlParameter(SpParams.SaveTrip.StaticMapUrl, trip.StaticMapUrl) { Direction = ParameterDirection.Input, DbType = DbType.String },
                new SqlParameter(SpParams.SaveTrip.StartDateTime, trip.StartDateTime) { Direction = ParameterDirection.Input, DbType = DbType.DateTime },

                new SqlParameter(SpParams.SaveTrip.Manufacturer, trip.Vehicle.Manufacturer) { Direction = ParameterDirection.Input, DbType = DbType.String },
                new SqlParameter(SpParams.SaveTrip.Model, trip.Vehicle.Model) { Direction = ParameterDirection.Input, DbType = DbType.String},
                new SqlParameter(SpParams.SaveTrip.Color, trip.Vehicle.Color) { Direction = ParameterDirection.Input, DbType = DbType.String },
                new SqlParameter(SpParams.SaveTrip.Year, trip.Vehicle.Year) { Direction = ParameterDirection.Input, DbType = DbType.Int32 },
            };

            ExecuteNonQueryProc(SpNames.UpdateTripById, parameters);

            this.AddMapPoints(trip);
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
            var parameters = new[]
            {
               new SqlParameter(SpParams.AddMapPoint.TripId, tripId) { Direction = ParameterDirection.Input, DbType = DbType.Int32 },
               new SqlParameter(SpParams.AddMapPoint.Latitude, point.Latitude) { Direction = ParameterDirection.Input, DbType = DbType.Double },
               new SqlParameter(SpParams.AddMapPoint.Longitude, point.Longitude) { Direction = ParameterDirection.Input, DbType = DbType.Double },               
               new SqlParameter(SpParams.AddMapPoint.FormattedLongAddress, point.FormattedLongAddress) { Direction = ParameterDirection.Input, DbType = DbType.String },
               new SqlParameter(SpParams.AddMapPoint.FormattedShortAddress, point.FormattedShortAddress) { Direction = ParameterDirection.Input, DbType = DbType.String },
               new SqlParameter(SpParams.AddMapPoint.Sequence, sequence) { Direction = ParameterDirection.Input, DbType = DbType.Int32 },

               new SqlParameter(SpParams.AddMapPoint.City, point.AddressDetails.City) { Direction = ParameterDirection.Input, DbType = DbType.String },
               new SqlParameter(SpParams.AddMapPoint.District, point.AddressDetails.District) { Direction = ParameterDirection.Input, DbType = DbType.String },
               new SqlParameter(SpParams.AddMapPoint.Region, point.AddressDetails.Region) { Direction = ParameterDirection.Input, DbType = DbType.String },
               new SqlParameter(SpParams.AddMapPoint.Country, point.AddressDetails.Country) { Direction = ParameterDirection.Input, DbType = DbType.String },
               new SqlParameter(SpParams.AddMapPoint.StreetName, point.AddressDetails.StreetName) { Direction = ParameterDirection.Input, DbType = DbType.String },
               new SqlParameter(SpParams.AddMapPoint.StreetNumber, point.AddressDetails.StreetNumber) { Direction = ParameterDirection.Input, DbType = DbType.String },
            };

            point.Sequence = sequence;
            point.Id = ExecuteScalar(SpNames.AddMapPoint, parameters);
        }

        private List<MapPoint> GetMapPoints(int tripId)
        {
            List<MapPoint> mapPoints = new List<MapPoint>();

            var parameters = new[]
            {
                new SqlParameter(SpParams.TripCommon.TripId, tripId) {Direction = ParameterDirection.Input, DbType = DbType.Int32},
            };

            ExecuteDataReaderProc(SpNames.GetMapPointsByTripId, parameters, (reader) =>
            {
                while (reader.Read())
                { 
                    mapPoints.Add(this.PopulateMapPoint(reader));
                }
            });

            return mapPoints;
        }

        public void AddPassenger(int tripId, int passengerId)
        {
            var parameters = new[]
            {
                new SqlParameter(SpParams.AddPassenger.TripId, tripId) {Direction = ParameterDirection.Input, DbType = DbType.Int32},
                new SqlParameter(SpParams.AddPassenger.PassengerId, passengerId) {Direction = ParameterDirection.Input, DbType = DbType.Int32},
                new SqlParameter(SpParams.AddPassenger.BookedSeats, 1) {Direction = ParameterDirection.Input, DbType = DbType.Int32}
            };

            ExecuteNonQueryProc(SpNames.AddPassenger, parameters);
        }
        #endregion

        #region Populating helpers
        private MapPoint PopulateMapPoint(SqlDataReader dataReader)
        {
            var mapPoint = new MapPoint()
            {
                Id = dataReader["Id"].FromDb<int>(),
                Sequence = dataReader["Sequence"].FromDb<int>(),
                Latitude = dataReader["Latitude"].FromDb<double>(),
                Longitude = dataReader["Longitude"].FromDb<double>(),
                FormattedLongAddress = dataReader["FormattedLongAddress"].FromDb<string>(),
                FormattedShortAddress = dataReader["FormattedShortAddress"].FromDb<string>(),
                AddressDetails = new Address()
                {
                    City = dataReader["City"].FromDb<string>(),
                    Country = dataReader["Country"].FromDb<string>(),
                    District = dataReader["District"].FromDb<string>(),
                    Region = dataReader["Region"].FromDb<string>(),
                    StreetName = dataReader["StreetName"].FromDb<string>(),
                    StreetNumber = dataReader["StreetNumber"].FromDb<string>()
                }
            };

            return mapPoint;
        }

        private TripEntity PopulateTripEntity(SqlDataReader dataReader)
        {
            var tripId = dataReader["Id"].FromDb<int>();

            var driver = new UserEntity()
            {
                Id = dataReader["UserId"].FromDb<int>(),
                UserName = dataReader["UserName"].FromDb<string>(),
                Email = dataReader["UserEmail"].FromDb<string>(),
            };

            var vehicle = new VehicleEntity()
            {
                Id = dataReader["VehicleId"].FromDb<int>(),
                Manufacturer = dataReader["VehicleManufacturer"].FromDb<string>(),
                Model = dataReader["VehicleModel"].FromDb<string>(),
                Color = dataReader["VehicleColor"].FromDb<string>(),
                Year = dataReader["VehicleYear"].FromDb<int>()
            };

            var mapPoints = this.GetMapPoints(tripId);

            var trip = new TripEntity()
            {
                Id = tripId,
                Vehicle = vehicle,
                Driver = driver,
                Seats = dataReader["Seats"].FromDb<int>(),
                SeatsTaken = dataReader["SeatsTaken"].FromDb<int>(),
                Price = dataReader["Price"].FromDb<decimal>(),
                StaticMapUrl = dataReader["StaticMapUrl"].FromDb<string>(),
                StartDateTime = dataReader["StartDateTime"].FromDb<DateTime>(),
                Origin = mapPoints.FirstOrDefault(),
                Destination = mapPoints.LastOrDefault(),
                WayPoints = mapPoints.Count > 2 ? mapPoints.GetRange(1, mapPoints.Count - 2) : new List<MapPoint>()
            };

            return trip;
        }
        #endregion
    }
}
