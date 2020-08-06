using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBManager.Source.Entities
{
    public class LoungeView
    {
        public int Number { get; set; }
        public int Floor { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int BedCapacity { get; set; }

        public LoungeView() { }

        public LoungeView(int lvNumber, int lvFloor, string lvName, string lvType, int lvBedCapacity)
        {
            Number = lvNumber;
            Floor = lvFloor;
            Name = lvName;
            Type = lvType;
            BedCapacity = lvBedCapacity;
        }
    }
}