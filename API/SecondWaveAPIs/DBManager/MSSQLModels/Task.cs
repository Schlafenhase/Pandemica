using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("task", Schema = "dss")]
    public partial class Task
    {
        public Task()
        {
            TaskdependencyNexttask = new HashSet<Taskdependency>();
            TaskdependencyPrevtask = new HashSet<Taskdependency>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("actionid")]
        public Guid Actionid { get; set; }
        [Column("taskNumber")]
        public long TaskNumber { get; set; }
        [Column("lastheartbeat", TypeName = "datetime")]
        public DateTime? Lastheartbeat { get; set; }
        [Column("state")]
        public int? State { get; set; }
        [Column("type")]
        public int? Type { get; set; }
        [Column("agentid")]
        public Guid? Agentid { get; set; }
        [Column("owning_instanceid")]
        public Guid? OwningInstanceid { get; set; }
        [Column("creationtime", TypeName = "datetime")]
        public DateTime? Creationtime { get; set; }
        [Column("pickuptime", TypeName = "datetime")]
        public DateTime? Pickuptime { get; set; }
        [Column("completedtime", TypeName = "datetime")]
        public DateTime? Completedtime { get; set; }
        [Column("request")]
        public byte[] Request { get; set; }
        [Column("response")]
        public byte[] Response { get; set; }
        [Column("priority")]
        public int? Priority { get; set; }
        [Column("retry_count")]
        public int RetryCount { get; set; }
        [Column("dependency_count")]
        public int DependencyCount { get; set; }
        [Column("version")]
        public long Version { get; set; }
        [Column("lastresettime", TypeName = "datetime")]
        public DateTime? Lastresettime { get; set; }

        [ForeignKey(nameof(Actionid))]
        [InverseProperty("Task")]
        public virtual Action Action { get; set; }
        [InverseProperty(nameof(Taskdependency.Nexttask))]
        public virtual ICollection<Taskdependency> TaskdependencyNexttask { get; set; }
        [InverseProperty(nameof(Taskdependency.Prevtask))]
        public virtual ICollection<Taskdependency> TaskdependencyPrevtask { get; set; }
    }
}
