using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("userdatabase", Schema = "dss")]
    public partial class Userdatabase
    {
        public Userdatabase()
        {
            Syncgroup = new HashSet<Syncgroup>();
            Syncgroupmember = new HashSet<Syncgroupmember>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("server")]
        [StringLength(256)]
        public string Server { get; set; }
        [Column("database")]
        [StringLength(256)]
        public string Database { get; set; }
        [Column("state")]
        public int State { get; set; }
        [Column("subscriptionid")]
        public Guid Subscriptionid { get; set; }
        [Column("agentid")]
        public Guid Agentid { get; set; }
        [Column("connection_string")]
        public byte[] ConnectionString { get; set; }
        [Column("db_schema")]
        public string DbSchema { get; set; }
        [Column("is_on_premise")]
        public bool IsOnPremise { get; set; }
        [Column("sqlazure_info", TypeName = "xml")]
        public string SqlazureInfo { get; set; }
        [Column("last_schema_updated", TypeName = "datetime")]
        public DateTime? LastSchemaUpdated { get; set; }
        [Column("last_tombstonecleanup", TypeName = "datetime")]
        public DateTime? LastTombstonecleanup { get; set; }
        [Column("region")]
        [StringLength(256)]
        public string Region { get; set; }
        [Column("jobId")]
        public Guid? JobId { get; set; }

        [ForeignKey(nameof(Subscriptionid))]
        [InverseProperty("Userdatabase")]
        public virtual Subscription Subscription { get; set; }
        [InverseProperty("HubMember")]
        public virtual ICollection<Syncgroup> Syncgroup { get; set; }
        [InverseProperty("Database")]
        public virtual ICollection<Syncgroupmember> Syncgroupmember { get; set; }
    }
}
