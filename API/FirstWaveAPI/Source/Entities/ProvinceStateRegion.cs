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
        public int id { get; set; }

        public ProvinceStateRegion(){}

        public ProvinceStateRegion(string psrName, string psrCountry, int psrId)
        {
            name = psrName;
            country = psrCountry;
            id = psrId;
        }
    }
}