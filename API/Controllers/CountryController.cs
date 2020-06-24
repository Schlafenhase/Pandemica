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
    public class CountryController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Country")]
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            connection.openConnection();
            Country[] allrecords;
            allrecords = select.makeCountrySelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Country/{name}")]
        [HttpGet]
        public IEnumerable<Country> GetCountryFromName(string name)
        {
            connection.openConnection();
            Country[] allrecords;
            allrecords = specificSelect.makeSpecificCountrySelectByName(name).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Country/{email}")]
        [HttpGet]
        public IEnumerable<Country> GetCountryFromEMail(string email)
        {
            connection.openConnection();
            Country[] allrecords;
            allrecords = specificSelect.makeSpecificCountrySelectByEMail(email).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Country")]
        [HttpPost]
        public void Post(Country country)
        {
            connection.openConnection();
            insert.makeCountryInsert(country.name, country.continentName, country.eMail);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Country/{name}")]
        [HttpPut]
        public void Put(string name, Country country)
        {
            connection.openConnection();
            update.makeSpecificCountryUpdateByName(name, country.continentName, country.eMail);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        [Route("api/Country/{name}")]
        [HttpDelete]
        public void Delete(string name)
        {
            connection.openConnection();
            delete.makeSpecificCountryDeleteByName(name);
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
