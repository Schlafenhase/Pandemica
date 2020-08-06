using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("PATIENT")]
    public partial class Patient
    {
        public Patient()
        {
            Contact = new HashSet<Contact>();
            PatientMedication = new HashSet<PatientMedication>();
            PatientPathologies = new HashSet<PatientPathologies>();
            PatientState = new HashSet<PatientState>();
        }

        [Key]
        [StringLength(15)]
        public string Ssn { get; set; }
        [Required]
        [StringLength(15)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15)]
        public string LastName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        public bool Hospitalized { get; set; }
        [Column("ICU")]
        public bool Icu { get; set; }
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
        [Required]
        [StringLength(15)]
        public string Region { get; set; }
        [Required]
        [StringLength(30)]
        public string Nationality { get; set; }
        public int Hospital { get; set; }
        [Required]
        [StringLength(1)]
        public string Sex { get; set; }

        [ForeignKey(nameof(Country))]
        [InverseProperty("PatientCountryNavigation")]
        public virtual Country CountryNavigation { get; set; }
        [ForeignKey(nameof(Hospital))]
        [InverseProperty("Patient")]
        public virtual Hospital HospitalNavigation { get; set; }
        [ForeignKey(nameof(Nationality))]
        [InverseProperty("PatientNationalityNavigation")]
        public virtual Country NationalityNavigation { get; set; }
        [InverseProperty("PatientNavigation")]
        public virtual ICollection<Contact> Contact { get; set; }
        [InverseProperty("PatientNavigation")]
        public virtual ICollection<PatientMedication> PatientMedication { get; set; }
        [InverseProperty("PatientNavigation")]
        public virtual ICollection<PatientPathologies> PatientPathologies { get; set; }
        [InverseProperty("PatientNavigation")]
        public virtual ICollection<PatientState> PatientState { get; set; }
    }
}
