using System;
using System.Collections.Generic;
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
        public IHttpActionResult AddTrip(TripAddRequestModel tripAddModel)
        {
            var mapPoints = new List<MapPoint>(tripAddModel.Trip.WayPoints) { tripAddModel.Trip.Origin, tripAddModel.Trip.Destination };

            tripAddModel.Trip.StaticMapUrl = _mapService.CreateStaticMapUrl(tripAddModel.StaticMapConfiguration, mapPoints);

            _tripRepository.Add(tripAddModel.Trip);

            return Ok();
        }
        #endregion
    }
}