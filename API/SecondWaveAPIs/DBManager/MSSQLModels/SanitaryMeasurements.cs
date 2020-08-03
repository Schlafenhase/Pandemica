using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("SANITARY_MEASUREMENTS")]
    public partial class SanitaryMeasurements
    {
        public SanitaryMeasurements()
        {
            Enforces = new HashSet<Enforces>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }

        [InverseProperty("MeasurementNavigation")]
        public virtual ICollection<Enforces> Enforces { get; set; }
    }
}
