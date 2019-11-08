using System;
using System.Collections.Generic;

namespace TruckApp.Models
{
    public class Review
    {
        public int REVIEW_ID { get; set; }
        public int TRUCK_ID { get; set; }
        public string REVIEWER { get; set; }
        public int RATING { get; set; }
        public string REVIEW { get; set; }
        
    }
}
