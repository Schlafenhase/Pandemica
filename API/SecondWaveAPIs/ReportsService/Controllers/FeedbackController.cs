using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using ReportsService.Models;
using ReportsService.Reports;
using MongoClient = ReportsService.Source.Mongo.MongoClient;

namespace ReportsService.Controllers
{
    public class FeedbackController : ApiController
    {
        // GET FEEDBACK RECORDS
        [Route("api/gfr")]
        [HttpGet]
        public HttpResponseMessage GetFeedbackRecords()
        {

            // Get the specific health center reports collection
            const int healthCenterId = 1;
            var collectionName = $"feedbackHC{healthCenterId}";
            var collection = MongoClient.Instance.Db.GetCollection<Feedback>(collectionName);

            // Retrieve all documents in collection
            var documents = collection.Find(new BsonDocument()).ToList();

            // Report generation
            var response = Request.CreateResponse(HttpStatusCode.OK);
            //response.Content = ReportsManager.Instance.GenerateReport(documents);
            response.Content = ReportsManager.Instance.ForcePdf(documents);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }

        // POST NEW FEEDBACK REPORT
        [Route("api/sfr")]
        [HttpGet]
        public void SendFeedbackReport()
        {
            // ANALYZE INCOMING FEEDBACK REPORT LITE (TO INSERT INTO DATABASE) - REPLACE
            var report = new FeedbackLite()
            {
                healthCenterId = 1,
                cleanliness = 1,
                service = 3,
                punctuality = 4
            };

            // Get the specific health center feedback collection
            var healthCenterId = report.healthCenterId;
            var collectionName = $"feedbackHC{healthCenterId}";
            var collection = MongoClient.Instance.Db.GetCollection<BsonDocument>(collectionName);

            // ASSIGN INCOMING INFORMATION - REPLACE
            var cleanlinessQ = report.cleanliness;
            var serviceQ = report.service;
            var punctualityQ = report.punctuality;

            // Create new feedback document and insert it into database
            var feedback = new BsonDocument {
                { "cleanliness", cleanlinessQ },
                { "service", serviceQ },
                { "punctuality", punctualityQ }
            };
            collection.InsertOne(feedback);
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
