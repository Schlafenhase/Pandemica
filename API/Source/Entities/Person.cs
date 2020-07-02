using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Person
    {
        public string ssn { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string birthDate { get; set; }
        public string eMail { get; set; }
        public string address { get; set; }
        public string sex { get; set; }

        public Person(){}

        public Person(string pSsn, string pFirstName, string pLastName, string pBirthDate, string pEMail, string pAddress, string pSex)
        {
            ssn = pSsn;
            firstName = pFirstName;
            lastName = pLastName;
            birthDate = pBirthDate;
            eMail = pEMail;
            address = pAddress;
            sex = pSex;
        }
    }
}