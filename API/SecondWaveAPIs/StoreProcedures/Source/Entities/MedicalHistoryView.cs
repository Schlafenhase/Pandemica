using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreProcedures.Source.Entities
{
    public class MedicalHistoryView
    {
        public string Procedure { get; set; }
        public string Startdate { get; set; }
        public int Duration { get; set; }

        public MedicalHistoryView() { }

        public MedicalHistoryView(string mhvProcedure, string mhvStartdate, int mhvDuration)
        {
            Procedure = mhvProcedure;
            Startdate = mhvStartdate;
            Duration = mhvDuration;
        }
    }
}