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
    public class StateController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();

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

        [Route("api/State/{name}")]
        [HttpGet]
        public string Get(string name)
        {
            return name;
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

        [Route("api/State/{name}")]
        [HttpPut]
        public void Put(string name, State state)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/State/{name}")]
        [HttpDelete]
        public void Delete(string name)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
