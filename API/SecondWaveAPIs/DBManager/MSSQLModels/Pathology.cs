using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("PATHOLOGY")]
    public partial class Pathology
    {
        public Pathology()
        {
            PatientPathologies = new HashSet<PatientPathologies>();
        }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Symptoms { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Treatment { get; set; }
        [Key]
        public int Id { get; set; }

        [InverseProperty("PathologyNavigation")]
        public virtual ICollection<PatientPathologies> PatientPathologies { get; set; }
    }
}
