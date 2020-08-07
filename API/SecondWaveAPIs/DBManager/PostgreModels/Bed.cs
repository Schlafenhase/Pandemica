using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        [Column("lounge_number")]
        public int LoungeNumber { get; set; }
        [Column("icu")]
        public bool Icu { get; set; }

        public virtual Lounge LoungeNumberNavigation { get; set; }
        public virtual ICollection<BedEquipment> BedEquipment { get; set; }
    }
}
