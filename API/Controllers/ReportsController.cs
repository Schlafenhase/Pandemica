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
        
        /// <summary>
        /// Function in charge of returning a report to the user
        /// </summary>
        /// <param name="reportType">
        /// Type of the report
        /// </param>
        /// <returns>
        /// Report
        /// </returns>
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
                    GetCasesDeaths(response);
                    break;
                default:
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                    break;
            }
            
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }
        
        /// <summary>
        /// Function in charge of generating a patient status report
        /// </summary>
        /// <param name="response">
        /// Response
        /// </param>
        private static void GetPatientsByStatus(HttpResponseMessage response)
        {
            const string filename = "PatientsByStatus.pdf";
            ReportGenerator.ExportPdf(filename, SubFolder, new PatientsReports());
            FindPdf(response, filename);
        }
        
        /// <summary>
        /// Function in charge of generating a deaths report
        /// </summary>
        /// <param name="response">
        /// Response
        /// </param>
        private static void GetCasesDeaths(HttpResponseMessage response)
        {
            const string filename = "CasesDeaths.pdf";
            ReportGenerator.ExportPdf(filename, SubFolder, new DailyReport());
            FindPdf(response, filename);
        }
        
        /// <summary>
        /// Function in charge of finding the pdf
        /// </summary>
        /// <param name="response">
        /// Respones
        /// </param>
        /// <param name="filename">
        /// Filename
        /// </param>
        private static void FindPdf(HttpResponseMessage response, string filename)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SubFolder, filename);
            var pdf = File.OpenRead(path);
            response.Content = new StreamContent(pdf);
        }
    }
}