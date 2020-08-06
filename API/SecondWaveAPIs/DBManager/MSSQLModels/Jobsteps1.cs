using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("jobsteps", Schema = "jobs_internal")]
    public partial class Jobsteps1
    {
        public Jobsteps1()
        {
            JobExecutions1 = new HashSet<JobExecutions1>();
        }

        [Key]
        [Column("job_id")]
        public Guid JobId { get; set; }
        [Key]
        [Column("job_version_number")]
        public int JobVersionNumber { get; set; }
        [Key]
        [Column("step_id")]
        public int StepId { get; set; }
        [Column("jobstep_data_id")]
        public Guid JobstepDataId { get; set; }
        [Required]
        [Column("step_name")]
        [StringLength(128)]
        public string StepName { get; set; }

        [ForeignKey(nameof(JobId))]
        [InverseProperty(nameof(Jobs1.Jobsteps1))]
        public virtual Jobs1 Job { get; set; }
        [ForeignKey("JobId,JobVersionNumber")]
        [InverseProperty(nameof(JobVersions1.Jobsteps1))]
        public virtual JobVersions1 JobNavigation { get; set; }
        [ForeignKey(nameof(JobstepDataId))]
        [InverseProperty("Jobsteps1")]
        public virtual JobstepData JobstepData { get; set; }
        [InverseProperty("Jobsteps1")]
        public virtual ICollection<JobExecutions1> JobExecutions1 { get; set; }
    }
}
