using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("PROVINCE_STATE_REGION")]
    public partial class ProvinceStateRegion
    {
        [Key]
        [StringLength(15)]
        public string Name { get; set; }
        [Key]
        [StringLength(30)]
        public string Country { get; set; }
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Country))]
        [InverseProperty("ProvinceStateRegion")]
        public virtual Country CountryNavigation { get; set; }
    }
}
