using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("agent", Schema = "dss")]
    public partial class Agent
    {
        public Agent()
        {
            AgentInstance = new HashSet<AgentInstance>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        [StringLength(140)]
        public string Name { get; set; }
        [Column("subscriptionid")]
        public Guid? Subscriptionid { get; set; }
        [Column("state")]
        public int? State { get; set; }
        [Column("lastalivetime", TypeName = "datetime")]
        public DateTime? Lastalivetime { get; set; }
        [Column("is_on_premise")]
        public bool IsOnPremise { get; set; }
        [Column("version")]
        [StringLength(40)]
        public string Version { get; set; }
        [Column("password_hash")]
        [MaxLength(256)]
        public byte[] PasswordHash { get; set; }
        [Column("password_salt")]
        [MaxLength(256)]
        public byte[] PasswordSalt { get; set; }

        [ForeignKey(nameof(Subscriptionid))]
        [InverseProperty("Agent")]
        public virtual Subscription Subscription { get; set; }
        [InverseProperty("Agent")]
        public virtual ICollection<AgentInstance> AgentInstance { get; set; }
    }
}
