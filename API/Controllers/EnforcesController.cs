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
    public class EnforcesController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        /// <summary>
        /// Function in charge of recopilating all the country measures in the database
        /// </summary>
        /// <returns>
        /// A list with all the country measures found
        /// </returns>
        [Route("api/Enforces/Id")]
        [HttpGet]
        public IEnumerable<EnforcesId> GetEnforcesWithMeasurementsId()
        {
            connection.openConnection();
            EnforcesId[] allrecords;
            allrecords = select.makeEnforcesIDSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of recopilating all the country measures with their names in the database
        /// </summary>
        /// <returns>
        /// A list with all the country measures found
        /// </returns>
        [Route("api/Enforces/Name")]
        [HttpGet]
        public IEnumerable<EnforcesName> GetEnforcesWithMeasurementsNames()
        {
            connection.openConnection();
            EnforcesName[] allrecords;
            allrecords = select.makeEnforcesNameSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of recopilating all the sanitary measures of a country
        /// </summary>
        /// <param name="name">
        /// Name of a country
        /// </param>
        /// <returns>
        /// List with all the country measures found
        /// </returns>
        [Route("api/Enforces/Country/{name}")]
        [HttpGet]
        public IEnumerable<EnforcesId> GetEnforcesFromCountry(string name)
        {
            connection.openConnection();
            EnforcesId[] allrecords;
            allrecords = specificSelect.makeSpecificEnforcesSelectByCountry(name).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of recopilating all the country measures
        /// </summary>
        /// <param name="id">
        /// Id of the sanitary measurement
        /// </param>
        /// <returns>
        /// List with all the country measures found
        /// </returns>
        [Route("api/Enforces/Measurement/{id:int}")]
        [HttpGet]
        public IEnumerable<EnforcesId> GetEnforcesFromMeasurement(int id)
        {
            connection.openConnection();
            EnforcesId[] allrecords;
            allrecords = specificSelect.makeSpecificEnforcesSelectByMeasurement(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of inserting a country measure through id
        /// </summary>
        /// <param name="enforces">
        /// Country measure to be added
        /// </param>
        [Route("api/Enforces/Id")]
        [HttpPost]
        public void PostWithId(EnforcesId enforces)
        {
            connection.openConnection();
            insert.makeEnforcesIdInsert(enforces.country, enforces.measurement.ToString(), enforces.startDate, enforces.finalDate);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge of inserting a country measure through name
        /// </summary>
        /// <param name="enforces">
        /// Country measure to be added
        /// </param>
        [Route("api/Enforces/Name")]
        [HttpPost]
        public void PostWithName(EnforcesName enforces)
        {
            connection.openConnection();
            insert.makeEnforcesNameInsert(enforces.country, enforces.measurementName, enforces.startDate, enforces.finalDate);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge of updating a country measure through id
        /// </summary>
        /// <param name="id">
        /// Id of the country measure
        /// </param>
        /// <param name="enforces">
        /// Country measure with the data to update
        /// </param>
        [Route("api/Enforces/Id/{id:int}")]
        [HttpPut]
        public void PutWithId(int id, EnforcesId enforces)
        {
            connection.openConnection();
            update.makeSpecificEnforcesIdUpdate(id.ToString(), enforces.country, enforces.measurement.ToString(), enforces.startDate, enforces.finalDate);
            connection.closeConnection();
            Debug.WriteLine("Updated from Country");
        }

        /// <summary>
        /// Function in charge of updating a country measure through name
        /// </summary>
        /// <param name="id">
        /// Id of the country measure
        /// </param>
        /// <param name="enforces">
        /// Country measure with the data to update
        /// </param>
        [Route("api/Enforces/Name/{id:int}")]
        [HttpPut]
        public void PutWithName(int id, EnforcesName enforces)
        {
            connection.openConnection();
            update.makeSpecificEnforcesNameUpdate(id.ToString(), enforces.country, enforces.measurementName, enforces.startDate, enforces.finalDate);
            connection.closeConnection();
            Debug.WriteLine("Updated from Country");
        }

        /// <summary>
        /// Function in charge of deleting a country measure from the database
        /// </summary>
        /// <param name="id">
        /// Id of the country measure to be deleted
        /// </param>
        [Route("api/Enforces/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            connection.openConnection();
            delete.makeSpecificEnforcesDelete(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted from Country");
        }
    }
}
