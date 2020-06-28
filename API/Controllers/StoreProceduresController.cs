using System;
using System.Collections.Generic;
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
        
        Prueba dataInfo = new Prueba();
        DatabaseDataHolder connection = new DatabaseDataHolder();
        
        [Route("api/StoreProcedure/Home/{country}")]
        [HttpGet]
        public JObject Get(string country)
        {
            connection.openConnection();
            var allrecords = dataInfo.spPrueba(country);
            connection.closeConnection();
            return allrecords;
        }
    }
}