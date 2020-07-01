using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class SpecialPatientMedicationWithPatientSsn : SpecialPatientMedication
    {
        public string patientSsn { get; set; }

        public SpecialPatientMedicationWithPatientSsn() { }

        public SpecialPatientMedicationWithPatientSsn(string spmName, string spmPharmacy, string psmPatientSsn) : base(spmName, spmPharmacy)
        {
            patientSsn = psmPatientSsn;
        }
    }
}