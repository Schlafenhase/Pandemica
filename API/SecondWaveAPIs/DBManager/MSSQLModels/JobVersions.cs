using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    public partial class JobVersions
    {
        [Required]
        [Column("job_name")]
        [StringLength(128)]
        public string JobName { get; set; }
        [Column("job_id")]
        public Guid JobId { get; set; }
        [Column("job_version")]
        public int JobVersion { get; set; }
    }
}
