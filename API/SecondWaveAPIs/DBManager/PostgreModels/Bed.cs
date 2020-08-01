using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace DBManager.PostgreModels
{
    [Table("bed")]
    public partial class Bed
    {
        public Bed()
        {
            BedEquipment = new HashSet<BedEquipment>();
        }

        [Key]
        [Column("number")]
        public int Number { get; set; }
        [Required]
        [Column("icu", TypeName = "bit(1)")]
        public BitArray Icu { get; set; }
        [Key]
        [Column("lounge_number")]
        public int LoungeNumber { get; set; }

        public virtual Lounge LoungeNumberNavigation { get; set; }
        public virtual ICollection<BedEquipment> BedEquipment { get; set; }
    }
}
