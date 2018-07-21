using System;
using System.Globalization;

namespace ViewModels
{
    public class RestaurantEventModel
    {
        public int RestaurantEventID { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public int RestaurantId { get; set; }
        public DateTime PublicationDate { get; set; }
        public string RestaurantThumbnail { get; set; }
        public bool HasThumbnail
        {
            get { return !string.IsNullOrEmpty(RestaurantThumbnail); }
        }
        public string StartDateText
        {
            get
            {
                if (EventStartDate.HasValue)
                {
                    return EventStartDate.Value.Day.ToString() + " " + new CultureInfo("ro").DateTimeFormat.GetMonthName(EventStartDate.Value.Month).Substring(0, 3).ToUpper();
                }
                return "";
            }
        }
        public string EndDateText
        {
            get
            {
                if (EventEndDate.HasValue)
                {
                    return EventEndDate.Value.Day.ToString() + " " + new CultureInfo("ro").DateTimeFormat.GetMonthName(EventEndDate.Value.Month).Substring(0, 3);
                }
                return "";
            }
        }
    }
}