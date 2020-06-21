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
    public class PersonController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Person")]
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            connection.openConnection();
            Person[] allrecords;
            allrecords = select.makePersonSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Person/{id:int}")]
        [HttpGet]
        public IEnumerable<Person> Get(int id)
        {
            connection.openConnection();
            Person[] allrecords;
            allrecords = specificSelect.makeSpecificPersonSelectById(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Person")]
        [HttpPost]
        public void Post(Person person)
        {

            connection.openConnection();
            insert.makePersonInsert(person.ssn.ToString(), person.firstName, person.lastName, person.age.ToString(), person.eMail, person.address);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Person/{id:int}")]
        [HttpPut]
        public void Put(int id, Person person)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/Person/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
