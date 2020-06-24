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
            allrecords = specificSelect.makeSpecificPatientStateSelectByState(id.ToString()).ToArray();
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
            connection.openConnection();
            update.makeSpecificPatientStateUpdateByState(id.ToString(), patientState.date);
            connection.closeConnection();
            Debug.WriteLine("Updated from State");
        }

        [Route("api/PatientState/Patient/{id}")]
        [HttpPut]
        public void PutPatientStateFromPatient(string id, PatientState patientState)
        {
            connection.openConnection();
            update.makeSpecificPatientStateUpdateByPatient(id, patientState.date);
            connection.closeConnection();
            Debug.WriteLine("Updated from Patient");
        }

        [Route("api/PatientState/State/{id:int}")]
        [HttpDelete]
        public void DeletePatientStateFromState(int id)
        {
            connection.openConnection();
            delete.makeSpecificPatientStateDeleteByState(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted from State");
        }

        [Route("api/PatientState/Patient/{id}")]
        [HttpDelete]
        public void DeletePatientStateFromPatient(string id)
        {
            connection.openConnection();
            delete.makeSpecificPatientStateDeleteByPatient(id);
            connection.closeConnection();
            Debug.WriteLine("Deleted from Patient");
        }
    }
}
