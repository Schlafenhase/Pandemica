using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Continent
    {
        public string name { get; set; }

        public Continent(){}

        public Continent(string cName)
        {
            name = cName;
        }
    }
}