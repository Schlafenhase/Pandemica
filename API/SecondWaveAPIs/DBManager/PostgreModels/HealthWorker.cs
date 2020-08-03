using System;
using System.Collections.Generic;

namespace DBManager.PostgreModels
{
    public partial class HealthWorker
    {
        public string Ssn { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public string Role { get; set; }
        public int HospitalId { get; set; }
        public char Sex { get; set; }
        public string Email { get; set; }

        public virtual Hospital Hospital { get; set; }
    }
}
