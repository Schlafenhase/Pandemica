using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBManager.Source.Entities
{
    public class BedView
    {
        public int Number { get; set; }
        public bool Icu { get; set; }
        public int LoungeNumber { get; set; }

        public BedView() { }

        public BedView(int bvNumber, bool bvIcu, int bvLoungeNumber)
        {
            Number = bvNumber;
            Icu = bvIcu;
            LoungeNumber = bvLoungeNumber;
        }
    }
}