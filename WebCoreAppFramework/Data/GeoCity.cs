using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreAppFramework.Data
{
    public class GeoCity
    {
        public GeoCity(string line)
        {
            var split = line.Split("\",\"");
            City = split[0].Trim('\"');
            City_ascii = split[1];
            Lat = double.Parse(split[2].Trim('\"'));
            Lng = double.Parse(split[3].Trim('\"'));
            Country = split[4];
            Iso2 = split[5];
            Iso3 = split[6];
            Adminname = split[7];
            Capital = split[8];
            Population = split[9];
            Id = split[10].Trim('\"');
        }
        public string City { get; set; }
        public string City_ascii { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Country { get; set; }
        public string Iso2 { get; set; }
        public string Iso3 { get; set; }
        public string Adminname { get; set; }
        public string Capital { get; set; }
        public string Population { get; set; }
        public string Id { get; set; }

    }
}
