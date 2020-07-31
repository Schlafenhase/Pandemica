using API.Source.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace API.Source.Server_Connections
{
    public class GeneralUpdate
    {
        public void PutContactFromPatient(int id, Contact contact)
        {
            Debug.WriteLine("Updated from patient");
        }
    }
}