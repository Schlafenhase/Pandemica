using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("UIHistory", Schema = "dss")]
    public partial class Uihistory
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("completionTime")]
        public DateTime CompletionTime { get; set; }
        [Column("taskType")]
        public int TaskType { get; set; }
        [Column("recordType")]
        public int RecordType { get; set; }
        [Column("serverid")]
        public Guid Serverid { get; set; }
        [Column("agentid")]
        public Guid Agentid { get; set; }
        [Column("databaseid")]
        public Guid Databaseid { get; set; }
        [Column("syncgroupId")]
        public Guid SyncgroupId { get; set; }
        [Required]
        [Column("detailEnumId")]
        [StringLength(400)]
        public string DetailEnumId { get; set; }
        [Column("detailStringParameters")]
        public string DetailStringParameters { get; set; }
        [Column("isWritable")]
        public bool? IsWritable { get; set; }
    }
}
