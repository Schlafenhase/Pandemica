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

        DatabaseDataHolder connection = new DatabaseDataHolder();

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

        [Route("api/State/{id:int}")]
        [HttpGet]
        public IEnumerable<State> Get(int id)
        {
            connection.openConnection();
            State[] allrecords;
            allrecords = specificSelect.makeSpecificStateSelectByName(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/State")]
        [HttpPost]
        public void Post(State state)
        {
            connection.openConnection();
            insert.makeStateInsert(state.name);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/State/{id:int}")]
        [HttpPut]
        public void Put(int id, State state)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/State/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
