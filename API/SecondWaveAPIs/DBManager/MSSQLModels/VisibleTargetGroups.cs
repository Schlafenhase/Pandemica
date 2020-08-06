using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    public partial class VisibleTargetGroups
    {
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
    }
}
