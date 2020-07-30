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

        /// <summary>
        /// Function in charge of recolecting all the medications of the patients in the database
        /// </summary>
        /// <returns>
        /// List with all the patient medications
        /// </returns>
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

        /// <summary>
        /// Function in charge of searching all the medications from a patient
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <returns>
        /// List with all the medications found
        /// </returns>
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

        /// <summary>
        /// Function in charge of searching all the medications from a patient 
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <returns>
        /// List with all the medications
        /// </returns>
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

        /// <summary>
        /// Function in charge of searching all the medications from a medication 
        /// </summary>
        /// <param name="id">
        /// Id of the medication
        /// </param>
        /// <returns>
        /// List with all the medications
        /// </returns>
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

        /// <summary>
        /// Function in charge in charge of inserting a medication to a patient
        /// </summary>
        /// <param name="patientMedication">
        /// Patient medication to be added
        /// </param>
        [Route("api/PatientMedication")]
        [HttpPost]
        public void Post(SpecialPatientMedicationWithPatientSsn patientMedication)
        {
            connection.openConnection();
            insert.makePatientMedicationInsert(patientMedication.patientSsn, patientMedication.name);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge of updating a patient medication in the database
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <param name="name">
        /// Previous medication name
        /// </param>
        /// <param name="patientMedication">
        /// Patient medication with the new name
        /// </param>
        [Route("api/PatientMedication/{id}/{name}")]
        [HttpPut]
        public void PutPatientMedication(string id, string name, SpecialPatientMedicationWithPatientSsn patientMedication)
        {
            connection.openConnection();
            update.makeSpecificPatientMedicationUpdate(id, patientMedication.name, name);
            connection.closeConnection();
            Debug.WriteLine("Updated from Patient");
        }

        /// <summary>
        /// Function in charge of deleting a patient medication from the database
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <param name="name">
        /// Name of the medication
        /// </param>
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
