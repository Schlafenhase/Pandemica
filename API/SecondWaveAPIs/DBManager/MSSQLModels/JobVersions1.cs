using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("job_versions", Schema = "jobs_internal")]
    public partial class JobVersions1
    {
        public JobVersions1()
        {
            JobExecutions1 = new HashSet<JobExecutions1>();
            Jobsteps1 = new HashSet<Jobsteps1>();
        }

        [Key]
        [Column("job_id")]
        public Guid JobId { get; set; }
        [Key]
        [Column("job_version_number")]
        public int JobVersionNumber { get; set; }

        [ForeignKey(nameof(JobId))]
        [InverseProperty(nameof(Jobs1.JobVersions1))]
        public virtual Jobs1 Job { get; set; }
        [InverseProperty("Job")]
        public virtual ICollection<JobExecutions1> JobExecutions1 { get; set; }
        [InverseProperty("JobNavigation")]
        public virtual ICollection<Jobsteps1> Jobsteps1 { get; set; }
    }
}
