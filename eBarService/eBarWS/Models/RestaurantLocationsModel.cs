using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBarWS.Models
{
    public class RestaurantLocationsModel
    {
        public int LocationId { get; set; }
        public string RestaurantCity { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantAddress { get; set; }
    }
}