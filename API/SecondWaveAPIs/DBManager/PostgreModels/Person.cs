using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.PostgreModels
{
    [Table("person")]
    public partial class Person
    {
        [Key]
        [Column("ssn")]
        [StringLength(15)]
        public string Ssn { get; set; }
        [Column("email")]
        [StringLength(15)]
        public string Email { get; set; }
    }
}
