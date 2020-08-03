using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("MEDICATION")]
    public partial class Medication
    {
        public Medication()
        {
            PatientMedication = new HashSet<PatientMedication>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        [StringLength(15)]
        public string Pharmacy { get; set; }

        [InverseProperty("MedicationNavigation")]
        public virtual ICollection<PatientMedication> PatientMedication { get; set; }
    }
}
