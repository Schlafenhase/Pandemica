using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("MetaInformation", Schema = "dss")]
    public partial class MetaInformation
    {
        [Key]
        public int Id { get; set; }
        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public int VersionBuild { get; set; }
        public int VersionService { get; set; }
        [StringLength(50)]
        public string VersionString { get; set; }
        public long? Version { get; set; }
        [Required]
        public bool? State { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Timestamp { get; set; }
    }
}
