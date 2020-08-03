using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using ReportsService.Reports;

namespace ReportsService.Controllers
{
    public class ReviewReportController : ApiController
    {
        /// <summary>
        /// Function in charge of returning a report to the user
        /// </summary>
        /// <param name="healthCenterId">
        /// Id of the health center
        /// </param>
        /// <returns>
        /// Report
        /// </returns>
        [Route("api/reports/review/{healthCenterId}")]
        [HttpGet]
        public HttpResponseMessage Get(string healthCenterId)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            ReportsManager.Instance.GenerateReport(healthCenterId);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }
    }
}