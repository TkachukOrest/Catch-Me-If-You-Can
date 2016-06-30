using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CatchMe.Domain.Entities;
using CatchMe.Domain.Values;
using CatchMe.Infrastructure.Abstract;
using CatchMe.Infrastructure.Models;
using CatchMe.MapService;
using CatchMe.Repositories.Abstract;
using CatchMe.WebUI.Filters.Api;
using CatchMe.WebUI.Models;

namespace CatchMe.WebUI.Controllers.Api
{
    [LogApiActionError]
    public class TripController : ApiController
    {
        #region Fields

        private readonly ITripRepository _tripRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapService _mapService;
        private readonly IEmailService _emailService;

        #endregion

        #region Constructor

        public TripController(ITripRepository tripRepository,
            IUserRepository userRepository,
            IMapService mapService,
            IEmailService emailService)
        {
            _tripRepository = tripRepository;
            _userRepository = userRepository;
            _mapService = mapService;
            _emailService = emailService;
        }

        #endregion

        #region Actions

        [HttpGet]
        public IHttpActionResult GetAllTrips()
        {
            var trips =
                _tripRepository.GetAll().Where(t => t.Seats > t.SeatsTaken && t.StartDateTime > DateTime.Now).ToList();

            return Ok(trips);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetTripDetailsById(int id)
        {
            var trip = _tripRepository.GetById(id);
            var user = _userRepository.FindById(trip.Driver.Id);

            var result = new TripDetailsViewModel()
            {
                Trip = trip,
                DriverName = user.UserName,
                DriverProfile = user.Profile
            };

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult CatchCar(CatchCarBindingModel catchCarModel)
        {
            var trip = _tripRepository.GetById(catchCarModel.TripId);
            if (trip.SeatsTaken >= trip.Seats) return BadRequest();

            var passenger = _userRepository.FindByName(catchCarModel.PassengerName);
            if (passenger == null) return BadRequest();

            SendNotificationToTripDriver(trip, passenger);
            SendNotificationToTripPassenger(trip, passenger);

            _tripRepository.AddPassenger(trip.Id, passenger.Id);
            _tripRepository.Save(trip);

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetTripById(int id)
        {
            var trip = _tripRepository.GetById(id);

            if (trip.Driver.UserName != User.Identity.Name)
            {
                return Unauthorized();
            }

            return Ok(trip);
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteTrip([FromUri] int id)
        {
            var trip = _tripRepository.GetById(id);

            if (trip.Driver.UserName != User.Identity.Name)
            {
                return Unauthorized();
            }

            _tripRepository.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult SaveTrip(TripPostBindingModel tripModel)
        {
            var mapPoints = new List<MapPoint>(tripModel.Trip.WayPoints) { tripModel.Trip.Origin, tripModel.Trip.Destination };
            tripModel.Trip.StaticMapUrl = _mapService.CreateStaticMapUrl(tripModel.StaticMapConfiguration, mapPoints);
            tripModel.Trip.Driver = _userRepository.FindByName(User.Identity.Name);

            _tripRepository.Save(tripModel.Trip);

            return Ok();
        }
        #endregion

        #region Helpers

        private void SendNotificationToTripDriver(TripEntity trip, UserEntity passenger)
        {
            _emailService.Send(new EmailMessage()
            {
                Destination = trip.Driver.UserName,
                Subject = "New passenger",
                Message = string.Format("Your trip from {0} to {1} ({2}) has new passenger: {3}. Contact information:\n {4}.\nPrice: {5}.\n Your car:\n {6}",
               trip.Origin.FormattedLongAddress,
               trip.Destination.FormattedLongAddress,
               trip.StartDateTime,
               passenger.Email,
               passenger.Profile,
               trip.Price,
               trip.Vehicle)
            });
        }

        private void SendNotificationToTripPassenger(TripEntity trip, UserEntity passenger)
        {
            _emailService.Send(new EmailMessage()
            {
                Destination = passenger.UserName,
                Subject = "New trip",
                Message =
                    string.Format("You was successfully assigned to trip from {0} to {1} ({2}) with Driver: {3}.\n" +
                                  " Contact information:\n {4}.\n Price: {5}.\n Car:\n {6} ",
                        trip.Origin.FormattedLongAddress,
                        trip.Destination.FormattedLongAddress,
                        trip.StartDateTime,
                        trip.Driver.Email,
                        trip.Driver.Profile,
                        trip.Price,
                        trip.Vehicle)
            });
        }

        //private async Task<UserEntity> GetUser(string userName)
        //{
        //    var getUserApiUrl = _configurationService.GetConfiguration(AppSettingsKeys.SecurityServiceBaseAddressKey) + GetUserWebUrl;

        //    var user = await _webApiRequestService.GetAsync<UserEntity>(getUserApiUrl,
        //                        new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("userName", userName) },
        //                        Request.Headers.Authorization);

        //    return user;
        //}

        #endregion
    }
}