using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class PatientState
    {
        public int state { get; set; }
        public string patient { get; set; }
        public string date { get; set; }
        public int id { get; set; }

        public PatientState(){}

        public PatientState(int psState, string psPatient, string psDate, int psId)
        {
            state = psState;
            patient = psPatient;
            date = psDate;
            id = psId;
        }
    }
}