using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using API.Excel;
using API.Source.Entities;
using API.Source.Server_Connections;

namespace API.Controllers
{
    public class ExcelController : ApiController
    {
        DatabaseDataHolder connection = new DatabaseDataHolder();
        
        /// <summary>
        /// Function in charge of receiving an excel with data of patients
        /// </summary>
        /// <returns>
        /// Signal to notify that the upload was succesful
        /// </returns>
        [Route("api/excel")]
        [HttpPost]
        public async Task<List<string>> Upload()
        {
            var errors = new List<string>();
            // Verifies if content is multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType); 

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            // Read each file
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();
                Console.WriteLine($"Uploaded: {filename}");
                
                // Do whatever you want with filename and its binary data.
                var patients = PatientExcelParser.LoadPatients(buffer);
                errors = InsertPatients(patients);
            }

            return errors;
        }

        // Insert each patient in the DB
        private List<string> InsertPatients(IEnumerable<PatientX> patients)
        {
            connection.openConnection();
            var inserter = new GeneralInsert();
            foreach (var patient in patients)
            {
                inserter.makePatientInsert(
                    patient.ssn, 
                    patient.firstName,
                    patient.lastName,
                    patient.birthDate,
                    patient.hospitalized? "0" : "1",
                    patient.icu? "0" : "1",
                    patient.country,
                    patient.region ?? "",
                    patient.nationality,
                    patient.hospital ?? "1",
                    patient.sex);
                inserter.makePatientStateInsert(
                    patient.state, 
                    patient.ssn, 
                    DateTime.Today.ToShortDateString());
            }
            connection.closeConnection();
            return inserter.patientsError;
        }
    }
}