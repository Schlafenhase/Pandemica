using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("scaleunitlimits", Schema = "dss")]
    public partial class Scaleunitlimits
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int MaxValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModified { get; set; }
    }
}
