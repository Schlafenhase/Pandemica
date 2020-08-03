using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("job_cancellations", Schema = "jobs_internal")]
    public partial class JobCancellations
    {
        [Key]
        [Column("job_execution_id")]
        public Guid JobExecutionId { get; set; }
        [Column("requested_time")]
        public DateTime RequestedTime { get; set; }

        [ForeignKey(nameof(JobExecutionId))]
        [InverseProperty(nameof(JobExecutions1.JobCancellations))]
        public virtual JobExecutions1 JobExecution { get; set; }
    }
}
