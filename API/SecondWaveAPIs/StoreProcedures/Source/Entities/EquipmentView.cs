using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreProcedures.Source.Entities
{
    public class EquipmentView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Provider { get; set; }
        public int Quantity { get; set; }

        public EquipmentView() { }

        public EquipmentView(int eId, string eName, string eProvider, int eQuantity)
        {
            Id = eId;
            Name = eName;
            Provider = eProvider;
            Quantity = eQuantity;
        }
    }
}