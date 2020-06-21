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

        [Route("api/PatientState/State/{name}")]
        [HttpGet]
        public IEnumerable<PatientState> GetPatientStateFromState(string name)
        {
            connection.openConnection();
            PatientState[] allrecords;
            allrecords = specificSelect.makeSpecificPatientStateSelectByState(name).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/PatientState/Patient/{id:int}")]
        [HttpGet]
        public IEnumerable<PatientState> GetPatientStateFromPatient(int id)
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
            insert.makePatientStateInsert(patientState.state, patientState.patient.ToString(), patientState.date);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/PatientState/State/{name}")]
        [HttpPut]
        public void PutPatientStateFromState(string name, PatientState patientState)
        {
            Debug.WriteLine("Updated from patient");
        }

        [Route("api/PatientState/Patient/{id:int}")]
        [HttpPut]
        public void PutPatientStateFromPatient(int id, PatientState patientState)
        {
            Debug.WriteLine("Updated from pathologies");
        }

        [Route("api/PatientState/State/{name}")]
        [HttpDelete]
        public void DeletePatientStateFromState(string name)
        {
            Debug.WriteLine("Deleted from patient");
        }

        [Route("api/PatientState/Patient/{id:int}")]
        [HttpDelete]
        public void DeletePatientStateFromPatient(int id)
        {
            Debug.WriteLine("Deleted from pathologies");
        }
    }
}
