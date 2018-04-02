﻿using System.IO;

namespace eBarService.Models
{
    public class RestaurantModel
    {
        public string RestaurantName { get; set; }
        public string RestaurantCity { get; set; }
        public string RestaurantType { get; set; }
        public string RestaurantDescription { get; set; }
        public string RestaurantContentType { get; set; }
        public string RestaurantAddress { get; set; }
        public string ThumbnailBase64String{ get; set; }

        public bool HasThumbnail => !string.IsNullOrEmpty(ThumbnailBase64String);
    }
}