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

        /// <summary>
        /// Function in charge of recolecting all the patients in the database
        /// </summary>
        /// <returns>
        /// List with all the patients found
        /// </returns>
        [Route("api/Patient")]
        [HttpGet]
        public IEnumerable<PatientWithHospitalId> Get()
        {
            connection.openConnection();
            PatientWithHospitalId[] allrecords;
            allrecords = select.makePatientSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of seaching a patient through ssn
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <returns>
        /// List with the found patient
        /// </returns>
        [Route("api/Patient/{id}")]
        [HttpGet]
        public IEnumerable<PatientWithHospitalId> Get(string id)
        {
            connection.openConnection();
            PatientWithHospitalId[] allrecords;
            allrecords = specificSelect.makeSpecificPatientSelectById(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of recolecting all the patients of an hospital
        /// </summary>
        /// <param name="id">
        /// Id of the hospital
        /// </param>
        /// <returns>
        /// List with all the patients found
        /// </returns>
        [Route("api/Patient/Hospital/{id:int}")]
        [HttpGet]
        public IEnumerable<Patient> GetPatientsFromHospital(int id)
        {
            connection.openConnection();
            Patient[] allrecords;
            allrecords = specificSelect.makeSpecificPatientSelectByHospital(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of inserting a patient to the database
        /// </summary>
        /// <param name="patient">
        /// Patient to be added
        /// </param>
        [Route("api/Patient")]
        [HttpPost]
        public void Post(PatientWithHospitalId patient)
        {
            connection.openConnection();
            insert.makePatientInsert(patient.ssn, patient.firstName, patient.lastName, patient.birthDate, patient.hospitalized.ToString(), patient.icu.ToString(), patient.country, patient.region, patient.nationality, patient.hospital.ToString(), patient.sex);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge of updating a patient from the database
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <param name="patient">
        /// Patient with the updated data
        /// </param>
        [Route("api/Patient/{id}")]
        [HttpPut]
        public void Put(string id, PatientWithHospitalId patient)
        {
            connection.openConnection();
            update.makeSpecificPatientUpdate(id, patient.firstName, patient.lastName, patient.birthDate, patient.hospitalized.ToString(), patient.icu.ToString(), patient.country, patient.region, patient.nationality, patient.hospital.ToString(), patient.sex);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        /// <summary>
        /// Function in charge of deleting a patient from the database
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        [Route("api/Patient/{id}")]
        [HttpDelete]
        public void Delete(string id)
        {
            connection.openConnection();
            delete.makeSpecificPatientDelete(id);
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
