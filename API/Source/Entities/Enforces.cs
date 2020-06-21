using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Enforces
    {
        public string country { get; set; }
        public int measurement { get; set; }
        public string startDate { get; set; }
        public string finalDate { get; set; }

        public Enforces(){}

        public Enforces(string eCountry, int eMeasurement, string eStartDate, string eFinalDate)
        {
            country = eCountry;
            measurement = eMeasurement;
            startDate = eStartDate;
            finalDate = eFinalDate;
        }
    }
}