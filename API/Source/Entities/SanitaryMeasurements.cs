using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class SanitaryMeasurements
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public SanitaryMeasurements(){}

        public SanitaryMeasurements(int smId, string smName, string smDescription)
        {
            id = smId;
            name = smName;
            description = smDescription;
        }
    }
}