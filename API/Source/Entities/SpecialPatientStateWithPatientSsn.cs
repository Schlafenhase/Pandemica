using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class SpecialPatientStateWithPatientSsn : SpecialPatientState
    {
        public string patientSsn { get; set; }

        public SpecialPatientStateWithPatientSsn() { }

        public SpecialPatientStateWithPatientSsn(string spsName, string spsDate, string spsPatientSsn) : base(spsName, spsDate)
        {
            patientSsn = spsPatientSsn;
        }
    }
}