using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProcedures.PostgreModels
{
    [Table("hospital")]
    public partial class Hospital
    {
        public Hospital()
        {
            HealthWorker = new HashSet<HealthWorker>();
            HospitalProcedure = new HashSet<HospitalProcedure>();
            Lounge = new HashSet<Lounge>();
            Reservation = new HashSet<Reservation>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [InverseProperty("Hospital")]
        public virtual ICollection<HealthWorker> HealthWorker { get; set; }
        [InverseProperty("Hospital")]
        public virtual ICollection<HospitalProcedure> HospitalProcedure { get; set; }
        [InverseProperty("Hospital")]
        public virtual ICollection<Lounge> Lounge { get; set; }
        [InverseProperty("Hospital")]
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
