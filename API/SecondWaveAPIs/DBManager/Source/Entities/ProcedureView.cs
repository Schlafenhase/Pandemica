using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBManager.Source.Entities
{
    public class ProcedureView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }

        public ProcedureView() { }

        public ProcedureView(int pvId, string pvName, int pvDuration)
        {
            Id = pvId;
            Name = pvName;
            Duration = pvDuration;
        }
    }
}