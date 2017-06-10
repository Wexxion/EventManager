using System;

namespace DomainLayer
{
    public class Location : RepoLayer.ValueType
    {
        public double Latitide { get; }
        public double Longtitude { get; }

        public Location(double latitide, double longtitude)
        {
            Latitide = latitide;
            Longtitude = longtitude;
        }
    }
}
