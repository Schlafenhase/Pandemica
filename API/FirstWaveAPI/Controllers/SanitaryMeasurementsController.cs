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
    public class SanitaryMeasurementsController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        /// <summary>
        /// Function in charge recolecting all the sanitary measures in the databases
        /// </summary>
        /// <returns>
        /// List with all the sanitary measures
        /// </returns>
        [Route("api/SanitaryMeasurements")]
        [HttpGet]
        public IEnumerable<SanitaryMeasurements> Get()
        {
            connection.openConnection();
            SanitaryMeasurements[] allrecords;
            allrecords = select.makeSanitaryMeasurementsSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of recolecting all the sanitary measures names in the database
        /// </summary>
        /// <returns>
        /// List with all the names of the sanitary measures
        /// </returns>
        [Route("api/SanitaryMeasurements/Names")]
        [HttpGet]
        public IEnumerable<string> GetSanitaryMeasurementNames()
        {
            connection.openConnection();
            string[] allrecords;
            allrecords = select.makeSanitaryMeasurementsNamesSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of searching a sanitary measure through id
        /// </summary>
        /// <param name="id">
        /// Id of the sanitary measure
        /// </param>
        /// <returns>
        /// List with the sanitary measure found
        /// </returns>
        [Route("api/SanitaryMeasurements/{id:int}")]
        [HttpGet]
        public IEnumerable<SanitaryMeasurements> Get(int id)
        {
            connection.openConnection();
            SanitaryMeasurements[] allrecords;
            allrecords = specificSelect.makeSpecificSanitaryMeasurementsSelectById(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of inserting a sanitary measure to the database
        /// </summary>
        /// <param name="sanitaryMeasurements">
        /// Sanitary measure to be added
        /// </param>
        [Route("api/SanitaryMeasurements")]
        [HttpPost]
        public void Post(SanitaryMeasurements sanitaryMeasurements)
        {
            connection.openConnection();
            insert.makeSanitaryMeasurementsInsert(sanitaryMeasurements.name, sanitaryMeasurements.description);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge of updating a sanitary measure in the database
        /// </summary>
        /// <param name="id">
        /// Id of the sanitary measure
        /// </param>
        /// <param name="sanitaryMeasurements">
        /// Sanitari measure with the data
        /// </param>
        [Route("api/SanitaryMeasurements/{id:int}")]
        [HttpPut]
        public void Put(int id, SanitaryMeasurements sanitaryMeasurements)
        {
            connection.openConnection();
            update.makeSpecificSanitaryMeasurementsUpdate(id.ToString(), sanitaryMeasurements.name, sanitaryMeasurements.description);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        /// <summary>
        /// Function in charge of deleting a sanitary measure in the database
        /// </summary>
        /// <param name="id">
        /// Id of the sanitary measure
        /// </param>
        [Route("api/SanitaryMeasurements/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            connection.openConnection();
            delete.makeSpecificSanitaryMeasurementsDelete(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
