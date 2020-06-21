using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Country
    {
        public string name { get; set; }
        public string continentName { get; set; }

        public Country(){}

        public Country(string cName, string cContinentName)
        {
            name = cName;
            continentName = cContinentName;
        }
    }
}