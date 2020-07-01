using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class SpecialPatientPathologiesWithPatientSsn : SpecialPatientPathologies
    {
        public string patientSsn { get; set; }

        public SpecialPatientPathologiesWithPatientSsn() { }

        public SpecialPatientPathologiesWithPatientSsn(string sppName, string sppSymptoms, string sppDescription, string sppTreatment, string sppPatientSsn) : base(sppName, sppSymptoms, sppDescription, sppTreatment)
        {
            patientSsn = sppPatientSsn;
        }
    }
}