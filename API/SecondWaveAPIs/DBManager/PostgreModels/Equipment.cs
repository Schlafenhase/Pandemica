using System;
using System.Collections.Generic;

namespace DBManager.PostgreModels
{
    public partial class Equipment
    {
        public Equipment()
        {
            BedEquipment = new HashSet<BedEquipment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Provider { get; set; }

        public virtual ICollection<BedEquipment> BedEquipment { get; set; }
    }
}
