using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("PATIENT_PATHOLOGIES")]
    public partial class PatientPathologies
    {
        [Key]
        [StringLength(15)]
        public string Patient { get; set; }
        [Key]
        public int Pathology { get; set; }
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Pathology))]
        [InverseProperty("PatientPathologies")]
        public virtual Pathology PathologyNavigation { get; set; }
        [ForeignKey(nameof(Patient))]
        [InverseProperty("PatientPathologies")]
        public virtual Patient PatientNavigation { get; set; }
    }
}
