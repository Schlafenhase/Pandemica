using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("subscription", Schema = "dss")]
    public partial class Subscription
    {
        public Subscription()
        {
            Agent = new HashSet<Agent>();
            Syncgroup = new HashSet<Syncgroup>();
            Userdatabase = new HashSet<Userdatabase>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        [StringLength(140)]
        public string Name { get; set; }
        [Column("creationtime", TypeName = "datetime")]
        public DateTime? Creationtime { get; set; }
        [Column("lastlogintime", TypeName = "datetime")]
        public DateTime? Lastlogintime { get; set; }
        [Column("tombstoneretentionperiodindays")]
        public int Tombstoneretentionperiodindays { get; set; }
        [Column("policyid")]
        public int? Policyid { get; set; }
        [Column("subscriptionstate")]
        public byte Subscriptionstate { get; set; }
        public Guid? WindowsAzureSubscriptionId { get; set; }
        public bool? EnableDetailedProviderTracing { get; set; }
        [Column("syncserveruniquename")]
        [StringLength(256)]
        public string Syncserveruniquename { get; set; }
        [Column("version")]
        [StringLength(40)]
        public string Version { get; set; }

        [InverseProperty("Subscription")]
        public virtual ICollection<Agent> Agent { get; set; }
        [InverseProperty("Subscription")]
        public virtual ICollection<Syncgroup> Syncgroup { get; set; }
        [InverseProperty("Subscription")]
        public virtual ICollection<Userdatabase> Userdatabase { get; set; }
    }
}
