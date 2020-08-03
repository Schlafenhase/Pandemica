using System;
using System.Collections.Generic;

namespace DBManager.PostgreModels
{
    public partial class Lounge
    {
        public Lounge()
        {
            Bed = new HashSet<Bed>();
        }

        public int Number { get; set; }
        public int Floor { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; }
        public virtual ICollection<Bed> Bed { get; set; }
    }
}
