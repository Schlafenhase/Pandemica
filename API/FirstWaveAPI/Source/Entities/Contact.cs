using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Contact
    {
        public string person { get; set; }
        public string patient { get; set; }
        public int id { get; set; }
        public string contactDate { get; set; }

        public Contact(){}

        public Contact(string cPerson, string cPatient, int cId, string cDate)
        {
            person = cPerson;
            patient = cPatient;
            id = cId;
            contactDate = cDate;
        }
    }
}