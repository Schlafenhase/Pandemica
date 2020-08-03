using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("COUNTRY")]
    public partial class Country
    {
        public Country()
        {
            Enforces = new HashSet<Enforces>();
            Hospital = new HashSet<Hospital>();
            PatientCountryNavigation = new HashSet<Patient>();
            PatientNationalityNavigation = new HashSet<Patient>();
            ProvinceStateRegion = new HashSet<ProvinceStateRegion>();
        }

        [Key]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(15)]
        public string ContinentName { get; set; }
        [Required]
        [Column("EMail")]
        [StringLength(25)]
        public string Email { get; set; }

        [ForeignKey(nameof(ContinentName))]
        [InverseProperty(nameof(Continent.Country))]
        public virtual Continent ContinentNameNavigation { get; set; }
        [InverseProperty("CountryNavigation")]
        public virtual ICollection<Enforces> Enforces { get; set; }
        [InverseProperty("CountryNavigation")]
        public virtual ICollection<Hospital> Hospital { get; set; }
        [InverseProperty(nameof(Patient.CountryNavigation))]
        public virtual ICollection<Patient> PatientCountryNavigation { get; set; }
        [InverseProperty(nameof(Patient.NationalityNavigation))]
        public virtual ICollection<Patient> PatientNationalityNavigation { get; set; }
        [InverseProperty("CountryNavigation")]
        public virtual ICollection<ProvinceStateRegion> ProvinceStateRegion { get; set; }
    }
}
