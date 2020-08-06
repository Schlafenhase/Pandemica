using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("CONTINENT")]
    public partial class Continent
    {
        public Continent()
        {
            Country = new HashSet<Country>();
        }

        [Key]
        [StringLength(15)]
        public string Name { get; set; }

        [InverseProperty("ContinentNameNavigation")]
        public virtual ICollection<Country> Country { get; set; }
    }
}
