using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("job_executions", Schema = "jobs_internal")]
    public partial class JobExecutions1
    {
        public JobExecutions1()
        {
            InverseParentJobExecution = new HashSet<JobExecutions1>();
            InverseRootJobExecution = new HashSet<JobExecutions1>();
            Jobs1 = new HashSet<Jobs1>();
        }

        [Key]
        [Column("job_execution_id")]
        public Guid JobExecutionId { get; set; }
        [Column("job_id")]
        public Guid JobId { get; set; }
        [Column("job_version_number")]
        public int JobVersionNumber { get; set; }
        [Column("step_id")]
        public int? StepId { get; set; }
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        [Column("start_time")]
        public DateTime? StartTime { get; set; }
        [Column("end_time")]
        public DateTime? EndTime { get; set; }
        [Required]
        [Column("row_version")]
        public byte[] RowVersion { get; set; }
        [Column("infrastructure_failures")]
        public int InfrastructureFailures { get; set; }
        [Column("current_task_attempts")]
        public int CurrentTaskAttempts { get; set; }
        [Column("next_retry_delay_ms")]
        public int NextRetryDelayMs { get; set; }
        [Column("do_not_retry_until_time")]
        public DateTime? DoNotRetryUntilTime { get; set; }
        [Required]
        [Column("lifecycle")]
        [StringLength(50)]
        public string Lifecycle { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Column("target_id")]
        public Guid? TargetId { get; set; }
        [Column("parent_job_execution_id")]
        public Guid? ParentJobExecutionId { get; set; }
        [Column("root_job_execution_id")]
        public Guid RootJobExecutionId { get; set; }
        [Column("initiated_for_schedule_time")]
        public DateTime? InitiatedForScheduleTime { get; set; }
        [Column("last_job_task_execution_id")]
        public Guid? LastJobTaskExecutionId { get; set; }

        [ForeignKey("JobId,JobVersionNumber")]
        [InverseProperty(nameof(JobVersions1.JobExecutions1))]
        public virtual JobVersions1 Job { get; set; }
        [ForeignKey("JobId,JobVersionNumber,StepId")]
        [InverseProperty("JobExecutions1")]
        public virtual Jobsteps1 Jobsteps1 { get; set; }
        [ForeignKey(nameof(LastJobTaskExecutionId))]
        [InverseProperty("JobExecutions1")]
        public virtual JobTaskExecutions LastJobTaskExecution { get; set; }
        [ForeignKey(nameof(ParentJobExecutionId))]
        [InverseProperty(nameof(JobExecutions1.InverseParentJobExecution))]
        public virtual JobExecutions1 ParentJobExecution { get; set; }
        [ForeignKey(nameof(RootJobExecutionId))]
        [InverseProperty(nameof(JobExecutions1.InverseRootJobExecution))]
        public virtual JobExecutions1 RootJobExecution { get; set; }
        [ForeignKey(nameof(TargetId))]
        [InverseProperty(nameof(Targets.JobExecutions1))]
        public virtual Targets Target { get; set; }
        [InverseProperty("JobExecution")]
        public virtual JobCancellations JobCancellations { get; set; }
        [InverseProperty("JobExecution")]
        public virtual JobTaskExecutions JobTaskExecutions { get; set; }
        [InverseProperty(nameof(JobExecutions1.ParentJobExecution))]
        public virtual ICollection<JobExecutions1> InverseParentJobExecution { get; set; }
        [InverseProperty(nameof(JobExecutions1.RootJobExecution))]
        public virtual ICollection<JobExecutions1> InverseRootJobExecution { get; set; }
        [InverseProperty("LastJobExecution")]
        public virtual ICollection<Jobs1> Jobs1 { get; set; }
    }
}
