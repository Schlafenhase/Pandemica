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

        [Route("api/PatientMedication/{id}")]
        [HttpGet]
        public IEnumerable<SpecialPatientMedication> GetSpecialPatientMedication(string id)
        {
            connection.openConnection();
            SpecialPatientMedication[] allrecords;
            allrecords = select.makeSpecialPatientMedicationSelect(id).ToArray();
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
        public void Post(SpecialPatientMedicationWithPatientSsn patientMedication)
        {
            connection.openConnection();
            insert.makePatientMedicationInsert(patientMedication.patientSsn, patientMedication.name);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/PatientMedication/{id}/{name}")]
        [HttpPut]
        public void PutPatientMedication(string id, string name, SpecialPatientMedicationWithPatientSsn patientMedication)
        {
            connection.openConnection();
            update.makeSpecificPatientMedicationUpdate(id, patientMedication.name, name);
            connection.closeConnection();
            Debug.WriteLine("Updated from Patient");
        }

        [Route("api/PatientMedication/{id}/{name}")]
        [HttpDelete]
        public void DeletePatientMedication(string id, string name)
        {
            connection.openConnection();
            delete.makeSpecificPatientMedicationDelete(id, name);
            connection.closeConnection();
            Debug.WriteLine("Deleted from Medication");
        }
    }
}
