using System;
using System.Collections.Generic;
using System.Collections;

namespace DBManager.PostgreModels
{
    public partial class Bed
    {
        public Bed()
        {
            BedEquipment = new HashSet<BedEquipment>();
        }

        public int Number { get; set; }
        public BitArray Icu { get; set; }
        public int LoungeNumber { get; set; }

        public virtual Lounge LoungeNumberNavigation { get; set; }
        public virtual ICollection<BedEquipment> BedEquipment { get; set; }
    }
}
