using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("agent_instance", Schema = "dss")]
    public partial class AgentInstance
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("agentid")]
        public Guid Agentid { get; set; }
        [Column("lastalivetime", TypeName = "datetime")]
        public DateTime? Lastalivetime { get; set; }
        [Required]
        [Column("version")]
        [StringLength(40)]
        public string Version { get; set; }

        [ForeignKey(nameof(Agentid))]
        [InverseProperty("AgentInstance")]
        public virtual Agent Agent { get; set; }
    }
}
