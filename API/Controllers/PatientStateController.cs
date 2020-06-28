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

        DatabaseDataHolder connection = new DatabaseDataHolder();

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

        [Route("api/PatientState/State/{id:int}")]
        [HttpGet]
        public IEnumerable<PatientState> GetPatientStateFromState(int id)
        {
            connection.openConnection();
            PatientState[] allrecords;
            allrecords = specificSelect.makeSpecificPatientStateSelectByState(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

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

        [Route("api/PatientState")]
        [HttpPost]
        public void Post(PatientState patientState)
        {
            connection.openConnection();
            insert.makePatientStateInsert(patientState.state.ToString(), patientState.patient, patientState.date);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/PatientState/State/{id:int}")]
        [HttpPut]
        public void PutPatientStateFromState(int id, PatientState patientState)
        {
            Debug.WriteLine("Updated from patient");
        }

        [Route("api/PatientState/Patient/{id}")]
        [HttpPut]
        public void PutPatientStateFromPatient(string id, PatientState patientState)
        {
            Debug.WriteLine("Updated from pathologies");
        }

        [Route("api/PatientState/State/{id:int}")]
        [HttpDelete]
        public void DeletePatientStateFromState(int id)
        {
            Debug.WriteLine("Deleted from patient");
        }

        [Route("api/PatientState/Patient/{id}")]
        [HttpDelete]
        public void DeletePatientStateFromPatient(string id)
        {
            Debug.WriteLine("Deleted from pathologies");
        }
    }
}
