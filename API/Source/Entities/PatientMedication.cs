using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class PatientMedication
    {
        public int patient { get; set; }
        public int medication { get; set; }

        public PatientMedication(){}

        public PatientMedication(int pmPatient, int pmMedication)
        {
            patient = pmPatient;
            medication = pmMedication;
        }
    }
}