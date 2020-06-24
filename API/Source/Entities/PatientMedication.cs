using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class PatientMedication
    {
        public string patient { get; set; }
        public int medication { get; set; }

        public PatientMedication(){}

        public PatientMedication(string pmPatient, int pmMedication)
        {
            patient = pmPatient;
            medication = pmMedication;
        }
    }
}