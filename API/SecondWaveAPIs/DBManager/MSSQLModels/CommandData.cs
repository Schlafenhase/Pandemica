using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("command_data", Schema = "jobs_internal")]
    public partial class CommandData
    {
        public CommandData()
        {
            JobstepData = new HashSet<JobstepData>();
            ScriptBatches = new HashSet<ScriptBatches>();
        }

        [Key]
        [Column("command_data_id")]
        public Guid CommandDataId { get; set; }
        [Column("script_has_been_split")]
        public bool ScriptHasBeenSplit { get; set; }
        [Required]
        [Column("text")]
        public string Text { get; set; }
        [Column("text_checksum")]
        public int? TextChecksum { get; set; }

        [InverseProperty("CommandData")]
        public virtual ICollection<JobstepData> JobstepData { get; set; }
        [InverseProperty("CommandData")]
        public virtual ICollection<ScriptBatches> ScriptBatches { get; set; }
    }
}
