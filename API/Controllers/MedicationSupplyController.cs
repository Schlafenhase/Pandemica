using API.Source.Entities;
using API.Source.Server_Connections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class MedicationSupplyController : ApiController
    {
        General_Insert insert = new General_Insert();
        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/MedicationSupply")]
        [HttpGet]
        public IEnumerable<MedicationSupply> Get()
        {
            MedicationSupply ms1 = new MedicationSupply(1, 2, 50);
            MedicationSupply ms2 = new MedicationSupply(2, 3, 1000);
            return new MedicationSupply[] { ms1, ms2 };
        }

        [Route("api/MedicationSupply/Medication/{id:int}")]
        [HttpGet]
        public int GetMedicationSupplyFromMedication(int id)
        {
            return id;
        }

        [Route("api/MedicationSupply/Hospital/{id:int}")]
        [HttpGet]
        public int GetContactFromHospital(int id)
        {
            return id;
        }

        [Route("api/MedicationSupply")]
        [HttpPost]
        public void Post(MedicationSupply medicationSupply)
        {
            connection.openConnection();
            insert.makeMedicationSupplyInsert(medicationSupply.medication.ToString(), medicationSupply.hospital.ToString(), medicationSupply.quantity.ToString());
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/MedicationSupply/Medication/{id:int}")]
        [HttpPut]
        public void PutContactFromMedication(int id, MedicationSupply medicationSupply)
        {
            Debug.WriteLine("Updated from medication");
        }

        [Route("api/MedicationSupply/Hospital/{id:int}")]
        [HttpPut]
        public void PutContactFromHospital(int id, MedicationSupply medicationSupply)
        {
            Debug.WriteLine("Updated from hospital");
        }

        [Route("api/MedicationSupply/Medication/{id:int}")]
        [HttpDelete]
        public void DeleteContactFromMedication(int id)
        {
            Debug.WriteLine("Deleted from medication");
        }

        [Route("api/MedicationSupply/Hospital/{id:int}")]
        [HttpDelete]
        public void DeleteContactFromHospital(int id)
        {
            Debug.WriteLine("Deleted from hospital");
        }
    }
}
