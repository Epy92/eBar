using System;
using Newtonsoft.Json;

namespace eBarWS.Models
{
    public class RestaurantEvent
    {
        public string EventTitle { get; set; }
        public int EventId { get; set; }
        public string EventDescription { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public string RestaurantThmumbnail { get; set; }
        public bool HasThumbnail { get; set; }
        public int RestaurantId { get; set; }
    }
}