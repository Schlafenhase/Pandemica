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
    public class PatientPathologiesController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/PatientPathologies")]
        [HttpGet]
        public IEnumerable<PatientPathologies> Get()
        {
            connection.openConnection();
            PatientPathologies[] allrecords;
            allrecords = select.makePatientPathologiesSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/PatientPathologies/Patient/{id}")]
        [HttpGet]
        public IEnumerable<PatientPathologies> GetPatientPathologiesFromPatient(string id)
        {
            connection.openConnection();
            PatientPathologies[] allrecords;
            allrecords = specificSelect.makeSpecificPatientPathologiesSelectByPatient(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/PatientPathologies/Pathologies/{id:int}")]
        [HttpGet]
        public IEnumerable<PatientPathologies> GetPatientPathologiesFromPathology(int id)
        {
            connection.openConnection();
            PatientPathologies[] allrecords;
            allrecords = specificSelect.makeSpecificPatientPathologiesSelectByPathology(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/PatientPathologies")]
        [HttpPost]
        public void Post(PatientPathologies patientPathologies)
        {
            connection.openConnection();
            insert.makePatientPathologiesInsert(patientPathologies.patient, patientPathologies.pathology.ToString());
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/PatientPathologies/Patient/{id}")]
        [HttpPut]
        public void PutPatientPathologiesFromPatient(string id, PatientPathologies patientPathologies)
        {
            Debug.WriteLine("Updated from patient");
        }

        [Route("api/PatientPathologies/Pathologies/{id:int}")]
        [HttpPut]
        public void PutPatientPathologiesFromMedication(int id, PatientPathologies patientPathologies)
        {
            Debug.WriteLine("Updated from pathologies");
        }

        [Route("api/PatientPathologies/Patient/{id}")]
        [HttpDelete]
        public void DeletePatientPathologiesFromPatient(string id)
        {
            Debug.WriteLine("Deleted from patient");
        }

        [Route("api/PatientPathologies/Pathologies/{id:int}")]
        [HttpDelete]
        public void DeletePatientPathologiesFromMedication(int id)
        {
            Debug.WriteLine("Deleted from pathologies");
        }
    }
}
