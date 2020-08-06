using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.PostgreModels
{
    [Table("procedure")]
    public partial class Procedure
    {
        public Procedure()
        {
            HospitalProcedure = new HashSet<HospitalProcedure>();
            Reservation = new HashSet<Reservation>();
            ReservationProcedures = new HashSet<ReservationProcedures>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(15)]
        public string Name { get; set; }
        [Column("duration")]
        public int Duration { get; set; }

        [InverseProperty("Procedure")]
        public virtual ICollection<HospitalProcedure> HospitalProcedure { get; set; }
        [InverseProperty("Procedure")]
        public virtual ICollection<Reservation> Reservation { get; set; }
        [InverseProperty("Procedure")]
        public virtual ICollection<ReservationProcedures> ReservationProcedures { get; set; }
    }
}
