using System.Collections.Generic;
using CatchMe.BL.Abstract;
using CatchMe.Domain.Entities;
using CatchMe.Domain.Values;
using CatchMe.MapService;
using CatchMe.Repositories.Abstract;

namespace CatchMe.BL.Concrete
{
    public class TripManager: ITripManager
    {
        #region Fields
        private readonly ITripRepository _tripRepository;
        private readonly IMapService _mapService;
        #endregion

        #region Constructor
        public TripManager(ITripRepository tripRepository, IMapService mapService)
        {
            _tripRepository = tripRepository;
            _mapService = mapService;
        }
        #endregion

        #region ITripManager
        public IEnumerable<TripEntity> GetAll()
        {
            return _tripRepository.GetAll();
        }

        public void AddTrip(TripEntity trip, StaticMapConfiguration staticMapConfiguration)
        {
            var mapPoints = new List<MapPoint>(trip.WayPoints) { trip.Origin, trip.Destination };

            trip.StaticMapUrl = _mapService.CreateStaticMapUrl(staticMapConfiguration, mapPoints);

            _tripRepository.Add(trip);
        }        
        #endregion
    }
}
