using System;
using System.Collections.Generic;

namespace DBManager.PostgreModels
{
    public partial class Patient
    {
        public Patient()
        {
            Reservation = new HashSet<Reservation>();
        }

        public string Ssn { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
