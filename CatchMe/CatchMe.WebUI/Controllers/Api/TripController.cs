using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using CatchMe.Domain.Values;
using CatchMe.MapService;
using CatchMe.Repositories.Abstract;
using CatchMe.WebUI.Models;

namespace CatchMe.WebUI.Controllers.Api
{
    public class TripController : ApiController
    {
        #region Fields

        private readonly ITripRepository _tripRepository;
        private readonly IMapService _mapService;

        #endregion

        #region Constructor

        public TripController(ITripRepository tripRepository, IMapService mapService)
        {
            _tripRepository = tripRepository;
            _mapService = mapService;
        }

        #endregion

        #region Actions

        [HttpGet]
        public IHttpActionResult GetAllTrips()
        {
            var trips = _tripRepository.GetAll();

            return Ok(trips);
        }

        [HttpGet]
        public IHttpActionResult GetTripById(int id)
        {
            var trip = _tripRepository.GetById(id);

            return Ok(trip);
        }

        [HttpDelete]
        public IHttpActionResult DeleteTrip([FromUri] int id)
        {
            _tripRepository.Delete(id);

            return Ok();
        }
       
        [HttpPost]
        public IHttpActionResult SaveTrip(TripPostRequestModel tripModel)
        {
            var mapPoints = new List<MapPoint>(tripModel.Trip.WayPoints) { tripModel.Trip.Origin, tripModel.Trip.Destination };
            tripModel.Trip.StaticMapUrl = _mapService.CreateStaticMapUrl(tripModel.StaticMapConfiguration, mapPoints);

            _tripRepository.Save(tripModel.Trip);

            return Ok();
        }
        #endregion
    }
}