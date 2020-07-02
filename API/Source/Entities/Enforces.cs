using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Enforces
    {
        public string country { get; set; }
        public string startDate { get; set; }
        public string finalDate { get; set; }
        public int id { get; set; }

        public Enforces(){}

        public Enforces(string eCountry, string eStartDate, string eFinalDate, int eId)
        {
            country = eCountry;
            startDate = eStartDate;
            finalDate = eFinalDate;
            id = eId;
        }
    }
}