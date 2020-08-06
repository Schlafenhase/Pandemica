using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("job_task_executions", Schema = "jobs_internal")]
    public partial class JobTaskExecutions
    {
        public JobTaskExecutions()
        {
            InversePreviousJobTaskExecution = new HashSet<JobTaskExecutions>();
            JobExecutions1 = new HashSet<JobExecutions1>();
        }

        [Key]
        [Column("job_task_execution_id")]
        public Guid JobTaskExecutionId { get; set; }
        [Column("job_execution_id")]
        public Guid JobExecutionId { get; set; }
        [Column("previous_job_task_execution_id")]
        public Guid? PreviousJobTaskExecutionId { get; set; }
        [Required]
        [Column("task_type")]
        [StringLength(50)]
        public string TaskType { get; set; }
        [Required]
        [Column("lifecycle")]
        [StringLength(50)]
        public string Lifecycle { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        [Column("start_time")]
        public DateTime? StartTime { get; set; }
        [Column("end_time")]
        public DateTime? EndTime { get; set; }
        [Column("message")]
        public string Message { get; set; }
        [Column("exception")]
        public string Exception { get; set; }
        [Required]
        [Column("row_version")]
        public byte[] RowVersion { get; set; }

        [ForeignKey(nameof(JobExecutionId))]
        [InverseProperty("JobTaskExecutions")]
        public virtual JobExecutions1 JobExecution { get; set; }
        [ForeignKey(nameof(PreviousJobTaskExecutionId))]
        [InverseProperty(nameof(JobTaskExecutions.InversePreviousJobTaskExecution))]
        public virtual JobTaskExecutions PreviousJobTaskExecution { get; set; }
        [InverseProperty(nameof(JobTaskExecutions.PreviousJobTaskExecution))]
        public virtual ICollection<JobTaskExecutions> InversePreviousJobTaskExecution { get; set; }
        [InverseProperty("LastJobTaskExecution")]
        public virtual ICollection<JobExecutions1> JobExecutions1 { get; set; }
    }
}
