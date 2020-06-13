using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class PatientState
    {
        public string state { get; set; }
        public int patient { get; set; }
        public string date { get; set; }

        public PatientState(){}

        public PatientState(string psState, int psPatient, string psDate)
        {
            state = psState;
            patient = psPatient;
            date = psDate;
        }
    }
}