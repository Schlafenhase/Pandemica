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
    public class StateController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        /// <summary>
        /// Function in charge of recolecting all the states of the database
        /// </summary>
        /// <returns>
        /// List with all the states
        /// </returns>
        [Route("api/State")]
        [HttpGet]
        public IEnumerable<State> Get()
        {
            connection.openConnection();
            State[] allrecords;
            allrecords = select.makeStateSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of recolecting all the state name of the database
        /// </summary>
        /// <returns>
        /// List with all the state names
        /// </returns>
        [Route("api/State/Names")]
        [HttpGet]
        public IEnumerable<string> makeStateNamesSelect()
        {
            connection.openConnection();
            string[] allrecords;
            allrecords = select.makeStateNamesSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of searching a state through id
        /// </summary>
        /// <param name="id">
        /// Id of the state
        /// </param>
        /// <returns>
        /// List with the state found
        /// </returns>
        [Route("api/State/{id:int}")]
        [HttpGet]
        public IEnumerable<State> Get(int id)
        {
            connection.openConnection();
            State[] allrecords;
            allrecords = specificSelect.makeSpecificStateSelectById(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge in inserting a state
        /// </summary>
        /// <param name="state">
        /// State to be inserted
        /// </param>
        [Route("api/State")]
        [HttpPost]
        public void Post(State state)
        {
            connection.openConnection();
            insert.makeStateInsert(state.name);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge of updating a state
        /// </summary>
        /// <param name="id">
        /// Id of the state
        /// </param>
        /// <param name="state">
        /// State with the date
        /// </param>
        [Route("api/State/{id:int}")]
        [HttpPut]
        public void Put(int id, State state)
        {
            connection.openConnection();
            update.makeSpecificStateUpdate(id.ToString(), state.name);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        /// <summary>
        /// Function in charge of deleting a state
        /// </summary>
        /// <param name="id">
        /// Id of the state to delete
        /// </param>
        [Route("api/State/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            connection.openConnection();
            delete.makeSpecificStateDelete(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
