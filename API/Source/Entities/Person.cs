using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Person
    {
        public int ssn { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string eMail { get; set; }
        public string address { get; set; }

        public Person(){}

        public Person(int pSsn, string pFirstName, string pLastName, int pAge, string pEMail, string pAddress)
        {
            ssn = pSsn;
            firstName = pFirstName;
            lastName = pLastName;
            age = pAge;
            eMail = pEMail;
            address = pAddress;
        }
    }
}