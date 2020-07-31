using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class EnforcesName : Enforces
    {
        public string measurementName { get; set; }

        public EnforcesName() { }

        public EnforcesName(string eCountry, string eStartDate, string eFinalDate, int eId, string eMeasurementName) : base(eCountry, eStartDate, eFinalDate, eId)
        {
            measurementName = eMeasurementName;
        }
    }
}