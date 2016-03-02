﻿namespace CatchMe.Domain.Values
{
    public struct MapPoint
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public MapPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}