using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBManager.Source.Entities
{
    public class HealthWorkerView
    {
        public string Ssn { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Phone { get; set; }
        public string Birthdate { get; set; }
        public string Role { get; set; }
        public char Sex { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Startdate { get; set; }

        public HealthWorkerView() { }

        public HealthWorkerView(string hwSsn, string hwFname, string hwLname, string hwPhone, string hwBirthdate, string hwRole, char hwSex, string hwEmail, string hwAddress, string hwStartdate)
        {
            Ssn = hwSsn;
            Fname = hwFname;
            Lname = hwLname;
            Phone = hwPhone;
            Birthdate = hwBirthdate;
            Role = hwRole;
            Sex = hwSex;
            Email = hwEmail;
            Address = hwAddress;
            Startdate = hwStartdate;
        }
    }
}