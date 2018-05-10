using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DBModels;
using eBarWS.Controllers;
using eBarWS.DatabaseOperations;
using eBarWS.Interfaces;
using eBarWS.Utils;

namespace eBarWS
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;            

            builder.RegisterType<RestaurantController>().AsSelf();
            builder.RegisterType<UserSessionController>().AsSelf();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
           
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterWebApiModelBinderProvider();
            builder.RegisterType<RestaurantOperations>().As<IRestaurantOperations>();
            builder.RegisterType<UserOperations>().As<IUserOperations>();
            builder.RegisterType<RestaurantOperations>().As<IRestaurantOperations>();
            builder.RegisterType<CategoryOperations>().As<ICategoryOperations>();
            builder.RegisterType<ProductOperations>().As<IProductOperations>();
            builder.RegisterType<Logger>().As<ILogger>();
            

            var container = builder.Build();
            
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            GlobalConfiguration.Configuration.EnsureInitialized();

            //IocConfig.Configure();

            //GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}
