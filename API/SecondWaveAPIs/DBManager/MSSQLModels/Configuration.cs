using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("configuration", Schema = "dss")]
    public partial class Configuration
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ConfigKey { get; set; }
        [Required]
        [StringLength(256)]
        public string ConfigValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModified { get; set; }
    }
}
