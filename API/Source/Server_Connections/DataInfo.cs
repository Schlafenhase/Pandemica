using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Server_Connections
{
    public class DataInfo
    {
        public string requestedData { get; set; }
        public int resultData { get; set; }

        public DataInfo() { }

        public DataInfo(string requestedDataEntry, int resultDataEntry)
        {
            requestedData = requestedDataEntry;
            resultData = resultDataEntry;
        }
    }
}