using System.Collections.Generic;
using CatchMe.Domain.Values;

namespace CatchMe.MapService
{
    public interface IMapService
    {
        string GetApiUrl();

        string GetApiKey();

        string CreateStaticMapUrl(StaticMapConfiguration mapConfiguration, IEnumerable<MapPoint> markerPoints);
    }
}
