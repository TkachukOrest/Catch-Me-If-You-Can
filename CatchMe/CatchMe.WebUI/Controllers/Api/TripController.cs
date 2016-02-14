using System.Collections.Generic;
using System.Web.Http;
using CatchMe.Entities;
using CatchMe.Repositories.Abstract;

namespace CatchMe.WebUI.Controllers.Api
{
    public class TripController : ApiController
    {
        private ITripRepository _tripRepository;

        public TripController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        [HttpGet]
        public IEnumerable<Trip> GetAllTrips()
        {
            var trips = _tripRepository.GetAll();

            return trips;
        }

        [HttpGet]
        public string GetTrip(int id)
        {
            return "";
        }

        [HttpPost]
        public bool AddTrip(Trip trip)
        {
            return _tripRepository.Add(trip);
        }
    }
}