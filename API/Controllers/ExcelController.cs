using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using API.Excel;

namespace API.Controllers
{
    public class ExcelController : ApiController
    {
        /// <summary>
        /// Function in charge of receiving an excel with data of patients
        /// </summary>
        /// <returns>
        /// Signal to notify that the upload was succesful
        /// </returns>
        [Route("api/excel")]
        [HttpPost]
        public async Task<IHttpActionResult> Upload()
        {
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
            }

            return Ok();
        }
    }
}