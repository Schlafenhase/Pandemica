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
    public class EnforcesController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();

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
        public string GetEnforcesFromCountry(string name)
        {
            return name;
        }

        [Route("api/Enforces/Measurement/{id:int}")]
        [HttpGet]
        public int GetEnforcesFromMeasurement(int id)
        {
            return id;
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
        public void PutContactFromPerson(string name, Enforces enforces)
        {
            Debug.WriteLine("Updated from country");
        }

        [Route("api/Enforces/Measurement/{id:int}")]
        [HttpPut]
        public void PutContactFromPatient(int id, Enforces enforces)
        {
            Debug.WriteLine("Updated from measurement");
        }

        [Route("api/Enforces/Country/{name}")]
        [HttpDelete]
        public void DeleteContactFromPerson(string name)
        {
            Debug.WriteLine("Deleted from country");
        }

        [Route("api/Enforces/Measurement/{id:int}")]
        [HttpDelete]
        public void DeleteContactFromPatient(int id)
        {
            Debug.WriteLine("Deleted from measurement");
        }
    }
}
