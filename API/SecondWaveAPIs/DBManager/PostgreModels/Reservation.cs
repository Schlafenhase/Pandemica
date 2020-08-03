using System;
using System.Collections.Generic;

namespace DBManager.PostgreModels
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public string Procedure { get; set; }
        public DateTime Startdate { get; set; }
        public int HospitalId { get; set; }
        public string PatientId { get; set; }

        public virtual Hospital Hospital { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
