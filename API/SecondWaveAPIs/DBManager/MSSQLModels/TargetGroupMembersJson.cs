using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    public partial class TargetGroupMembersJson
    {
        [Column("target_group_name")]
        [StringLength(128)]
        public string TargetGroupName { get; set; }
        [Required]
        [Column("target_group_members")]
        public string TargetGroupMembers { get; set; }
    }
}
