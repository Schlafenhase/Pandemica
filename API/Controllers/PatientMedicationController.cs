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
    public class PatientMedicationController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();

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

        [Route("api/PatientMedication/Patient/{id:int}")]
        [HttpGet]
        public int GetPatientMedicationFromPatient(int id)
        {
            return id;
        }

        [Route("api/PatientMedication/Medication/{id:int}")]
        [HttpGet]
        public int GetPatientMedicationFromMedication(int id)
        {
            return id;
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

        [Route("api/PatientMedication/Patient/{id:int}")]
        [HttpPut]
        public void PutPatientMedicationFromPatient(int id, PatientMedication patientMedication)
        {
            Debug.WriteLine("Updated from patient");
        }

        [Route("api/PatientMedication/Medication/{id:int}")]
        [HttpPut]
        public void PutPatientMedicationFromMedication(int id, PatientMedication patientMedication)
        {
            Debug.WriteLine("Updated from medication");
        }

        [Route("api/PatientMedication/Patient/{id:int}")]
        [HttpDelete]
        public void DeletePatientMedicationFromPatient(int id)
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
