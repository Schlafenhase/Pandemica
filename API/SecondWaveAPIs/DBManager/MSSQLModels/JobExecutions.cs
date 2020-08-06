using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    public partial class JobExecutions
    {
        [Column("job_execution_id")]
        public Guid JobExecutionId { get; set; }
        [Required]
        [Column("job_name")]
        [StringLength(128)]
        public string JobName { get; set; }
        [Column("job_id")]
        public Guid JobId { get; set; }
        [Column("job_version")]
        public int JobVersion { get; set; }
        [Column("step_name")]
        [StringLength(128)]
        public string StepName { get; set; }
        [Column("step_id")]
        public int? StepId { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Required]
        [Column("lifecycle")]
        [StringLength(50)]
        public string Lifecycle { get; set; }
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        [Column("start_time")]
        public DateTime? StartTime { get; set; }
        [Column("end_time")]
        public DateTime? EndTime { get; set; }
        [Column("current_attempts")]
        public int CurrentAttempts { get; set; }
        [Column("current_attempt_start_time")]
        public DateTime? CurrentAttemptStartTime { get; set; }
        [Column("next_attempt_start_time")]
        public DateTime? NextAttemptStartTime { get; set; }
        [Column("last_message")]
        public string LastMessage { get; set; }
        [Column("target_type")]
        [StringLength(128)]
        public string TargetType { get; set; }
        [Column("target_id")]
        public Guid? TargetId { get; set; }
        [Column("target_subscription_id")]
        public Guid? TargetSubscriptionId { get; set; }
        [Column("target_resource_group_name")]
        [StringLength(128)]
        public string TargetResourceGroupName { get; set; }
        [Column("target_server_name")]
        [StringLength(256)]
        public string TargetServerName { get; set; }
        [Column("target_database_name")]
        [StringLength(128)]
        public string TargetDatabaseName { get; set; }
        [Column("target_elastic_pool_name")]
        [StringLength(128)]
        public string TargetElasticPoolName { get; set; }
    }
}
