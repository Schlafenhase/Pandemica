using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager.MSSQLModels
{
    public partial class PatientStateByCountry
    {
        [StringLength(30)]
        public string Country { get; set; }
        public int? Confirmed { get; set; }
        public int? Active { get; set; }
        public int? Dead { get; set; }
        public int? Recovered { get; set; }
    }
}
