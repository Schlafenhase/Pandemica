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
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        Tools tool = new Tools();



        DatabaseDataHolder connection = new DatabaseDataHolder();



        [Route("api/Contact")]
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            connection.openConnection();
            Contact[] allrecords;
            allrecords = select.makeContactSelect().ToArray();
            connection.closeConnection();
            tool.Email("josealejandroibarra@gmail.com");
            return allrecords;
        }

        [Route("api/Contact/Patient/{id}")]
        [HttpGet]
        public IEnumerable<PersonWithDateOfContact> GetContactFromPatient(string id)
        {
            connection.openConnection();
            PersonWithDateOfContact[] allrecords;
            allrecords = select.makePersonSelectFromPatient(id).ToArray();
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

        [Route("api/Contact/{id}")]
        [HttpPut]
        public void Put(string id, Contact contact)
        {
            connection.openConnection();
            update.makeSpecificContactUpdate(id, contact.person, contact.patient);
            connection.closeConnection();
            Debug.WriteLine("Updated from Patient");
        }

        [Route("api/Contact/{id}")]
        [HttpDelete]
        public void Delete(string id)
        {
            connection.openConnection();
            delete.makeSpecificContactDelete(id);
            connection.closeConnection();
            Debug.WriteLine("Deleted from Person");
        }
    }
}
