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
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Country")]
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            connection.openConnection();
            Country[] allrecords;
            allrecords = select.makeCountrySelect("Name", "ContinentName").ToArray();
            connection.closeConnection();
            return allrecords;
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
