using System;
using System.Globalization;

namespace ViewModels
{
    public class RestaurantEventModel
    {
        public int RestaurantEventID { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }
        public int RestaurantId { get; set; }
        public DateTime EventPublicationDate { get; set; }
        public string RestaurantThumbnail { get; set; }
        public string RestaurantName { get; set; }        
        public bool HasThumbnail
        {
            get { return !string.IsNullOrEmpty(RestaurantThumbnail); }
        }
        public string StartDateText
        {
            get
            {
                if (!string.IsNullOrEmpty(EventStartDate))
                {
                    return DateTime.Parse(EventStartDate).Day.ToString() + " " + new CultureInfo("ro").DateTimeFormat.GetMonthName(DateTime.Parse(EventStartDate).Month).Substring(0, 3).ToUpper();
                }
                return "";
            }
        }
        public string EndDateText
        {
            get
            {
                if (!string.IsNullOrEmpty(EventStartDate))
                {
                    return DateTime.Parse(EventEndDate).Day.ToString() + " " + new CultureInfo("ro").DateTimeFormat.GetMonthName(DateTime.Parse(EventEndDate).Month).Substring(0, 3);
                }
                return "";
            }
        }
    }
}