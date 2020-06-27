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
        public string eMail { get; set; }

        public Country(){}

        public Country(string cName, string cContinentName, string cEMail)
        {
            name = cName;
            continentName = cContinentName;
            eMail = cEMail;
        }
    }
}