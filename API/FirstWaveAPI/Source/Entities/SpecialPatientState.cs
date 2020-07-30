using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class SpecialPatientState
    {
        public string name { get; set; }
        public string date { get; set; }

        public SpecialPatientState() { }

        public SpecialPatientState(string spsName, string spsDate)
        {
            name = spsName;
            date = spsDate;
        }
    }
}