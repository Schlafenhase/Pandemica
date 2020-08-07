using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProcedures.PostgreModels
{
    [Table("history_store")]
    public partial class HistoryStore
    {
        [Column("timemark")]
        public DateTime Timemark { get; set; }
        [Key]
        [Column("table_name")]
        [StringLength(50)]
        public string TableName { get; set; }
        [Required]
        [Column("pk_date_src")]
        [StringLength(400)]
        public string PkDateSrc { get; set; }
        [Key]
        [Column("pk_date_dest")]
        [StringLength(400)]
        public string PkDateDest { get; set; }
        [Column("record_state")]
        public short RecordState { get; set; }
    }
}
