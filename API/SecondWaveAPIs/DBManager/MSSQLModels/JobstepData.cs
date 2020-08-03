using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("jobstep_data", Schema = "jobs_internal")]
    public partial class JobstepData
    {
        public JobstepData()
        {
            Jobsteps1 = new HashSet<Jobsteps1>();
        }

        [Key]
        [Column("jobstep_data_id")]
        public Guid JobstepDataId { get; set; }
        [Required]
        [Column("command_type")]
        [StringLength(50)]
        public string CommandType { get; set; }
        [Column("result_set_destination_target_id")]
        public Guid? ResultSetDestinationTargetId { get; set; }
        [Column("result_set_destination_credential_name")]
        [StringLength(128)]
        public string ResultSetDestinationCredentialName { get; set; }
        [Column("result_set_destination_schema_name")]
        [StringLength(128)]
        public string ResultSetDestinationSchemaName { get; set; }
        [Column("result_set_destination_table_name")]
        [StringLength(128)]
        public string ResultSetDestinationTableName { get; set; }
        [Column("command_data_id")]
        public Guid? CommandDataId { get; set; }
        [Column("credential_name")]
        [StringLength(128)]
        public string CredentialName { get; set; }
        [Column("target_id")]
        public Guid? TargetId { get; set; }
        [Column("initial_retry_interval_ms")]
        public int InitialRetryIntervalMs { get; set; }
        [Column("maximum_retry_interval_ms")]
        public int MaximumRetryIntervalMs { get; set; }
        [Column("retry_interval_backoff_multiplier")]
        public float RetryIntervalBackoffMultiplier { get; set; }
        [Column("retry_attempts")]
        public int RetryAttempts { get; set; }
        [Column("step_timeout_ms")]
        public int StepTimeoutMs { get; set; }
        [Column("max_parallelism")]
        public int? MaxParallelism { get; set; }

        [ForeignKey(nameof(CommandDataId))]
        [InverseProperty("JobstepData")]
        public virtual CommandData CommandData { get; set; }
        [ForeignKey(nameof(ResultSetDestinationTargetId))]
        [InverseProperty(nameof(Targets.JobstepDataResultSetDestinationTarget))]
        public virtual Targets ResultSetDestinationTarget { get; set; }
        [ForeignKey(nameof(TargetId))]
        [InverseProperty(nameof(Targets.JobstepDataTarget))]
        public virtual Targets Target { get; set; }
        [InverseProperty("JobstepData")]
        public virtual ICollection<Jobsteps1> Jobsteps1 { get; set; }
    }
}
