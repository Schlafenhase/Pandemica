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
    public class PatientMedicationController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/PatientMedication")]
        [HttpGet]
        public IEnumerable<PatientMedication> Get()
        {
            connection.openConnection();
            PatientMedication[] allrecords;
            allrecords = select.makePatientMedicationSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/PatientMedication/Patient/{id}")]
        [HttpGet]
        public IEnumerable<PatientMedication> GetPatientMedicationFromPatient(string id)
        {
            connection.openConnection();
            PatientMedication[] allrecords;
            allrecords = specificSelect.makeSpecificPatientMedicationSelectByPatient(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/PatientMedication/Medication/{id:int}")]
        [HttpGet]
        public IEnumerable<PatientMedication> GetPatientMedicationFromMedication(int id)
        {
            connection.openConnection();
            PatientMedication[] allrecords;
            allrecords = specificSelect.makeSpecificPatientMedicationSelectByMedication(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/PatientMedication")]
        [HttpPost]
        public void Post(PatientMedication patientMedication)
        {
            connection.openConnection();
            insert.makePatientMedicationInsert(patientMedication.patient.ToString(), patientMedication.medication.ToString());
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/PatientMedication/Patient/{id}")]
        [HttpPut]
        public void PutPatientMedicationFromPatient(string id, PatientMedication patientMedication)
        {
            Debug.WriteLine("Updated from patient");
        }

        [Route("api/PatientMedication/Medication/{id:int}")]
        [HttpPut]
        public void PutPatientMedicationFromMedication(int id, PatientMedication patientMedication)
        {
            Debug.WriteLine("Updated from medication");
        }

        [Route("api/PatientMedication/Patient/{id}")]
        [HttpDelete]
        public void DeletePatientMedicationFromPatient(string id)
        {
            Debug.WriteLine("Deleted from patient");
        }

        [Route("api/PatientMedication/Medication/{id:int}")]
        [HttpDelete]
        public void DeletePatientMedicationFromMedication(int id)
        {
            Debug.WriteLine("Deleted from medication");
        }
    }
}
