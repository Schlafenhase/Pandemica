using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProcedures.PostgreModels
{
    [Table("reservation_procedures")]
    public partial class ReservationProcedures
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Key]
        [Column("procedure_id")]
        public int ProcedureId { get; set; }
        [Key]
        [Column("reservation_id")]
        public int ReservationId { get; set; }

        [ForeignKey(nameof(ProcedureId))]
        [InverseProperty("ReservationProcedures")]
        public virtual Procedure Procedure { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
