using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("script_batches", Schema = "jobs_internal")]
    public partial class ScriptBatches
    {
        [Key]
        [Column("command_data_id")]
        public Guid CommandDataId { get; set; }
        [Key]
        [Column("script_batch_number")]
        public int ScriptBatchNumber { get; set; }
        [Column("command_text")]
        public string CommandText { get; set; }

        [ForeignKey(nameof(CommandDataId))]
        [InverseProperty("ScriptBatches")]
        public virtual CommandData CommandData { get; set; }
    }
}
