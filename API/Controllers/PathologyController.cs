using API.Source.Entities;
using API.Source.Server_Connections;
using API.Source.Server_Connections.Specific_Selects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class PathologyController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        /// <summary>
        /// Function in charge of recolecting all the pathologies of the database
        /// </summary>
        /// <returns>
        /// List with all the pathologies found
        /// </returns>
        [Route("api/Pathology")]
        [HttpGet]
        public IEnumerable<Pathology> Get()
        {
            connection.openConnection();
            Pathology[] allrecords;
            allrecords = select.makePathologySelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of recolecting all the pathology names of of the database
        /// </summary>
        /// <returns>
        /// List with all the pathology names found
        /// </returns>
        [Route("api/Pathology/Names")]
        [HttpGet]
        public IEnumerable<string> GetPathologyNames()
        {
            connection.openConnection();
            string[] allrecords;
            allrecords = select.makePathologyNamesSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of searching a pathology through id
        /// </summary>
        /// <param name="id">
        /// Id of the pathology
        /// </param>
        /// <returns>
        /// List with the pathology found
        /// </returns>
        [Route("api/Pathology/{id:int}")]
        [HttpGet]
        public IEnumerable<Pathology> Get(int id)
        {
            connection.openConnection();
            Pathology[] allrecords;
            allrecords = specificSelect.makeSpecificPathologySelectById(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of inserting a pathology to the database
        /// </summary>
        /// <param name="pathology">
        /// Pathology to be added
        /// </param>
        [Route("api/Pathology")]
        [HttpPost]
        public void Post(Pathology pathology)
        {
            connection.openConnection();
            insert.makePathologyInsert(pathology.name, pathology.symptoms, pathology.description, pathology.treatment);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge of updating a patholofy in the database
        /// </summary>
        /// <param name="id">
        /// Id of the pathology
        /// </param>
        /// <param name="pathology">
        /// Pathology with the data to be updated
        /// </param>
        [Route("api/Pathology/{id:int}")]
        [HttpPut]
        public void Put(int id, Pathology pathology)
        {
            connection.openConnection();
            update.makeSpecificPathologyUpdate(id.ToString(), pathology.name, pathology.symptoms, pathology.description, pathology.treatment);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        /// <summary>
        /// Function in charge of deleting a pathology from the database
        /// </summary>
        /// <param name="id">
        /// Id of the pathology
        /// </param>
        [Route("api/Pathology/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            connection.openConnection();
            delete.makeSpecificPathologyDelete(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
