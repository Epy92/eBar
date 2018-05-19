using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBarWS.Models
{
    public class RestaurantAdministratorsModel
    {
        public int RestaurantAdminId { get; set; }
        public int RestaurantId { get; set; }
        public int UserID { get; set; }
    }
}