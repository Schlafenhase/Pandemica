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
    public class PatientStateController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        /// <summary>
        /// Function in charge of recolecting all the patient states from the database
        /// </summary>
        /// <returns>
        /// List with all the patient states
        /// </returns>
        [Route("api/PatientState")]
        [HttpGet]
        public IEnumerable<PatientState> Get()
        {
            connection.openConnection();
            PatientState[] allrecords;
            allrecords = select.makePatientStateSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of recolecing all the states from a patient
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <returns>
        /// List with all the patient states found
        /// </returns>
        [Route("api/PatientState/{id}")]
        [HttpGet]
        public IEnumerable<SpecialPatientState> GetSpecialPatientState(string id)
        {
            connection.openConnection();
            SpecialPatientState[] allrecords;
            allrecords = select.makeSpecialPatientStateSelect(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of recolecing all the states from a state
        /// </summary>
        /// <param name="id">
        /// Id of the state
        /// </param>
        /// <returns>
        /// List with all the patient states found
        /// </returns>
        [Route("api/PatientState/State/{id:int}")]
        [HttpGet]
        public IEnumerable<PatientState> GetPatientStateFromState(int id)
        {
            connection.openConnection();
            PatientState[] allrecords;
            allrecords = specificSelect.makeSpecificPatientStateSelectByState(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of recolecing all the states from a patient
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <returns>
        /// List with all the patient states found
        /// </returns>
        [Route("api/PatientState/Patient/{id}")]
        [HttpGet]
        public IEnumerable<PatientState> GetPatientStateFromPatient(string id)
        {
            connection.openConnection();
            PatientState[] allrecords;
            allrecords = specificSelect.makeSpecificPatientStateSelectByPatient(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of inserting a patient state to the database
        /// </summary>
        /// <param name="patientState">
        /// Patient state to be added
        /// </param>
        [Route("api/PatientState")]
        [HttpPost]
        public void Post(SpecialPatientStateWithPatientSsn patientState)
        {
            connection.openConnection();
            insert.makePatientStateInsert(patientState.name, patientState.patientSsn, patientState.date);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge of updating a patient state in the database
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <param name="name">
        /// Previous name of the state
        /// </param>
        /// <param name="month">
        /// Month of the new date</param>
        /// <param name="day">
        /// Day of the new date
        /// </param>
        /// <param name="year">
        /// Year of the new date
        /// </param>
        /// <param name="patientState">
        /// Patient state with the new data
        /// </param>
        [Route("api/PatientState/{id}/{name}/{month}/{day}/{year}")]
        [HttpPut]
        public void PutPatientState(string id, string name, string month, string day, string year, SpecialPatientState patientState)
        {
            string date = month + "/" + day + "/" + year;
            connection.openConnection();
            update.makeSpecificPatientStateUpdate(patientState.name, id, patientState.date, name, date);
            connection.closeConnection();
            Debug.WriteLine("Updated from State");
        }

        /// <summary>
        /// Function in charge deleting a patient state from the database
        /// </summary>
        /// <param name="id">
        /// Ssn of the patient
        /// </param>
        /// <param name="name">
        /// Name of the state
        /// </param>
        [Route("api/PatientState/{id}/{name}")]
        [HttpDelete]
        public void DeletePatientState(string id, string name)
        {
            connection.openConnection();
            delete.makeSpecificPatientStateDelete(id, name);
            connection.closeConnection();
            Debug.WriteLine("Deleted from State");
        }
    }
}
