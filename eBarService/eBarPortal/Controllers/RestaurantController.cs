using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using eBarPortal.APIHelpers;
using eBarPortal.Models.Restaurant;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eBarPortal.Controllers
{
    public class RestaurantController : Controller
    {
        RestaurantApiHelper _restaurantApiHelper = null;
        public RestaurantController()
        {
            _restaurantApiHelper = new RestaurantApiHelper();
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult RestaurantDetails(int restaurantId, string PageMode)
        {
            var model = new RestaurantDetailsViewModel();
            foreach (var item in ViewModels.RestTypesEnum.RestaurantTypes)
            {
                SelectListItem restType = new SelectListItem()
                {
                    Value = item.Key.ToString(),
                    Text = item.Value
                };

                model.RestaurantTypes.Add(restType);
            }

            model.Cities.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Craiova"
            });

            model.Counties.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Dolj"
            });


            if (restaurantId > 0)
            {
                if (string.IsNullOrEmpty(PageMode))
                {
                    PageMode = "ViewMode";
                }
            }
            ViewData["PageMode"] = PageMode;
            return View(model);
        }
    }
}