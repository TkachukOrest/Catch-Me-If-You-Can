using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CatchMe.Domain.Entities;
using CatchMe.Domain.Values;
using CatchMe.Infrastructure.Abstract;
using CatchMe.Infrastructure.Models;
using CatchMe.MapService;
using CatchMe.Repositories.Abstract;
using CatchMe.WebUI.Code;
using CatchMe.WebUI.Models;

namespace CatchMe.WebUI.Controllers.Api
{
    public class TripController : ApiController
    {
        #region Consts

        private const string GetUserWebUrl = "api/Account/GetUser";

        #endregion

        #region Fields

        private readonly ITripRepository _tripRepository;
        private readonly IMapService _mapService;
        private readonly IWebApiRequestService _webApiRequestService;
        private readonly IConfigurationService _configurationService;
        private readonly IEmailService _emailService;

        #endregion

        #region Constructor

        public TripController(ITripRepository tripRepository,
            IWebApiRequestService webApiRequestService,
            IMapService mapService,
            IConfigurationService configurationService,
            IEmailService emailService)
        {
            _tripRepository = tripRepository;
            _webApiRequestService = webApiRequestService;
            _mapService = mapService;
            _configurationService = configurationService;
            _emailService = emailService;
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
        [Authorize]
        public async Task<IHttpActionResult> GetTripDetailsById(int id)
        {
            var trip = _tripRepository.GetById(id);

            var getUserApiUrl = _configurationService.GetConfiguration(AppSettingsKeys.SecurityServiceBaseAddressKey) + GetUserWebUrl;
            var user = await _webApiRequestService.GetAsync<UserEntity>(getUserApiUrl,
                new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("userName", trip.UserName) },
                Request.Headers.Authorization);

            if (user == null) return BadRequest();

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
        public async Task<IHttpActionResult> CatchCar(CatchCarBindingModel catchCarModel)
        {
            var trip = _tripRepository.GetById(catchCarModel.TripId);
            if(trip.SeatsTaken >= trip.Seats) return BadRequest();

            var driver = await this.GetUser(trip.UserName);
            var passenger = await this.GetUser(catchCarModel.PassengerName);
            if (driver == null || passenger == null) return BadRequest();

            trip.SeatsTaken++;
            _tripRepository.Save(trip);

            _emailService.Send(new EmailMessage() { Destination = driver.UserName,
                Subject = "New passenger",
                Message = string.Format("Your trip from {0} to {1} ({2}) has new passenger: {3}. Contact information:\n {4}. Your car:\n {5}",
                trip.Origin.FormattedLongAddress,
                trip.Destination.FormattedLongAddress,
                trip.StartDateTime,
                passenger.Email,
                passenger.Profile,
                trip.Vehicle)
            });

            _emailService.Send(new EmailMessage()
            {
                Destination = passenger.UserName,
                Subject = "New trip",
                Message = string.Format("You was successfully assigned to trip from {0} to {1} ({2}) with Driver: {3}.\n" +
                                        " Contact information:\n {4}. Car:\n {5} ",
                trip.Origin.FormattedLongAddress,
                trip.Destination.FormattedLongAddress,
                trip.StartDateTime,
                driver.Email,
                driver.Profile,
                trip.Vehicle)
            });

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetTripById(int id)
        {
            var trip = _tripRepository.GetById(id);

            if (trip.UserName != User.Identity.Name)
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

            if (trip.UserName != User.Identity.Name)
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
            tripModel.Trip.UserName = User.Identity.Name;            

            _tripRepository.Save(tripModel.Trip);

            return Ok();
        }
        #endregion

        #region Helpers

        private async Task<UserEntity> GetUser(string userName)
        {
            var getUserApiUrl = _configurationService.GetConfiguration(AppSettingsKeys.SecurityServiceBaseAddressKey) + GetUserWebUrl;

            var user = await _webApiRequestService.GetAsync<UserEntity>(getUserApiUrl,
                                new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("userName", userName) },
                                Request.Headers.Authorization);

            return user;
        }
        #endregion
    }
}