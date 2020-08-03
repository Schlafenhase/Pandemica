namespace ReportsService.Models
{
    public class FeedbackLite
    {
        public int healthCenterId { get; set; }
        public int cleanliness { get; set; }
        public int service { get; set; }
        public int punctuality { get; set; }
    }
}