using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("agent_version", Schema = "dss")]
    public partial class AgentVersion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Version { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpiresOn { get; set; }
        [StringLength(200)]
        public string Comment { get; set; }
    }
}
