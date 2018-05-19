using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBarWS.Models
{
    public class RestaurantTypesModel
    {
        public int TypeId { get; set; }
        public int RestaurantId { get; set; }
        public string TypeName { get; set; }
    }
}