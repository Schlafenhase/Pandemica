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
    public class ContinentController : ApiController
    {
        generalSelect select = new generalSelect();
        DatabaseDataHolder connection = new DatabaseDataHolder();
        [Route("api/Continent")]
        [HttpGet]
        public IEnumerable<Continent> Get()
        {
            Continent c1 = new Continent("Asia");
            Continent c2 = new Continent("America");
            connection.openConnection();
            select.makeSelect("Person,", "Patient", null, null, null);
            connection.closeConnection();
            return new Continent[] { c1, c2 };
        }

        [Route("api/Continent/{name}")]
        [HttpGet]
        public string Get(string name)
        {
            return name;
        }

        [Route("api/Continent")]
        [HttpPost]
        public void Post(Continent continent)
        {
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
