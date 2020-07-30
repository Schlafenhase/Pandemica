using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Source.Entities
{
    public class Hospital
    {
        public int id { get; set; }
        public string name { get; set; }
        public int phone { get; set; }
        public string managerName { get; set; }
        public int capacity { get; set; }
        public int icuCapacity { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string eMail { get; set; }

        public Hospital(){}

        public Hospital(int hId, string hName, int hPhone, string hManagerName, int hCapacity, int hIcuCapacity, string hCountry, string hRegion, string hEMail)
        {
            id = hId;
            name = hName;
            phone = hPhone;
            managerName = hManagerName;
            capacity = hCapacity;
            icuCapacity = hIcuCapacity;
            country = hCountry;
            region = hRegion;
            eMail = hEMail;
        }
    }
}