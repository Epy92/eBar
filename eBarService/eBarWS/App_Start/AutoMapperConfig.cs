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
            });
        }
    }
}