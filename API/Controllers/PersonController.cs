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
        Tools tool = new Tools();

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
        public void Post(PersonWithPatientSsn person)
        {
            connection.openConnection();
            insert.makePersonInsert(person.ssn, person.firstName, person.lastName, person.birthDate, person.eMail, person.address, person.sex, person.contactDate, person.patientSsn);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
            tool.Email(person.eMail);
        }

        [Route("api/Person/{id}")]
        [HttpPut]
        public void Put(string id, PersonWithPatientSsn person)
        {
            connection.openConnection();
            update.makeSpecificPersonUpdate(id, person.firstName, person.lastName, person.birthDate, person.eMail, person.address, person.sex, person.contactDate, person.patientSsn);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        [Route("api/Person/{id}")]
        [HttpDelete]
        public void Delete(string id)
        {
            connection.openConnection();
            delete.makeSpecificPersonDelete(id);
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
