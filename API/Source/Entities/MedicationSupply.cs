using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class MedicationSupply
    {
        public int medication { get; set; }
        public int hospital { get; set; }
        public int quantity { get; set; }

        public MedicationSupply(){}

        public MedicationSupply(int msMedication, int msHospital, int msQuantity)
        {
            medication = msMedication;
            hospital = msHospital;
            quantity = msQuantity;
        }
    }
}