using DBManager.Source.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBManager.Controllers
{
    public class ReportController : ApiController
    {
        [Route("api/Report")]
        [HttpPost]
        public void SendFeedbackReport(FeedbackReportLite report)
        {
            var connectionString = "mongodb+srv://schlafenhase:quebin@reportsservice.fihqz.mongodb.net/reportsDB?retryWrites=true&w=majority";
            var client = new MongoClient(connectionString);
            IMongoDatabase db = client.GetDatabase("reportsDB");

            var healthCenterID = report.healthCenterID;
            var collectionName = "feedbackHC" + healthCenterID;
            var collection = db.GetCollection<BsonDocument>(collectionName);

            var cleanlinessQ = report.cleanliness;
            var serviceQ = report.service;
            var punctualityQ = report.punctuality;

            var feedback = new BsonDocument {
                { "cleanliness", cleanlinessQ },
                { "service", serviceQ },
                { "punctuality", punctualityQ }
            };
            collection.InsertOne(feedback);
        }
    }
}
