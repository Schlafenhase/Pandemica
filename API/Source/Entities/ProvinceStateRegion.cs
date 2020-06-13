using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class ProvinceStateRegion
    {
        public string name { get; set; }
        public string country { get; set; }

        public ProvinceStateRegion(){}

        public ProvinceStateRegion(string psrName, string psrCountry)
        {
            name = psrName;
            country = psrCountry;
        }
    }
}