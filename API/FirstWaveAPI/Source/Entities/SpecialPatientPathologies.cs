using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class SpecialPatientPathologies
    {
        public string name { get; set; }
        public string symptoms { get; set; }
        public string description { get; set; }
        public string treatment { get; set; }

        public SpecialPatientPathologies() { }

        public SpecialPatientPathologies(string sppName, string sppSymptoms, string sppDescription, string sppTreatment)
        {
            name = sppName;
            symptoms = sppSymptoms;
            description = sppDescription;
            treatment = sppTreatment;
        }
    }
}