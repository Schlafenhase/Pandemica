using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class PersonWithDateOfContact : Person
    {
        public string contactDate { get; set; }

        public PersonWithDateOfContact() { }

        public PersonWithDateOfContact(string pSsn, string pFirstName, string pLastName, string pBirthDate, string pEMail, string pAddress, string pSex, string pContactDate) : base(pSsn, pFirstName, pLastName, pBirthDate, pEMail, pAddress, pSex)
        {
            contactDate = pContactDate;
        }
    }
}