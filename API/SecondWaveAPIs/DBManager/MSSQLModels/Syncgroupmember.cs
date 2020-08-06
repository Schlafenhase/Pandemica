using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("syncgroupmember", Schema = "dss")]
    public partial class Syncgroupmember
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(140)]
        public string Name { get; set; }
        [Required]
        [Column("scopename")]
        [StringLength(100)]
        public string Scopename { get; set; }
        [Column("syncgroupid")]
        public Guid Syncgroupid { get; set; }
        [Column("syncdirection")]
        public int Syncdirection { get; set; }
        [Column("databaseid")]
        public Guid Databaseid { get; set; }
        [Column("memberstate")]
        public int Memberstate { get; set; }
        [Column("hubstate")]
        public int Hubstate { get; set; }
        [Column("memberstate_lastupdated", TypeName = "datetime")]
        public DateTime MemberstateLastupdated { get; set; }
        [Column("hubstate_lastupdated", TypeName = "datetime")]
        public DateTime HubstateLastupdated { get; set; }
        [Column("lastsynctime", TypeName = "datetime")]
        public DateTime? Lastsynctime { get; set; }
        [Column("lastsynctime_zerofailures_member", TypeName = "datetime")]
        public DateTime? LastsynctimeZerofailuresMember { get; set; }
        [Column("lastsynctime_zerofailures_hub", TypeName = "datetime")]
        public DateTime? LastsynctimeZerofailuresHub { get; set; }
        [Column("jobId")]
        public Guid? JobId { get; set; }
        [Column("hubJobId")]
        public Guid? HubJobId { get; set; }
        [Column("noinitsync")]
        public bool Noinitsync { get; set; }
        [Column("memberhasdata")]
        public bool? Memberhasdata { get; set; }

        [ForeignKey(nameof(Databaseid))]
        [InverseProperty(nameof(Userdatabase.Syncgroupmember))]
        public virtual Userdatabase Database { get; set; }
        [ForeignKey(nameof(Syncgroupid))]
        [InverseProperty("Syncgroupmember")]
        public virtual Syncgroup Syncgroup { get; set; }
    }
}
