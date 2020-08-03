using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("EnumType", Schema = "dss")]
    public partial class EnumType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Type { get; set; }
        public int EnumId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModified { get; set; }
    }
}
