using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using API.Source.Server_Connections;
using CrystalDecisions.Shared.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace API.Controllers
{
    public class StoreProceduresController : ApiController
    {
        
        Tools dataInfo = new Tools();
        DatabaseDataHolder connection = new DatabaseDataHolder();
        
        /// <summary>
        /// Function in charge of getting all infected data of a country
        /// </summary>
        /// <param name="country">
        /// Name of the country
        /// </param>
        /// <returns>
        /// JObject with all the data
        /// </returns>
        [Route("api/StoreProcedure/Home/{country}")]
        [HttpGet]
        public JObject Get(string country)
        {
            connection.openConnection();
            var json = dataInfo.spCasesByCountry(country);
            connection.closeConnection();
            return json;
        }
    }
}