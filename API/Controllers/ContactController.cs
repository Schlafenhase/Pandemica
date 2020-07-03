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

        DatabaseDataHolder connection = new DatabaseDataHolder();

        /// <summary>
        /// Function in charge of recopilating all the contacts in the database
        /// </summary>
        /// <returns>
        /// A list with all the contacts found
        /// </returns>
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

        /// <summary>
        /// Function in charge of returning all the contacts of a patient
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <returns>
        /// A list with all the contacts found
        /// </returns>
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

        /// <summary>
        /// Function in charge receiving a contact to store it in the database
        /// </summary>
        /// <param name="contact">
        /// Contact to be added
        /// </param>
        [Route("api/Contact")]
        [HttpPost]
        public void Post(Contact contact)
        {
            connection.openConnection();
            insert.makeContactInsert(contact.person.ToString(), contact.patient.ToString());
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge receiving updated data of a contact
        /// </summary>
        /// <param name="id"></param>
        /// Id of the contact to be updated
        /// <param name="contact">
        /// Contact with the data to be updated
        /// </param>
        [Route("api/Contact/{id}")]
        [HttpPut]
        public void Put(string id, Contact contact)
        {
            connection.openConnection();
            update.makeSpecificContactUpdate(id, contact.person, contact.patient);
            connection.closeConnection();
            Debug.WriteLine("Updated from Patient");
        }

        /// <summary>
        /// Function in charge deleting a contact
        /// </summary>
        /// <param name="personSsn">
        /// Ssn of the person
        /// </param>
        /// <param name="patientSsn">
        /// Ssn of the patient
        /// </param>
        [Route("api/Contact/{personSsn}/{patientSsn}")]
        [HttpDelete]
        public void Delete(string personSsn, string patientSsn)
        {
            connection.openConnection();
            delete.makeSpecificContactDelete(personSsn, patientSsn);
            connection.closeConnection();
            Debug.WriteLine("Deleted from Person");
        }
    }
}
