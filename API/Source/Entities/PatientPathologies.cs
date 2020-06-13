using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class PatientPathologies
    {
        public int patient { get; set; }
        public string pathology { get; set; }
  
        public PatientPathologies(){}

        public PatientPathologies(int ppPatient, string ppPathology)
        {
            patient = ppPatient;
            pathology = ppPathology;
        }
    }
}