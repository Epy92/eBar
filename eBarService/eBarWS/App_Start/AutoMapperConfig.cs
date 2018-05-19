using AutoMapper;
using DBModels;
using eBarWS.Models;

namespace eBarWS.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<Restaurants, RestaurantModel>().ReverseMap();
                config.CreateMap<UserTbl, UserModel>().ReverseMap();
                config.CreateMap<Languages, LanguagesModel>().ReverseMap();
                config.CreateMap<RestaurantAdministrators, RestaurantAdministratorsModel>().ReverseMap();
                config.CreateMap<RestaurantDetails, RestaurantDetailsModel>().ReverseMap();
                config.CreateMap<RestaurantProducts, RestaurantProductsModel>().ReverseMap();
            });
        }
    }
}