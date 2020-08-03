using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("CONTACT")]
    public partial class Contact
    {
        [Key]
        [StringLength(15)]
        public string Person { get; set; }
        [Key]
        [StringLength(15)]
        public string Patient { get; set; }
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Patient))]
        [InverseProperty("Contact")]
        public virtual Patient PatientNavigation { get; set; }
        [ForeignKey(nameof(Person))]
        [InverseProperty("Contact")]
        public virtual Person PersonNavigation { get; set; }
    }
}
