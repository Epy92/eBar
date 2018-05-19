﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBarWS.Models
{
    public class RestaurantDetailsModel
    {
        public int RestaurantDetailsId { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantDirectoryGuid { get; set; }
        public string RestaurantDescription { get; set; }
        public string RestaurantThumbnail { get; set; }
        public string RestaurantProgram { get; set; }
        public string RestaurantContact { get; set; }
    }
}