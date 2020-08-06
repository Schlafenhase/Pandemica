using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.PostgreModels
{
    [Table("lounge")]
    public partial class Lounge
    {
        public Lounge()
        {
            Bed = new HashSet<Bed>();
        }

        [Key]
        [Column("number")]
        public int Number { get; set; }
        [Column("floor")]
        public int Floor { get; set; }
        [Required]
        [Column("name")]
        [StringLength(15)]
        public string Name { get; set; }
        [Required]
        [Column("type")]
        [StringLength(15)]
        public string Type { get; set; }
        [Key]
        [Column("hospital_id")]
        public int HospitalId { get; set; }
        [Column("bed_capacity")]
        public int BedCapacity { get; set; }

        [ForeignKey(nameof(HospitalId))]
        [InverseProperty("Lounge")]
        public virtual Hospital Hospital { get; set; }
        public virtual ICollection<Bed> Bed { get; set; }
    }
}
