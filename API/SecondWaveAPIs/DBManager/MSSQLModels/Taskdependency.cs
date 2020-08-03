using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("taskdependency", Schema = "dss")]
    public partial class Taskdependency
    {
        [Key]
        [Column("nexttaskid")]
        public Guid Nexttaskid { get; set; }
        [Key]
        [Column("prevtaskid")]
        public Guid Prevtaskid { get; set; }

        [ForeignKey(nameof(Nexttaskid))]
        [InverseProperty(nameof(Task.TaskdependencyNexttask))]
        public virtual Task Nexttask { get; set; }
        [ForeignKey(nameof(Prevtaskid))]
        [InverseProperty(nameof(Task.TaskdependencyPrevtask))]
        public virtual Task Prevtask { get; set; }
    }
}
