using System;
using System.Collections.Generic;

namespace DBManager.PostgreModels
{
    public partial class Hospital
    {
        public Hospital()
        {
            HealthWorker = new HashSet<HealthWorker>();
            HospitalProcedure = new HashSet<HospitalProcedure>();
            Lounge = new HashSet<Lounge>();
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        public virtual ICollection<HealthWorker> HealthWorker { get; set; }
        public virtual ICollection<HospitalProcedure> HospitalProcedure { get; set; }
        public virtual ICollection<Lounge> Lounge { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
