using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("targets", Schema = "jobs_internal")]
    public partial class Targets
    {
        public Targets()
        {
            JobExecutions1 = new HashSet<JobExecutions1>();
            JobstepDataResultSetDestinationTarget = new HashSet<JobstepData>();
            JobstepDataTarget = new HashSet<JobstepData>();
            TargetAssociationsChildTarget = new HashSet<TargetAssociations>();
            TargetAssociationsParentTarget = new HashSet<TargetAssociations>();
            TargetGroupMembershipsChildTarget = new HashSet<TargetGroupMemberships>();
            TargetGroupMembershipsParentTarget = new HashSet<TargetGroupMemberships>();
        }

        [Key]
        [Column("target_id")]
        public Guid TargetId { get; set; }
        [Required]
        [Column("row_version")]
        public byte[] RowVersion { get; set; }
        [Column("last_completed_refresh_start_time")]
        public DateTime? LastCompletedRefreshStartTime { get; set; }
        [Column("target_group_name")]
        [StringLength(128)]
        public string TargetGroupName { get; set; }
        [Column("delete_requested_time")]
        public DateTime? DeleteRequestedTime { get; set; }
        [Column("subscription_id")]
        public Guid? SubscriptionId { get; set; }
        [Column("resource_group_name")]
        [StringLength(128)]
        public string ResourceGroupName { get; set; }
        [Column("server_name")]
        [StringLength(256)]
        public string ServerName { get; set; }
        [Column("database_name")]
        [StringLength(128)]
        public string DatabaseName { get; set; }
        [Column("refresh_credential_name")]
        [StringLength(128)]
        public string RefreshCredentialName { get; set; }
        [Column("elastic_pool_name")]
        [StringLength(128)]
        public string ElasticPoolName { get; set; }
        [Column("shard_map_name")]
        [StringLength(128)]
        public string ShardMapName { get; set; }
        [Required]
        [Column("target_type")]
        [StringLength(128)]
        public string TargetType { get; set; }

        [InverseProperty("Target")]
        public virtual ICollection<JobExecutions1> JobExecutions1 { get; set; }
        [InverseProperty(nameof(JobstepData.ResultSetDestinationTarget))]
        public virtual ICollection<JobstepData> JobstepDataResultSetDestinationTarget { get; set; }
        [InverseProperty(nameof(JobstepData.Target))]
        public virtual ICollection<JobstepData> JobstepDataTarget { get; set; }
        [InverseProperty(nameof(TargetAssociations.ChildTarget))]
        public virtual ICollection<TargetAssociations> TargetAssociationsChildTarget { get; set; }
        [InverseProperty(nameof(TargetAssociations.ParentTarget))]
        public virtual ICollection<TargetAssociations> TargetAssociationsParentTarget { get; set; }
        [InverseProperty(nameof(TargetGroupMemberships.ChildTarget))]
        public virtual ICollection<TargetGroupMemberships> TargetGroupMembershipsChildTarget { get; set; }
        [InverseProperty(nameof(TargetGroupMemberships.ParentTarget))]
        public virtual ICollection<TargetGroupMemberships> TargetGroupMembershipsParentTarget { get; set; }
    }
}
