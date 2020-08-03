using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    public partial class CurrentJobVersionNumbers
    {
        [Column("job_id")]
        public Guid JobId { get; set; }
        [Column("current_job_version_number")]
        public int? CurrentJobVersionNumber { get; set; }
    }
}
