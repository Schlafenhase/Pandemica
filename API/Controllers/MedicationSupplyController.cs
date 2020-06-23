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
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

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
            allrecords = specificSelect.makeSpecificMedicationSupplySelectByMedication(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/MedicationSupply/Hospital/{id:int}")]
        [HttpGet]
        public IEnumerable<MedicationSupply> GetContactFromHospital(int id)
        {
            connection.openConnection();
            MedicationSupply[] allrecords;
            allrecords = specificSelect.makeSpecificMedicationSupplySelectByHospital(id.ToString()).ToArray();
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
            connection.openConnection();
            update.makeSpecificMedicationSupplyUpdateByMedication(id.ToString(), medicationSupply.quantity.ToString());
            connection.closeConnection();
            Debug.WriteLine("Updated from Medication");
        }

        [Route("api/MedicationSupply/Hospital/{id:int}")]
        [HttpPut]
        public void PutMedicationSupplyFromHospital(int id, MedicationSupply medicationSupply)
        {
            connection.openConnection();
            update.makeSpecificMedicationSupplyUpdateByHospital(id.ToString(), medicationSupply.quantity.ToString());
            connection.closeConnection();
            Debug.WriteLine("Updated from Hospital");
        }

        [Route("api/MedicationSupply/Medication/{id:int}")]
        [HttpDelete]
        public void DeleteMedicationSupplyFromMedication(int id)
        {
            connection.openConnection();
            delete.makeSpecificMedicationSupplyDeleteByMedication(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted from Medication");
        }

        [Route("api/MedicationSupply/Hospital/{id:int}")]
        [HttpDelete]
        public void DeleteMedicationSupplyFromHospital(int id)
        {
            connection.openConnection();
            delete.makeSpecificMedicationSupplyDeleteByHospital(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted from Hospital");
        }
    }
}
