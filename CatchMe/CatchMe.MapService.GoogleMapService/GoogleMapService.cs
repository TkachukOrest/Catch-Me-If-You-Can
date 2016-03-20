using System.Collections.Generic;
using System.Text;
using CatchMe.Domain.Values;
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
            return _configurationService.GetConfiguration(Constants.MapsApiDeveloperKey);
        }     

        public string CreateStaticMapUrl(StaticMapConfiguration mapConfiguration, IEnumerable<MapPoint> markerPoints)
        {
            var imageWidth = _configurationService.GetConfigurationValue<int>(Constants.StaticMapImageWidth);
            var imageHeight = _configurationService.GetConfigurationValue<int>(Constants.StaticMapImageHeight);
            var markers = ConvertToMarkersConfiguration(markerPoints);

            return string.Format(@"https://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom={2}&size={3}x{4}&maptype={5}&markers={6}&style={7}&path=weight:3%7Ccolor:blue%7Cenc:{8}",
                mapConfiguration.Center.Latitude,
                mapConfiguration.Center.Longitude,
                mapConfiguration.Zoom,
                imageWidth,
                imageHeight,
                mapConfiguration.MapType,
                markers,
                mapConfiguration.StyleRules,
                mapConfiguration.Path);
        }

        private string ConvertToMarkersConfiguration(IEnumerable<MapPoint> markerPoints)
        {
            var markerConfiguration = new StringBuilder();

            markerConfiguration.Append("color:blue%7C|weight:5%7C");            

            foreach (var marker in markerPoints)
            {
                markerConfiguration.Append($"|{marker.Latitude},{marker.Longitude}");
            }
                
            return markerConfiguration.ToString();
        }
    }
}
