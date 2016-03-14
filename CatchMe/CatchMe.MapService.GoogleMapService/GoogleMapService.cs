using CatchMe.Infrastructure.Abstract;

namespace CatchMe.MapService.GoogleMapService
{
    public class GoogleMapService : IMapService
    {
        private readonly IConfigurationService _configurationService;

        public GoogleMapService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public string GetApiUrl()
        {
            return $@"https://maps.googleapis.com/maps/api/js?key={this.GetApiKey()}&libraries=places";
        }

        public string GetApiKey()
        {
            return _configurationService.GetConfiguration(Constants.GoogleMapsApiDeveloperKey);
        }
    }
}
