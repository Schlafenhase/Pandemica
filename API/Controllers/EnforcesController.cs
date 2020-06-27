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
    public class EnforcesController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Enforces")]
        [HttpGet]
        public IEnumerable<Enforces> Get()
        {
            connection.openConnection();
            Enforces[] allrecords;
            allrecords = select.makeEnforcesSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Enforces/Country/{name}")]
        [HttpGet]
        public IEnumerable<Enforces> GetEnforcesFromCountry(string name)
        {
            connection.openConnection();
            Enforces[] allrecords;
            allrecords = specificSelect.makeSpecificEnforcesSelectByCountry(name).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Enforces/Measurement/{id:int}")]
        [HttpGet]
        public IEnumerable<Enforces> GetEnforcesFromMeasurement(int id)
        {
            connection.openConnection();
            Enforces[] allrecords;
            allrecords = specificSelect.makeSpecificEnforcesSelectByMeasurement(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Enforces")]
        [HttpPost]
        public void Post(Enforces enforces)
        {
            connection.openConnection();
            insert.makeEnforcesInsert(enforces.country, enforces.measurement.ToString(), enforces.startDate, enforces.finalDate);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Enforces/Country/{name}")]
        [HttpPut]
        public void PutEnforcesFromCountry(string name, Enforces enforces)
        {
            connection.openConnection();
            update.makeSpecificEnforcesUpdateByCountry(name, enforces.startDate, enforces.finalDate);
            connection.closeConnection();
            Debug.WriteLine("Updated from Country");
        }

        [Route("api/Enforces/Measurement/{id:int}")]
        [HttpPut]
        public void PutEnforcesFromMeasurement(int id, Enforces enforces)
        {
            connection.openConnection();
            update.makeSpecificEnforcesUpdateByMeasurement(id.ToString(), enforces.startDate, enforces.finalDate);
            connection.closeConnection();
            Debug.WriteLine("Updated from Measurement");
        }

        [Route("api/Enforces/Country/{name}")]
        [HttpDelete]
        public void DeleteEnforcesFromCountry(string name)
        {
            connection.openConnection();
            delete.makeSpecificEnforcesDeleteByCountry(name);
            connection.closeConnection();
            Debug.WriteLine("Deleted from Country");
        }

        [Route("api/Enforces/Measurement/{id:int}")]
        [HttpDelete]
        public void DeleteEnforcesFromMeasurement(int id)
        {
            connection.openConnection();
            delete.makeSpecificEnforcesDeleteByMeasurement(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted from Measurement");
        }
    }
}
