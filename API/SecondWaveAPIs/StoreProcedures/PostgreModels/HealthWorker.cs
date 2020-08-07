using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProcedures.PostgreModels
{
    [Table("health_worker")]
    public partial class HealthWorker
    {
        [Key]
        [Column("ssn")]
        [StringLength(15)]
        public string Ssn { get; set; }
        [Required]
        [Column("fname")]
        [StringLength(15)]
        public string Fname { get; set; }
        [Required]
        [Column("lname")]
        [StringLength(15)]
        public string Lname { get; set; }
        [Required]
        [Column("phone")]
        [StringLength(15)]
        public string Phone { get; set; }
        [Column("birthdate", TypeName = "date")]
        public DateTime Birthdate { get; set; }
        [Required]
        [Column("role")]
        [StringLength(15)]
        public string Role { get; set; }
        [Key]
        [Column("hospital_id")]
        public int HospitalId { get; set; }
        [Column("sex")]
        public char Sex { get; set; }
        [Required]
        [Column("email")]
        [StringLength(15)]
        public string Email { get; set; }
        [Required]
        [Column("address")]
        public string Address { get; set; }
        [Column("startdate", TypeName = "date")]
        public DateTime Startdate { get; set; }

        [ForeignKey(nameof(HospitalId))]
        [InverseProperty("HealthWorker")]
        public virtual Hospital Hospital { get; set; }
    }
}
