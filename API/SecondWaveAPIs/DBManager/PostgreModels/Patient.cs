using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.PostgreModels
{
    [Table("patient")]
    public partial class Patient
    {
        public Patient()
        {
            Reservation = new HashSet<Reservation>();
        }

        [Key]
        [Column("ssn")]
        [StringLength(15)]
        public string Ssn { get; set; }
        [Column("email")]
        [StringLength(15)]
        public string Email { get; set; }

        [InverseProperty("Patient")]
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
