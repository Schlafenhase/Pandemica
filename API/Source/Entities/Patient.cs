using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Patient
    {
        public string ssn { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string birthDate { get; set; }
        public bool hospitalized { get; set; }
        public bool icu { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string nationality { get; set; }
        public int hospital { get; set; }

        public Patient(){}

        public Patient(string pSsn, string pFirstName, string pLastName, string pbirthDate, bool pHospitalized, bool pIcu, string pState, string pCountry, string pRegion, string pNationality, int pHospital)
        {
            ssn = pSsn;
            firstName = pFirstName;
            lastName = pLastName;
            birthDate = pbirthDate;
            hospitalized = pHospitalized;
            icu = pIcu;
            country = pCountry;
            region = pRegion;
            nationality = pNationality;
            hospital = pHospital;
        }
    }
}