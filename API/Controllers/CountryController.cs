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
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

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

        [Route("api/Country/Names")]
        [HttpGet]
        public IEnumerable<string> GetCountryNames()
        {
            connection.openConnection();
            string[] allrecords;
            allrecords = select.makeCountryNamesSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Country/Name/{name}")]
        [HttpGet]
        public IEnumerable<Country> GetCountryFromName(string name)
        {
            connection.openConnection();
            Country[] allrecords;
            allrecords = specificSelect.makeSpecificCountrySelectByName(name).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Country/Email")]
        [HttpPost]
        public IEnumerable<Country> GetCountryFromName(Country country)
        {
            connection.openConnection();
            Country[] allrecords;
            allrecords = specificSelect.makeSpecificCountrySelectByEMail(country.eMail).ToArray();
            connection.closeConnection();
            return allrecords;
        }
    }
}
