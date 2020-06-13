using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Contact
    {
        public int person { get; set; }
        public int patient { get; set; }

        public Contact(){}

        public Contact(int cPerson, int cPatient)
        {
            person = cPerson;
            patient = cPatient;
        }
    }
}