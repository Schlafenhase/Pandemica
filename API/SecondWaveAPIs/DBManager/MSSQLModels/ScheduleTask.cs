using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("ScheduleTask", Schema = "dss")]
    public partial class ScheduleTask
    {
        public Guid Id { get; set; }
        [Key]
        public Guid SyncGroupId { get; set; }
        public long Interval { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }
        public byte State { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpirationTime { get; set; }
        public Guid? PopReceipt { get; set; }
        public byte DequeueCount { get; set; }
        public int Type { get; set; }

        [ForeignKey(nameof(SyncGroupId))]
        [InverseProperty(nameof(Syncgroup.ScheduleTask))]
        public virtual Syncgroup SyncGroup { get; set; }
    }
}
