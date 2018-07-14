using System;

namespace ViewModels
{
    public class RestaurantEvent
    {
        public string EventTitle { get; set; }
        public int EventId { get; set; }
        public string EventDescription { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public string RestaurantThumbnail { get; set; }
        public bool HasThumbnail
        {
            get { return !string.IsNullOrEmpty(RestaurantThumbnail); }
        }
        public int RestaurantId { get; set; }
    }
}