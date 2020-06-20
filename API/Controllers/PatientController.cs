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
    public class PatientController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Patient")]
        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            Patient p1 = new Patient(1, "Ale", "Ibarra", 20, true, true, "Active", "Costa Rica", "Alajuela", "Costa Rica", 1);
            Patient p2 = new Patient(2, "Jose", "Acuña", 20, true, true, "Active", "Panama", "Nose", "Costa Rica", 2);
            return new Patient[] { p1, p2 };
        }

        [Route("api/Patient/{id:int}")]
        [HttpGet]
        public int Get(int id)
        {
            return id;
        }

        [Route("api/Patient")]
        [HttpPost]
        public void Post(Patient patient)
        {
            connection.openConnection();
            insert.makePatientInsert(patient.ssn.ToString(), patient.firstName, patient.lastName, patient.age.ToString(), patient.hospitalized.ToString(), patient.icu.ToString(), patient.state, patient.country, patient.region, patient.nationality, patient.hospital.ToString());
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Patient/{id:int}")]
        [HttpPut]
        public void Put(int id, Patient patient)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/Patient/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
