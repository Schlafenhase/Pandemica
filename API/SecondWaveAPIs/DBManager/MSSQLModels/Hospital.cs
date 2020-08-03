using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("HOSPITAL")]
    public partial class Hospital
    {
        public Hospital()
        {
            Patient = new HashSet<Patient>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        public int Phone { get; set; }
        [Required]
        [StringLength(15)]
        public string ManagerName { get; set; }
        public int? Capacity { get; set; }
        [Column("ICUCapacity")]
        public int? Icucapacity { get; set; }
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
        [StringLength(15)]
        public string Region { get; set; }
        [Required]
        [Column("EMail")]
        [StringLength(25)]
        public string Email { get; set; }

        [ForeignKey(nameof(Country))]
        [InverseProperty("Hospital")]
        public virtual Country CountryNavigation { get; set; }
        [InverseProperty("HospitalNavigation")]
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
