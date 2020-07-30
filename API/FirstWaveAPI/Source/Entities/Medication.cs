using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Medication
    {
        public int id { get; set; }
        public string name { get; set; }
        public string pharmacy { get; set; }

        public Medication(){}

        public Medication(int pId, string pName, string pPharmacy)
        {
            id = pId;
            name = pName;
            pharmacy = pPharmacy;
        }
    }
}