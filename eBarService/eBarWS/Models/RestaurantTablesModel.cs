using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBarWS.Models
{
    public class RestaurantTablesModel
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public string TableBarcode { get; set; }
        public int RestaurantId { get; set; }
    }
}