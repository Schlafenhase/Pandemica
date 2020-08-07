using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreProcedures.Source.Entities
{
    public class MedicalHistory
    {
        public string Procedure { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }

        public MedicalHistory() { }

        public MedicalHistory(string mhvProcedure, DateTime mhvDate, int mhvDuration)
        {
            Procedure = mhvProcedure;
            Date = mhvDate;
            Duration = mhvDuration;
        }
    }
}