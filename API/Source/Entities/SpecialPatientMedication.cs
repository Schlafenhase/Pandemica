using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class SpecialPatientMedication
    {
        public string name { get; set; }
        public string pharmacy { get; set; }

        public SpecialPatientMedication() { }

        public SpecialPatientMedication(string spmName, string spmPharmacy)
        {
            name = spmName;
            pharmacy = spmPharmacy;
        }
    }
}