using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBManager.Source.Entities
{
    public class EquipmentView
    {
        public string Name { get; set; }
        public string Provider { get; set; }
        public int Quantity { get; set; }

        public EquipmentView() { }

        public EquipmentView(string eName, string eProvider, int eQuantity)
        {
            Name = eName;
            Provider = eProvider;
            Quantity = eQuantity;
        }
    }
}