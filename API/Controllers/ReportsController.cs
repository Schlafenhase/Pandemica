using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using API.Reports;

namespace API.Controllers
{
    public class ReportsController : ApiController
    {
        private const string SubFolder = "App_Data/Reports";
            
        [Route("api/reports/{reportType}")]
        [HttpGet]
        public HttpResponseMessage GetReports(string reportType)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            switch (reportType)
            {
                case "patientsByStatus":
                    GetPatientsByStatus(response);
                    break;
                case "casesDeaths":
                    break;
                default:
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    break;
            }
            
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }
        
        private static void GetPatientsByStatus(HttpResponseMessage response)
        {
            const string filename = "PatientsByStatus.pdf";
            ReportGenerator.ExportPdf(filename, SubFolder, new PatientsReport());
            FindPdf(response, filename);
        }
        
        private static void FindPdf(HttpResponseMessage response, string filename)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SubFolder, filename);
            var pdf = File.OpenRead(path);
            response.Content = new StreamContent(pdf);
        }
    }
}