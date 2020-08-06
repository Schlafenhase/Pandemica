using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    public partial class DatabaseCredentials
    {
        [Required]
        [Column("name")]
        [StringLength(128)]
        public string Name { get; set; }
        [Column("principal_id")]
        public int PrincipalId { get; set; }
        [Column("credential_id")]
        public int CredentialId { get; set; }
        [Column("credential_identity")]
        [StringLength(4000)]
        public string CredentialIdentity { get; set; }
        [Column("create_date", TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        [Column("modify_date", TypeName = "datetime")]
        public DateTime ModifyDate { get; set; }
        [Column("target_type")]
        [StringLength(60)]
        public string TargetType { get; set; }
        [Column("target_id")]
        public int? TargetId { get; set; }
    }
}
