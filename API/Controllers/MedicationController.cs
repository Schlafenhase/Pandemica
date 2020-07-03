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

        /// <summary>
        /// Function in charge of recolecting all the medications in the database
        /// </summary>
        /// <returns>
        /// List with all the medications found
        /// </returns>
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

        /// <summary>
        /// Function in charge of recolecting all the medication names in the database
        /// </summary>
        /// <returns>
        /// List with all the medication names found
        /// </returns>
        [Route("api/Medication/Names")]
        [HttpGet]
        public IEnumerable<string> makeStateNamesSelect()
        {
            connection.openConnection();
            string[] allrecords;
            allrecords = select.makeMedicationNamesSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of searching a medication through an id
        /// </summary>
        /// <param name="id">
        /// Id of the medication
        /// </param>
        /// <returns>
        /// List with the found medication
        /// </returns>
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

        /// <summary>
        /// Function in charge of inserting a medication to the database
        /// </summary>
        /// <param name="medication">
        /// Medication to be added
        /// </param>
        [Route("api/Medication")]
        [HttpPost]
        public void Post(Medication medication)
        {
            connection.openConnection();
            insert.makeMedicationInsert(medication.id.ToString(), medication.name, medication.pharmacy);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Fcuntion in charge of updating a medication from the database
        /// </summary>
        /// <param name="id">
        /// Id of the medication
        /// </param>
        /// <param name="medication">
        /// Medication with the data to be updated
        /// </param>
        [Route("api/Medication/{id:int}")]
        [HttpPut]
        public void Put(int id, Medication medication)
        {
            connection.openConnection();
            update.makeSpecificMedicationUpdate(id.ToString(), medication.name, medication.pharmacy);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        /// <summary>
        /// Function in charge of deleting a medication from the database
        /// </summary>
        /// <param name="id">
        /// Id of the medication
        /// </param>
        [Route("api/Medication/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            connection.openConnection();
            delete.makeSpecificMedicationDelete(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
