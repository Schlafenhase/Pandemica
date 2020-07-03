using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Source.Server_Connections;
using CrystalDecisions.Shared.Json;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    public class StoreProceduresController : ApiController
    {
        
        Tools dataInfo = new Tools();
        DatabaseDataHolder connection = new DatabaseDataHolder();
        
        [Route("api/StoreProcedure/Home/{country}")]
        [HttpGet]
        public JObject Get(string country)
        {
            connection.openConnection();
            var allrecords = dataInfo.spCasesByCountry(country);
            connection.closeConnection();
            return allrecords;
        }
    }
}