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
    public class PathologyController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Pathology")]
        [HttpGet]
        public IEnumerable<Pathology> Get()
        {
            connection.openConnection();
            Pathology[] allrecords;
            allrecords = select.makePathologySelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Pathology/{name}")]
        [HttpGet]
        public IEnumerable<Pathology> Get(string name)
        {
            connection.openConnection();
            Pathology[] allrecords;
            allrecords = specificSelect.makeSpecificPathologySelectByName(name).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Pathology")]
        [HttpPost]
        public void Post(Pathology pathology)
        {
            connection.openConnection();
            insert.makePathologyInsert(pathology.name, pathology.symptoms, pathology.description, pathology.treatment);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Pathology/{name}")]
        [HttpPut]
        public void Put(string name, Pathology pathology)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/Pathology/{name}")]
        [HttpDelete]
        public void Delete(string name)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
