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
        public string sex { get; set; }
        
        public Patient(){}

        public Patient(string pSsn, string pFirstName, string pLastName, string pBirthDate, bool pHospitalized, bool pIcu, string pCountry, string pRegion, string pNationality, string pSex)
        {
            ssn = pSsn;
            firstName = pFirstName;
            lastName = pLastName;
            birthDate = pBirthDate;
            hospitalized = pHospitalized;
            icu = pIcu;
            country = pCountry;
            region = pRegion;
            nationality = pNationality;
            sex = pSex;
        }
    }
}