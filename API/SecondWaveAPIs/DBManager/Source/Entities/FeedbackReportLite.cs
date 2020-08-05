using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBManager.Source.Entities
{
    public class FeedbackReportLite
    {
        public int healthCenterID { get; set; }
        public int cleanliness { get; set; }
        public int service { get; set; }
        public int punctuality { get; set; }
        
        public FeedbackReportLite() { }

        public FeedbackReportLite(int frlHealthCenterID, int frlCleanliness, int frlService, int frlPunctuality)
        {
            healthCenterID = frlHealthCenterID;
            cleanliness = frlCleanliness;
            service = frlService;
            punctuality = frlPunctuality;
        }
    }
}