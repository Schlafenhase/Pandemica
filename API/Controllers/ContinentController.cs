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
    public class ContinentController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Continent")]
        [HttpGet]
        public IEnumerable<Continent> Get()
        {
            connection.openConnection();
            Continent[] allrecords;
            allrecords = select.makeContinentSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Continent/{name}")]
        [HttpGet]
        public IEnumerable<Continent> Get(string name)
        {
            connection.openConnection();
            Continent[] allrecords;
            allrecords = specificSelect.makeSpecificContinentSelectByName(name).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Continent")]
        [HttpPost]
        public void Post(Continent continent)
        {
            connection.openConnection();
            insert.makeContinentInsert(continent.name);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Continent/{name}")]
        [HttpPut]
        public void Put(string name, Continent continent)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/Continent/{name}")]
        [HttpDelete]
        public void Delete(string name)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
