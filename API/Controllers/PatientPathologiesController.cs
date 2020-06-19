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
    public class PatientPathologiesController : ApiController
    {
        General_Insert insert = new General_Insert();
        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/PatientPathologies")]
        [HttpGet]
        public IEnumerable<PatientPathologies> Get()
        {
            PatientPathologies pc1 = new PatientPathologies(1, "Maicra");
            PatientPathologies pc2 = new PatientPathologies(2, "FFFFF");
            return new PatientPathologies[] { pc1, pc2 };
        }

        [Route("api/PatientPathologies/Patient/{id:int}")]
        [HttpGet]
        public int GetPatientPathologiesFromPatient(int id)
        {
            return id;
        }

        [Route("api/PatientPathologies/Pathologies/{name}")]
        [HttpGet]
        public string GetPatientPathologiesFromMedication(string name)
        {
            return name;
        }

        [Route("api/PatientPathologies")]
        [HttpPost]
        public void Post(PatientPathologies patientPathologies)
        {
            connection.openConnection();
            insert.makePatientPathologiesInsert(patientPathologies.patient.ToString(), patientPathologies.pathology);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/PatientPathologies/Patient/{id:int}")]
        [HttpPut]
        public void PutPatientPathologiesFromPatient(int id, PatientPathologies patientPathologies)
        {
            Debug.WriteLine("Updated from patient");
        }

        [Route("api/PatientPathologies/Pathologies/{name}")]
        [HttpPut]
        public void PutPatientPathologiesFromMedication(string name, PatientPathologies patientPathologies)
        {
            Debug.WriteLine("Updated from pathologies");
        }

        [Route("api/PatientPathologies/Patient/{id:int}")]
        [HttpDelete]
        public void DeletePatientPathologiesFromPatient(int id)
        {
            Debug.WriteLine("Deleted from patient");
        }

        [Route("api/PatientPathologies/Pathologies/{name}")]
        [HttpDelete]
        public void DeletePatientPathologiesFromMedication(string name)
        {
            Debug.WriteLine("Deleted from pathologies");
        }
    }
}
