using API.Source.Entities;
using API.Source.Server_Connections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Routing;
using System.Data;
using System.Data.SqlClient;


namespace API.Controllers
{
    public class ContactController : ApiController
    {
        General_Insert insert = new General_Insert();
        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Contact")]
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            Contact c1 = new Contact(1, 2);
            Contact c2 = new Contact(2, 3);
            return new Contact[] { c1, c2 };
        }

        [Route("api/Contact/Person/{id:int}")]
        [HttpGet]
        public int GetContactFromPerson(int id)
        {
            return id;
        }

        [Route("api/Contact/Patient/{id:int}")]
        [HttpGet]
        public int GetContactFromPatient(int id)
        {
            return id;
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

        [Route("api/Contact/Person/{id:int}")]
        [HttpPut]
        public void PutContactFromPerson(int id, Contact contact)
        {
            Debug.WriteLine("Updated from person");
        }

        [Route("api/Contact/Patient/{id:int}")]
        [HttpPut]
        public void PutContactFromPatient(int id, Contact contact)
        {
            Debug.WriteLine("Updated from patient");
        }

        [Route("api/Contact/Person/{id:int}")]
        [HttpDelete]
        public void DeleteContactFromPerson(int id)
        {
            Debug.WriteLine("Deleted from person");
        }

        [Route("api/Contact/Patient/{id:int}")]
        [HttpDelete]
        public void DeleteContactFromPatient(int id)
        {
            Debug.WriteLine("Deleted from patient");
        }
    }
}
