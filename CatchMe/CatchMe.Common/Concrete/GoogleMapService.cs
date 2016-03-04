using System.Configuration;
using CatchMe.Infrastructure.Abstract;

namespace CatchMe.Infrastructure.Concrete
{
    public class GoogleMapService : IMapService
    {
        public string GetApiUrl()
        {
            return string.Format(@"https://maps.googleapis.com/maps/api/js?key={0}&libraries=places", this.GetApiKey());
        }

        public string GetApiKey()
        {
            return ConfigurationManager.AppSettings[Constants.GoogleMapsApiDeveloperKey];
        }
    }
}
