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
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

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

        [Route("api/Person/{id}")]
        [HttpGet]
        public IEnumerable<Person> Get(string id)
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
            insert.makePersonInsert(person.ssn, person.firstName, person.lastName, person.birthDate, person.eMail, person.address);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Person/{id}")]
        [HttpPut]
        public void Put(string id, Person person)
        {
            connection.openConnection();
            update.makeSpecificPersonUpdateById(id, person.firstName, person.lastName, person.birthDate, person.eMail, person.address);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        [Route("api/Person/{id}")]
        [HttpDelete]
        public void Delete(string id)
        {
            connection.openConnection();
            delete.makeSpecificPersonDeleteBySsn(id);
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
