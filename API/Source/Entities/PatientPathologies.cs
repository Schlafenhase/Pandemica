using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class PatientPathologies
    {
        public string patient { get; set; }
        public int pathology { get; set; }
  
        public PatientPathologies(){}

        public PatientPathologies(string ppPatient, int ppPathology)
        {
            patient = ppPatient;
            pathology = ppPathology;
        }
    }
}