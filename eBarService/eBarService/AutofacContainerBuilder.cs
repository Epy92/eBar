using System.Web.Management;
using Autofac;
using Autofac.Integration.Wcf;
using eBarService.DatabaseOperations;
using eBarService.Interfaces;

namespace eBarService
{
    public static class AutofacContainerBuilder
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // register types
            builder.RegisterType<UserOperations>().As<IUserOperations>();
            builder.RegisterType<RestaurantOperations>().As<IRestaurantOperations>();
            builder.RegisterType<CategoryOperations>().As<ICategoryOperations>();
            builder.RegisterType<ProductOperations>().As<IProductOperations>();
            builder.RegisterType<UserService>();
            builder.RegisterType<RestaurantService>();

            // build container
            return builder.Build();
        }
    }
}