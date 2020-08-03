using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("Job", Schema = "TaskHosting")]
    public partial class Job
    {
        public Job()
        {
            MessageQueue = new HashSet<MessageQueue>();
        }

        [Key]
        public Guid JobId { get; set; }
        public bool IsCancelled { get; set; }
        [Column("InitialInsertTimeUTC", TypeName = "datetime")]
        public DateTime InitialInsertTimeUtc { get; set; }
        public int JobType { get; set; }
        public string InputData { get; set; }
        public int TaskCount { get; set; }
        public int CompletedTaskCount { get; set; }
        public Guid? TracingId { get; set; }

        [InverseProperty("Job")]
        public virtual ICollection<MessageQueue> MessageQueue { get; set; }
    }
}
