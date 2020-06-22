using API.Source.Entities;
<<<<<<< HEAD
using API.Source.Server_Connections;
using API.Source.Server_Connections.Specific_Selects;
=======
using API.Source.Server_Connections;
using API.Source.Server_Connections.Specific_Selects;
>>>>>>> API-DEV
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
<<<<<<< HEAD

namespace API.Controllers
{
    public class PatientController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

=======

namespace API.Controllers
{
    public class PatientController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

>>>>>>> API-DEV
        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Patient")]
        [HttpGet]
        public IEnumerable<Patient> Get()
<<<<<<< HEAD
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
=======
        {
            connection.openConnection();
            Patient[] allrecords;
            allrecords = select.makePatientSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/Patient/{id:int}")]
        [HttpGet]
        public IEnumerable<Patient> Get(int id)
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
            insert.makePatientInsert(patient.ssn.ToString(), patient.firstName, patient.lastName, patient.age.ToString(), patient.hospitalized.ToString(), patient.icu.ToString(), patient.state, patient.country, patient.region, patient.nationality, patient.hospital.ToString());
>>>>>>> API-DEV
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

<<<<<<< HEAD
        [Route("api/Patient/{id}")]
        [HttpPut]
        public void Put(string id, Patient patient)
=======
        [Route("api/Patient/{id:int}")]
        [HttpPut]
        public void Put(int id, Patient patient)
>>>>>>> API-DEV
        {
            Debug.WriteLine("Updated");
        }

<<<<<<< HEAD
        [Route("api/Patient/{id}")]
        [HttpDelete]
        public void Delete(string id)
=======
        [Route("api/Patient/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
>>>>>>> API-DEV
        {
            Debug.WriteLine("Deleted");
        }
    }
}
