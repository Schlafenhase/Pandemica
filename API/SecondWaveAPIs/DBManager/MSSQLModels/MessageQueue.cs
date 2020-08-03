using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("MessageQueue", Schema = "TaskHosting")]
    public partial class MessageQueue
    {
        [Key]
        public Guid MessageId { get; set; }
        public Guid? JobId { get; set; }
        public int MessageType { get; set; }
        public string MessageData { get; set; }
        [Column("InitialInsertTimeUTC", TypeName = "datetime")]
        public DateTime InitialInsertTimeUtc { get; set; }
        [Column("InsertTimeUTC", TypeName = "datetime")]
        public DateTime InsertTimeUtc { get; set; }
        [Column("UpdateTimeUTC", TypeName = "datetime")]
        public DateTime? UpdateTimeUtc { get; set; }
        public byte ExecTimes { get; set; }
        public int ResetTimes { get; set; }
        public long Version { get; set; }
        public Guid? TracingId { get; set; }
        public Guid? QueueId { get; set; }
        public Guid? WorkerId { get; set; }

        [ForeignKey(nameof(JobId))]
        [InverseProperty("MessageQueue")]
        public virtual Job Job { get; set; }
    }
}
