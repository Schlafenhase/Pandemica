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
    public class CountryController : ApiController
    {
        General_Insert insert = new General_Insert();
        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Country")]
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            Country c1 = new Country("Costa Rica", "America");
            Country c2 = new Country("Nicaragua", "America");
            return new Country[] { c1, c2 };
        }

        [Route("api/Country/{name}")]
        [HttpGet]
        public string Get(string name)
        {
            return name;
        }

        [Route("api/Country")]
        [HttpPost]
        public void Post(Country country)
        {
            connection.openConnection();
            insert.makeCountryInsert(country.name, country.continentName);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Country/{name}")]
        [HttpPut]
        public void Put(string name, Country country)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/Country/{name}")]
        [HttpDelete]
        public void Delete(string name)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
