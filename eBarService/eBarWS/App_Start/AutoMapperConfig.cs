﻿using AutoMapper;
using eBarDatabase;
using ViewModels;

namespace eBarWS.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<Languages, LanguagesModel>().ReverseMap();
                config.CreateMap<RestaurantAdministrators, RestaurantAdministratorsModel>().ReverseMap();
                config.CreateMap<RestaurantDetails, RestaurantDetailsModel>().ReverseMap();
                config.CreateMap<RestaurantLocations, RestaurantLocationsModel>().ReverseMap();
                config.CreateMap<RestaurantProducts, RestaurantProductsModel>().ReverseMap();
                config.CreateMap<RestaurantProductsCategories, RestaurantProductsCategoriesModel>().ReverseMap();
                config.CreateMap<Restaurants, RestaurantModel>().ReverseMap();
                config.CreateMap<RestaurantTables, RestaurantTablesModel>().ReverseMap();
                config.CreateMap<RestaurantTypes, RestaurantTypesModel>().ReverseMap();
                config.CreateMap<UserResetPasswordCodes, UserResetPasswordCodesModel>();
                config.CreateMap<UserTbl, UserModel>().ReverseMap();
                config.CreateMap<UserTypes, UserTypesModel>().ReverseMap();
               
                config.CreateMap<RestaurantProducts, RestaurantProductsModel>().ReverseMap();
                config.CreateMap<RestaurantEvent, RestaurantEventModel>().ReverseMap();
                config.CreateMap<RestaurantReview, RestaurantReviewModel>().ReverseMap();
            });
        }
    }
}