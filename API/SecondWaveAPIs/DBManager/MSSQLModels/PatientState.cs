using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("PATIENT_STATE")]
    public partial class PatientState
    {
        [Key]
        public int State { get; set; }
        [Key]
        [StringLength(15)]
        public string Patient { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Patient))]
        [InverseProperty("PatientState")]
        public virtual Patient PatientNavigation { get; set; }
        [ForeignKey(nameof(State))]
        [InverseProperty("PatientState")]
        public virtual State StateNavigation { get; set; }
    }
}
