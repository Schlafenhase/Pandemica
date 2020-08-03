using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReportsService.Models
{
    public class FeedbackReport
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public int cleanliness { get; set; }
        public int service { get; set; }
        public int punctuality { get; set; }
    }
}