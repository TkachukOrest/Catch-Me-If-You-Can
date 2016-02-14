using System.Collections.Generic;

namespace CatchMe.Entities
{
    public class WayInfo
    {
        public MapPoint Origin { get; set; }

        public MapPoint Destination { get; set; }

        public List<MapPoint> WayPoints { get; set; }
    }
}
