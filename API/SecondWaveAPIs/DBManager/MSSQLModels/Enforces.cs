using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("ENFORCES")]
    public partial class Enforces
    {
        [Key]
        [StringLength(30)]
        public string Country { get; set; }
        [Key]
        public int Measurement { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FinalDate { get; set; }
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Country))]
        [InverseProperty("Enforces")]
        public virtual Country CountryNavigation { get; set; }
        [ForeignKey(nameof(Measurement))]
        [InverseProperty(nameof(SanitaryMeasurements.Enforces))]
        public virtual SanitaryMeasurements MeasurementNavigation { get; set; }
    }
}
