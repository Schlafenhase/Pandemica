using System;
using System.Collections.Generic;

namespace DBManager.PostgreModels
{
    public partial class Procedure
    {
        public Procedure()
        {
            HospitalProcedure = new HashSet<HospitalProcedure>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }

        public virtual ICollection<HospitalProcedure> HospitalProcedure { get; set; }
    }
}
