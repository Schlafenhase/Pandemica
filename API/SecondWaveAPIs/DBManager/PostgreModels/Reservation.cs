using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.PostgreModels
{
    [Table("reservation")]
    public partial class Reservation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("procedure")]
        [StringLength(15)]
        public string Procedure { get; set; }
        [Column("startdate", TypeName = "date")]
        public DateTime Startdate { get; set; }
        [Column("role")]
        [StringLength(15)]
        public string Role { get; set; }
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
    }
}
