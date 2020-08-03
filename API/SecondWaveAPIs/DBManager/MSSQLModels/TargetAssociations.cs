using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("target_associations", Schema = "jobs_internal")]
    public partial class TargetAssociations
    {
        [Key]
        [Column("parent_target_id")]
        public Guid ParentTargetId { get; set; }
        [Key]
        [Column("child_target_id")]
        public Guid ChildTargetId { get; set; }

        [ForeignKey(nameof(ChildTargetId))]
        [InverseProperty(nameof(Targets.TargetAssociationsChildTarget))]
        public virtual Targets ChildTarget { get; set; }
        [ForeignKey(nameof(ParentTargetId))]
        [InverseProperty(nameof(Targets.TargetAssociationsParentTarget))]
        public virtual Targets ParentTarget { get; set; }
    }
}
