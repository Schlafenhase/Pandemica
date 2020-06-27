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
    public class PatientController : ApiController
    {

        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Patient")]
        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            connection.openConnection();
            Patient[] allrecords;
            allrecords = select.makePatientSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Patient/{id}")]
        [HttpGet]
        public IEnumerable<Patient> Get(string id)
        {
            connection.openConnection();
            Patient[] allrecords;
            allrecords = specificSelect.makeSpecificPatientSelectById(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Patient")]
        [HttpPost]
        public void Post(Patient patient)
        {
            connection.openConnection();
            insert.makePatientInsert(patient.ssn, patient.firstName, patient.lastName, patient.birthDate, patient.hospitalized.ToString(), patient.icu.ToString(), patient.country, patient.region, patient.nationality, patient.hospital.ToString());
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Patient/{id}")]
        [HttpPut]
        public void Put(string id, Patient patient)
        {
            connection.openConnection();
            update.makeSpecificPatientUpdateById(id, patient.firstName, patient.lastName, patient.birthDate, patient.hospital.ToString(), patient.icu.ToString(), patient.country, patient.region, patient.nationality, patient.hospital.ToString());
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        [Route("api/Patient/{id}")]
        [HttpDelete]
        public void Delete(string id)
        {
            connection.openConnection();
            delete.makeSpecificPatientDeleteBySsn(id);
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
