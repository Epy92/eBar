using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eBarPortal.Models.Restaurant
{
    public class RestaurantDetailsViewModel
    {
        public RestaurantDetailsViewModel()
        {
            RestaurantTypes = new List<SelectListItem>();
            Cities = new List<SelectListItem>();
            Counties = new List<SelectListItem>();
        }
        public string RestaurantTitle { get; set; }
        public string RestaurantDescription { get; set; }
        public List<SelectListItem> RestaurantTypes { get; set; }
        public string RestaurantType { get; set; }
        public int RestaurantTypeId { get; set; }
        public List<SelectListItem> Counties { get; set; }
        public string RestaurantCounty { get; set; }
        public int CountyId { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public string RestaurantCity { get; set; }
        public int CityId { get; set; }
        public string ImageBase64 { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantContact { get; set; }
        public string RestaurantProgram { get; set; }
    }
}
