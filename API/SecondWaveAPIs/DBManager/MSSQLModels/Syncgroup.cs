using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("syncgroup", Schema = "dss")]
    public partial class Syncgroup
    {
        public Syncgroup()
        {
            Syncgroupmember = new HashSet<Syncgroupmember>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        [StringLength(140)]
        public string Name { get; set; }
        [Column("subscriptionid")]
        public Guid? Subscriptionid { get; set; }
        [Column("schema_description", TypeName = "xml")]
        public string SchemaDescription { get; set; }
        [Column("state")]
        public int? State { get; set; }
        [Column("hub_memberid")]
        public Guid? HubMemberid { get; set; }
        [Column("conflict_resolution_policy")]
        public int ConflictResolutionPolicy { get; set; }
        [Column("sync_interval")]
        public int SyncInterval { get; set; }
        [Required]
        [Column("sync_enabled")]
        public bool? SyncEnabled { get; set; }
        [Column("lastupdatetime", TypeName = "datetime")]
        public DateTime? Lastupdatetime { get; set; }
        [Column("ocsschemadefinition")]
        public string Ocsschemadefinition { get; set; }
        [Column("hubhasdata")]
        public bool? Hubhasdata { get; set; }
        public bool ConflictLoggingEnabled { get; set; }
        public int ConflictTableRetentionInDays { get; set; }

        [ForeignKey(nameof(HubMemberid))]
        [InverseProperty(nameof(Userdatabase.Syncgroup))]
        public virtual Userdatabase HubMember { get; set; }
        [ForeignKey(nameof(Subscriptionid))]
        [InverseProperty("Syncgroup")]
        public virtual Subscription Subscription { get; set; }
        [InverseProperty("SyncGroup")]
        public virtual ScheduleTask ScheduleTask { get; set; }
        [InverseProperty("Syncgroup")]
        public virtual ICollection<Syncgroupmember> Syncgroupmember { get; set; }
    }
}
