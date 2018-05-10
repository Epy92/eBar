using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using eBarWS.DatabaseOperations;
using eBarWS.Interfaces;
using eBarWS.Utils;

namespace eBarWS
{
    public class IocConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // register types
            builder.RegisterType<RestaurantOperations>().As<IRestaurantOperations>();
            builder.RegisterType<RestaurantOperations>().As<IRestaurantOperations>().InstancePerRequest();
            builder.RegisterType<CategoryOperations>().As<ICategoryOperations>().InstancePerRequest();
            builder.RegisterType<ProductOperations>().As<IProductOperations>().InstancePerRequest();
            builder.RegisterType<Logger>().As<ILogger>().InstancePerRequest();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            // build container
            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}