using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class PersonWithPatientSsn : PersonWithDateOfContact
    {
        public string patientSsn { get; set; }

        public PersonWithPatientSsn() { }

        public PersonWithPatientSsn(string pSsn, string pFirstName, string pLastName, string pBirthDate, string pEMail, string pAddress, string pSex, string pContactDate, string pPatientSsn) : base(pSsn, pFirstName, pLastName, pBirthDate, pEMail, pAddress, pSex, pContactDate)
        {
            patientSsn = pPatientSsn;
        }
    }
}