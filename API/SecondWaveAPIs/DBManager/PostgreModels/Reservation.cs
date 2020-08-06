using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.PostgreModels
{
    [Table("reservation")]
    public partial class Reservation
    {
        public Reservation()
        {
            ReservationProcedures = new HashSet<ReservationProcedures>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Key]
        [Column("procedure_id")]
        public int ProcedureId { get; set; }
        [Column("startdate", TypeName = "date")]
        public DateTime Startdate { get; set; }
        [Key]
        [Column("hospital_id")]
        public int HospitalId { get; set; }
        [Key]
        [Column("patient_id")]
        [StringLength(15)]
        public string PatientId { get; set; }

        [ForeignKey(nameof(HospitalId))]
        [InverseProperty("Reservation")]
        public virtual Hospital Hospital { get; set; }
        [ForeignKey(nameof(PatientId))]
        [InverseProperty("Reservation")]
        public virtual Patient Patient { get; set; }
        [ForeignKey(nameof(ProcedureId))]
        [InverseProperty("Reservation")]
        public virtual Procedure Procedure { get; set; }
        public virtual ICollection<ReservationProcedures> ReservationProcedures { get; set; }
    }
}
