using System.Collections.Generic;
using System.Text;
using CatchMe.Domain.Values;
using CatchMe.Infrastructure.Abstract;

namespace CatchMe.MapService.GoogleMapService
{
    public class GoogleMapService : MapServiceBase, IMapService
    {
        #region Consts
        private const string GoogleMapApiUrl = @"https://maps.googleapis.com/maps/api";
        #endregion

        #region Constructor
        public GoogleMapService(IConfigurationService configurationService) : base(configurationService) { }
        #endregion

        #region IMapService
        public string GetApiUrl()
        {
            return $@"{GoogleMapApiUrl}/js?key={base.GetApiKey()}&libraries=places";
        }

        public string CreateStaticMapUrl(StaticMapConfiguration mapConfiguration, IEnumerable<MapPoint> markerPoints)
        {
            var markers = ConvertToMarkersConfiguration(markerPoints);
            var staticImageSize = base.GetStaticImageSize();

            return $@"{GoogleMapApiUrl}/staticmap?
                    center={mapConfiguration.Center.Latitude},{mapConfiguration.Center.Longitude}
                    &zoom={mapConfiguration.Zoom}
                    &size={staticImageSize.Width}x{staticImageSize.Height}
                    &maptype={mapConfiguration.MapType}
                    &markers={markers}
                    &style={mapConfiguration.StyleRules}
                    &path=weight:3%7Ccolor:blue%7Cenc:{mapConfiguration.Path}";
        }        
        #endregion

        #region Helpers
        private string ConvertToMarkersConfiguration(IEnumerable<MapPoint> markerPoints)
        {
            var markerConfiguration = new StringBuilder();

            markerConfiguration.Append("color:blue%7C|");

            foreach (var marker in markerPoints)
            {
                markerConfiguration.Append($"|{marker.Latitude},{marker.Longitude}");
            }

            return markerConfiguration.ToString();
        }
        #endregion
    }
}
