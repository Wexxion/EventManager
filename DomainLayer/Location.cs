using ValueType = TaskManager.RepoLayer.ValueType;

namespace TaskManager.DomainLayer
{
    public class Location : ValueType
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
