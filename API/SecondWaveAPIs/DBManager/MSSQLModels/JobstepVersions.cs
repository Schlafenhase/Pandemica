using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    public partial class JobstepVersions
    {
        [Required]
        [Column("job_name")]
        [StringLength(128)]
        public string JobName { get; set; }
        [Column("job_id")]
        public Guid JobId { get; set; }
        [Column("job_version")]
        public int JobVersion { get; set; }
        [Column("step_id")]
        public int StepId { get; set; }
        [Required]
        [Column("step_name")]
        [StringLength(128)]
        public string StepName { get; set; }
        [Required]
        [Column("command_type")]
        [StringLength(50)]
        public string CommandType { get; set; }
        [Column("command_source")]
        [StringLength(50)]
        public string CommandSource { get; set; }
        [Column("command")]
        public string Command { get; set; }
        [Column("credential_name")]
        [StringLength(128)]
        public string CredentialName { get; set; }
        [Column("target_group_name")]
        [StringLength(128)]
        public string TargetGroupName { get; set; }
        [Column("target_group_id")]
        public Guid? TargetGroupId { get; set; }
        [Column("initial_retry_interval_seconds")]
        public int? InitialRetryIntervalSeconds { get; set; }
        [Column("maximum_retry_interval_seconds")]
        public int? MaximumRetryIntervalSeconds { get; set; }
        [Column("retry_interval_backoff_multiplier")]
        public float RetryIntervalBackoffMultiplier { get; set; }
        [Column("retry_attempts")]
        public int RetryAttempts { get; set; }
        [Column("step_timeout_seconds")]
        public int? StepTimeoutSeconds { get; set; }
        [Column("output_type")]
        [StringLength(50)]
        public string OutputType { get; set; }
        [Column("output_credential_name")]
        [StringLength(128)]
        public string OutputCredentialName { get; set; }
        [Column("output_subscription_id")]
        public Guid? OutputSubscriptionId { get; set; }
        [Column("output_resource_group_name")]
        [StringLength(128)]
        public string OutputResourceGroupName { get; set; }
        [Column("output_server_name")]
        [StringLength(256)]
        public string OutputServerName { get; set; }
        [Column("output_database_name")]
        [StringLength(128)]
        public string OutputDatabaseName { get; set; }
        [Column("output_schema_name")]
        [StringLength(128)]
        public string OutputSchemaName { get; set; }
        [Column("output_table_name")]
        [StringLength(128)]
        public string OutputTableName { get; set; }
        [Column("max_parallelism")]
        public int? MaxParallelism { get; set; }
    }
}
