using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("action", Schema = "dss")]
    public partial class Action
    {
        public Action()
        {
            Task = new HashSet<Task>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("syncgroupid")]
        public Guid? Syncgroupid { get; set; }
        [Column("type")]
        public int Type { get; set; }
        [Column("state")]
        public int State { get; set; }
        [Column("creationtime", TypeName = "datetime")]
        public DateTime? Creationtime { get; set; }
        [Column("lastupdatetime", TypeName = "datetime")]
        public DateTime? Lastupdatetime { get; set; }

        [InverseProperty("Action")]
        public virtual ICollection<Task> Task { get; set; }
    }
}
