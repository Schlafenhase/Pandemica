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
    public class PatientPathologiesController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();
            
        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/PatientPathologies")]
        [HttpGet]
        public IEnumerable<PatientPathologies> Get()
        {
            connection.openConnection();
            PatientPathologies[] allrecords;
            allrecords = select.makePatientPathologiesSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/PatientPathologies/Patient/{id}")]
        [HttpGet]
        public IEnumerable<PatientPathologies> GetPatientPathologiesFromPatient(string id)
        {
            connection.openConnection();
            PatientPathologies[] allrecords;
            allrecords = specificSelect.makeSpecificPatientPathologiesSelectByPatient(id).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/PatientPathologies/Pathologies/{id:int}")]
        [HttpGet]
        public IEnumerable<PatientPathologies> GetPatientPathologiesFromPathology(int id)
        {
            connection.openConnection();
            PatientPathologies[] allrecords;
            allrecords = specificSelect.makeSpecificPatientPathologiesSelectByPathology(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/PatientPathologies")]
        [HttpPost]
        public void Post(PatientPathologies patientPathologies)
        {
            connection.openConnection();
            insert.makePatientPathologiesInsert(patientPathologies.patient, patientPathologies.pathology.ToString());
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/PatientPathologies/Patient/{id}")]
        [HttpPut]
        public void PutPatientPathologiesFromPatient(string id, PatientPathologies patientPathologies)
        {
            connection.openConnection();
            update.makeSpecificPatientPathologiesUpdateByPatient(id);
            connection.closeConnection();
            Debug.WriteLine("Updated from Patient");
        }

        [Route("api/PatientPathologies/Pathologies/{id:int}")]
        [HttpPut]
        public void PutPatientPathologiesFromPathology(int id, PatientPathologies patientPathologies)
        {
            connection.openConnection();
            update.makeSpecificPatientPathologiesUpdateByPathology(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Updated from Pathologies");
        }

        [Route("api/PatientPathologies/Patient/{id}")]
        [HttpDelete]
        public void DeletePatientPathologiesFromPatient(string id)
        {
            connection.openConnection();
            delete.makeSpecificPatientPathologiesDeleteByPatient(id);
            connection.closeConnection();
            Debug.WriteLine("Deleted from Patient");
        }

        [Route("api/PatientPathologies/Pathologies/{id:int}")]
        [HttpDelete]
        public void DeletePatientPathologiesFromPathology(int id)
        {
            connection.openConnection();
            delete.makeSpecificPatientPathologiesDeleteByPathology(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted from Pathologies");
        }
    }
}
