using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("STATE")]
    public partial class State
    {
        public State()
        {
            PatientState = new HashSet<PatientState>();
        }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        [Key]
        public int Id { get; set; }

        [InverseProperty("StateNavigation")]
        public virtual ICollection<PatientState> PatientState { get; set; }
    }
}
