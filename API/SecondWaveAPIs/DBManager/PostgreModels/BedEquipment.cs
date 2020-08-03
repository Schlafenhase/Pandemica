using System;
using System.Collections.Generic;

namespace DBManager.PostgreModels
{
    public partial class BedEquipment
    {
        public int BedNumber { get; set; }
        public int EquipmentId { get; set; }
        public int Id { get; set; }

        public virtual Bed BedNumberNavigation { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
