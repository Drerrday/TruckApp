using System;
namespace TruckApp.Models
{
    public class Truck
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int ReviewID { get; set; }
        public string ImageLink { get; set; }
        public Review Review { get; set; }
    }
}
