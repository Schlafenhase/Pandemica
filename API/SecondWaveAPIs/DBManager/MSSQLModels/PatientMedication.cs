using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("PATIENT_MEDICATION")]
    public partial class PatientMedication
    {
        [Key]
        [StringLength(15)]
        public string Patient { get; set; }
        [Key]
        public int Medication { get; set; }
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Medication))]
        [InverseProperty("PatientMedication")]
        public virtual Medication MedicationNavigation { get; set; }
        [ForeignKey(nameof(Patient))]
        [InverseProperty("PatientMedication")]
        public virtual Patient PatientNavigation { get; set; }
    }
}
