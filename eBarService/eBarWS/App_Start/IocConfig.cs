using Autofac;
using Autofac.Integration.WebApi;
using eBarWS.Controllers;
using eBarWS.Interfaces;
using eBarDatabase;

namespace eBarWS
{
    public class IocConfig
    {
        public static ContainerBuilder Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<RestaurantController>().AsSelf();
            builder.RegisterType<UserSessionController>().AsSelf();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiModelBinderProvider();
            builder.RegisterType<RestaurantOperations>().As<IRestaurantOperations>();
            builder.RegisterType<UserOperations>().As<IUserOperations>();
            builder.RegisterType<RestaurantOperations>().As<IRestaurantOperations>();
            builder.RegisterType<CategoryOperations>().As<ICategoryOperations>();
            builder.RegisterType<ProductOperations>().As<IProductOperations>();
            builder.RegisterType<Logger>().As<ILogger>();

            return builder;
        }
    }
}