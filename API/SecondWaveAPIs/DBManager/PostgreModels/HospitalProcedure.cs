using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.PostgreModels
{
    [Table("hospital_procedure")]
    public partial class HospitalProcedure
    {
        [Key]
        [Column("procedure_id")]
        public int ProcedureId { get; set; }
        [Key]
        [Column("hospital_id")]
        public int HospitalId { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey(nameof(HospitalId))]
        [InverseProperty("HospitalProcedure")]
        public virtual Hospital Hospital { get; set; }
        [ForeignKey(nameof(ProcedureId))]
        [InverseProperty("HospitalProcedure")]
        public virtual Procedure Procedure { get; set; }
    }
}
