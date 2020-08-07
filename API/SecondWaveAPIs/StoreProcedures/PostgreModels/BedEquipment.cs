using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProcedures.PostgreModels
{
    [Table("bed_equipment")]
    public partial class BedEquipment
    {
        [Key]
        [Column("bed_number")]
        public int BedNumber { get; set; }
        [Key]
        [Column("equipment_id")]
        public int EquipmentId { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }

        public virtual Bed BedNumberNavigation { get; set; }
        [ForeignKey(nameof(EquipmentId))]
        [InverseProperty("BedEquipment")]
        public virtual Equipment Equipment { get; set; }
    }
}
