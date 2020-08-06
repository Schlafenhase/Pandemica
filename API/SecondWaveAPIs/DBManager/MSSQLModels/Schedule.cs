using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("Schedule", Schema = "TaskHosting")]
    public partial class Schedule
    {
        public Schedule()
        {
            ScheduleTask1 = new HashSet<ScheduleTask1>();
        }

        [Key]
        public int ScheduleId { get; set; }
        public int FreqType { get; set; }
        public int FreqInterval { get; set; }

        [InverseProperty("ScheduleNavigation")]
        public virtual ICollection<ScheduleTask1> ScheduleTask1 { get; set; }
    }
}
