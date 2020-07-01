using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class PatientWithHospitalId : Patient
    {
        public int hospital { get; set; }

        public PatientWithHospitalId() { }

        public PatientWithHospitalId(string pSsn, string pFirstName, string pLastName, string pBirthDate, bool pHospitalized, bool pIcu, string pCountry, string pRegion, string pNationality, int pHospital, string pSex) : base(pSsn, pFirstName, pLastName, pBirthDate, pHospitalized, pIcu, pCountry, pRegion, pNationality, pSex)
        {    
            hospital = pHospital;
        }
    }
}