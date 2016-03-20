using System.Collections.Generic;
using System.Web.Http;
using CatchMe.BL.Abstract;
using CatchMe.Domain.Entities;
using CatchMe.WebUI.Models;

namespace CatchMe.WebUI.Controllers.Api
{
    public class TripController : ApiController
    {
        private readonly ITripManager _tripManager;

        public TripController(ITripManager tripManager)
        {            
            _tripManager = tripManager;
        }

        [HttpGet]
        public IEnumerable<TripEntity> GetAllTrips()
        {
            var trips = _tripManager.GetAll();

            return trips;
        }

        [HttpGet]
        public string GetTrip(int id)
        {
            return "";
        }

        [HttpPost]
        public void AddTrip(TripAddRequestModel tripAddModel)
        {
            _tripManager.AddTrip(tripAddModel.Trip, tripAddModel.StaticMapConfiguration);
        }
    }
}