using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("PERSON")]
    public partial class Person
    {
        public Person()
        {
            Contact = new HashSet<Contact>();
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
        public DateTime BirthDate { get; set; }
        [Required]
        [Column("EMail")]
        [StringLength(25)]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Address { get; set; }
        [Required]
        [StringLength(1)]
        public string Sex { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual ICollection<Contact> Contact { get; set; }
    }
}
