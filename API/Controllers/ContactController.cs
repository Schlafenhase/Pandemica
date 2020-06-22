using API.Source.Entities;
using API.Source.Server_Connections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Routing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using API.Source.Server_Connections.Specific_Selects;

namespace API.Controllers
{
    public class ContactController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Contact")]
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            connection.openConnection();
            Contact[] allrecords;
            allrecords = select.makeContactSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Contact/Person/{id}")]
        [HttpGet]
        public IEnumerable<Contact> GetContactFromPerson(string id)
        {
            connection.openConnection();
            Contact[] allrecords;
            allrecords = specificSelect.makeSpecificContactSelectByPerson(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Contact/Patient/{id}")]
        [HttpGet]
        public IEnumerable<Contact> GetContactFromPatient(string id)
        {
            connection.openConnection();
            Contact[] allrecords;
            allrecords = specificSelect.makeSpecificContactSelectByPatient(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Contact")]
        [HttpPost]
        public void Post(Contact contact)
        {
            connection.openConnection();
            insert.makeContactInsert(contact.person.ToString(), contact.patient.ToString());
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Contact/Person/{id}")]
        [HttpPut]
        public void PutContactFromPerson(string id, Contact contact)
        {
            Debug.WriteLine("Updated from person");
        }

        [Route("api/Contact/Patient/{id}")]
        [HttpPut]
        public void PutContactFromPatient(string id, Contact contact)
        {
            Debug.WriteLine("Updated from patient");
        }

        [Route("api/Contact/Person/{id}")]
        [HttpDelete]
        public void DeleteContactFromPerson(string id)
        {
            Debug.WriteLine("Deleted from person");
        }

        [Route("api/Contact/Patient/{id}")]
        [HttpDelete]
        public void DeleteContactFromPatient(string id)
        {
            Debug.WriteLine("Deleted from patient");
        }
    }
}
