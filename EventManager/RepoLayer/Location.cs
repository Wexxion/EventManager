using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.RepoLayer
{
    public class Location
    {
        public double Latitide { get; }
        public double Longtitude { get; }

        public Location(double latitide, double longtitude)
        {
            this.Latitide = latitide;
            this.Longtitude = longtitude;
        }
    }
}
