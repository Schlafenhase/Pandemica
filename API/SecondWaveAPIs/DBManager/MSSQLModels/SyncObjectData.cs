using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("SyncObjectData", Schema = "dss")]
    public partial class SyncObjectData
    {
        [Key]
        public Guid ObjectId { get; set; }
        [Key]
        public int DataType { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? DroppedTime { get; set; }
        [Required]
        public byte[] LastModified { get; set; }
        [Required]
        public byte[] ObjectData { get; set; }
    }
}
