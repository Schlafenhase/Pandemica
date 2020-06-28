using API.Source.Entities;
using API.Source.Server_Connections;
using API.Source.Server_Connections.Specific_Selects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

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

        [Route("api/Pathology/{id:int}")]
        [HttpGet]
        public IEnumerable<Pathology> Get(int id)
        {
            connection.openConnection();
            Pathology[] allrecords;
            allrecords = specificSelect.makeSpecificPathologySelectById(id.ToString()).ToArray();
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

        [Route("api/Pathology/{id:int}")]
        [HttpPut]
        public void Put(int id, Pathology pathology)
        {
            connection.openConnection();
            update.makeSpecificPathologyUpdateById(id.ToString(), pathology.name, pathology.symptoms, pathology.description, pathology.treatment);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        [Route("api/Pathology/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            connection.openConnection();
            delete.makeSpecificPathologyDeleteById(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
