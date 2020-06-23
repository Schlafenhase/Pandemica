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
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

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
            allrecords = specificSelect.makeSpecificPatientMedicationSelectByMedication(id.ToString()).ToArray();
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
            connection.openConnection();
            update.makeSpecificPatientMedicationUpdateByPatient(id);
            connection.closeConnection();
            Debug.WriteLine("Updated from Patient");
        }

        [Route("api/PatientMedication/Medication/{id:int}")]
        [HttpPut]
        public void PutPatientMedicationFromMedication(int id, PatientMedication patientMedication)
        {
            connection.openConnection();
            update.makeSpecificPatientMedicationUpdateByMedication(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Updated from Medication");
        }

        [Route("api/PatientMedication/Patient/{id}")]
        [HttpDelete]
        public void DeletePatientMedicationFromPatient(string id)
        {
            connection.openConnection();
            delete.makeSpecificPatientMedicationDeleteByPatient(id);
            connection.closeConnection();
            Debug.WriteLine("Deleted from Patient");
        }

        [Route("api/PatientMedication/Medication/{id:int}")]
        [HttpDelete]
        public void DeletePatientMedicationFromMedication(int id)
        {
            connection.openConnection();
            delete.makeSpecificPatientMedicationDeleteByMedication(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted from Medication");
        }
    }
}
