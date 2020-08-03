using System;
using System.Collections.Generic;

namespace DBManager.PostgreModels
{
    public partial class HospitalProcedure
    {
        public int ProcedureId { get; set; }
        public int HospitalId { get; set; }
        public int Id { get; set; }

        public virtual Hospital Hospital { get; set; }
        public virtual Procedure Procedure { get; set; }
    }
}
