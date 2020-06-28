using API.Source.Entities;
using API.Source.Server_Connections;
using API.Source.Server_Connections.Specific_Selects;
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
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/MedicationSupply")]
        [HttpGet]
        public IEnumerable<MedicationSupply> Get()
        {
            connection.openConnection();
            MedicationSupply[] allrecords;
            allrecords = select.makeMedicationSupplySelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/MedicationSupply/Medication/{id:int}")]
        [HttpGet]
        public IEnumerable<MedicationSupply> GetMedicationSupplyFromMedication(int id)
        {
            connection.openConnection();
            MedicationSupply[] allrecords;
            allrecords = specificSelect.makeSpecificMedicationSupplySelectByMedication(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/MedicationSupply/Hospital/{id:int}")]
        [HttpGet]
        public IEnumerable<MedicationSupply> GetContactFromHospital(int id)
        {
            connection.openConnection();
            MedicationSupply[] allrecords;
            allrecords = specificSelect.makeSpecificMedicationSupplySelectByHospital(id).ToArray();
            connection.closeConnection();
            return allrecords;
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
        public void PutMedicationSupplyFromMedication(int id, MedicationSupply medicationSupply)
        {
            Debug.WriteLine("Updated from medication");
        }

        [Route("api/MedicationSupply/Hospital/{id:int}")]
        [HttpPut]
        public void PutMedicationSupplyFromHospital(int id, MedicationSupply medicationSupply)
        {
            Debug.WriteLine("Updated from hospital");
        }

        [Route("api/MedicationSupply/Medication/{id:int}")]
        [HttpDelete]
        public void DeleteMedicationSupplyFromMedication(int id)
        {
            Debug.WriteLine("Deleted from medication");
        }

        [Route("api/MedicationSupply/Hospital/{id:int}")]
        [HttpDelete]
        public void DeleteMedicationSupplyFromHospital(int id)
        {
            Debug.WriteLine("Deleted from hospital");
        }
    }
}
