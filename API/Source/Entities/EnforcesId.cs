using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class EnforcesId : Enforces
    {
        public int measurement { get; set; }

        public EnforcesId() { }

        public EnforcesId(string eCountry, string eStartDate, string eFinalDate, int eId, int eMeasurement) : base(eCountry, eStartDate, eFinalDate, eId)
        {
            measurement = eMeasurement;
        }
    }
}