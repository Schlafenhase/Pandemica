using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.PostgreModels
{
    [Table("equipment")]
    public partial class Equipment
    {
        public Equipment()
        {
            BedEquipment = new HashSet<BedEquipment>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(15)]
        public string Name { get; set; }
        [Required]
        [Column("provider")]
        [StringLength(15)]
        public string Provider { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }

        [InverseProperty("Equipment")]
        public virtual ICollection<BedEquipment> BedEquipment { get; set; }
    }
}
