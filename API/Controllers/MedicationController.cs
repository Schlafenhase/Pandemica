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
    public class MedicationController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Medication")]
        [HttpGet]
        public IEnumerable<Medication> Get()
        {
            connection.openConnection();
            Medication[] allrecords;
            allrecords = select.makeMedicationSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Medication/{id:int}")]
        [HttpGet]
        public IEnumerable<Medication> Get(int id)
        {
            connection.openConnection();
            Medication[] allrecords;
            allrecords = specificSelect.makeSpecificMedicationSelectById(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Medication")]
        [HttpPost]
        public void Post(Medication medication)
        {
            connection.openConnection();
            insert.makeMedicationInsert(medication.id.ToString(), medication.name, medication.pharmacy);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Medication/{id:int}")]
        [HttpPut]
        public void Put(int id, Medication medication)
        {
            connection.openConnection();
            update.makeSpecificMedicationUpdateById(id.ToString(), medication.name, medication.pharmacy);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        [Route("api/Medication/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            connection.openConnection();
            delete.makeSpecificMedicationDeleteById(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
