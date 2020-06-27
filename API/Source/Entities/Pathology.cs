using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Pathology
    {
        public string name { get; set; }
        public string symptoms { get; set; }
        public string description { get; set; }
        public string treatment { get; set; }
        public int id { get; set; }

        public Pathology(){}

        public Pathology(string pName, string pSymptoms, string pDescription, string pTreatment, int pID)
        {
            name = pName;
            symptoms = pSymptoms;
            description = pDescription;
            treatment = pTreatment;
            id = pID;
        }
    }
}