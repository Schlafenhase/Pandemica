using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    [Table("TEMPORARY_DATA")]
    public partial class TemporaryData
    {
        [Column("requested_data")]
        [StringLength(20)]
        public string RequestedData { get; set; }
        [Column("resulted_data")]
        public int? ResultedData { get; set; }
    }
}
