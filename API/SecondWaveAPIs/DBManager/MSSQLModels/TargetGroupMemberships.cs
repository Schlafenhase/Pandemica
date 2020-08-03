using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("target_group_memberships", Schema = "jobs_internal")]
    public partial class TargetGroupMemberships
    {
        [Key]
        [Column("parent_target_id")]
        public Guid ParentTargetId { get; set; }
        [Key]
        [Column("child_target_id")]
        public Guid ChildTargetId { get; set; }
        [Column("include")]
        public bool Include { get; set; }

        [ForeignKey(nameof(ChildTargetId))]
        [InverseProperty(nameof(Targets.TargetGroupMembershipsChildTarget))]
        public virtual Targets ChildTarget { get; set; }
        [ForeignKey(nameof(ParentTargetId))]
        [InverseProperty(nameof(Targets.TargetGroupMembershipsParentTarget))]
        public virtual Targets ParentTarget { get; set; }
    }
}
