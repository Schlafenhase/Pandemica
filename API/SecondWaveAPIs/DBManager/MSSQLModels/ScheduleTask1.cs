using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("ScheduleTask", Schema = "TaskHosting")]
    public partial class ScheduleTask1
    {
        [Key]
        public Guid ScheduleTaskId { get; set; }
        public int TaskType { get; set; }
        [Required]
        [StringLength(128)]
        public string TaskName { get; set; }
        public int? Schedule { get; set; }
        public int State { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime NextRunTime { get; set; }
        public Guid? MessageId { get; set; }
        public string TaskInput { get; set; }
        public Guid QueueId { get; set; }
        public Guid TracingId { get; set; }
        public Guid JobId { get; set; }

        [ForeignKey(nameof(Schedule))]
        [InverseProperty("ScheduleTask1")]
        public virtual Schedule ScheduleNavigation { get; set; }
    }
}
