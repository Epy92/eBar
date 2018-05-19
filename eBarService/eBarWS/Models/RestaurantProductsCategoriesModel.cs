using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBarWS.Models
{
    public class RestaurantProductsCategoriesModel
    {
        public int CategoryId { get; set; }
        public int RestaurantId { get; set; }
        public string CategoryName { get; set; }
    }
}