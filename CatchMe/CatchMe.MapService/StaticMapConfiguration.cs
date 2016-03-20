using System.Collections.Generic;
using CatchMe.Domain.Values;

namespace CatchMe.MapService
{
    public class StaticMapConfiguration
    {
        public MapPoint Center { get; set; }

        public int Zoom { get; set; }

        public string MapType { get; set; }

        public string StyleRules { get; set; }        

        public string Path { get; set; }     
    }
}
