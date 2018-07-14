using System;
using System.Web;
using System.Web.Http;
using Autofac.Integration.WebApi;
using eBarWS.App_Start;
using Newtonsoft.Json;

namespace eBarWS
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.Initialize();
            var config = GlobalConfiguration.Configuration;

            var builder = IocConfig.Configure();
            builder.RegisterWebApiFilterProvider(config);
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            GlobalConfiguration.Configuration.EnsureInitialized();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
        }

        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            string origin = HttpContext.Current?.Request.Headers.Get("Origin");

            if (!string.IsNullOrEmpty(origin))
            {
                //todo check origin to be in whitelist + create whitelist
                Response.Headers.Remove("Access-Control-Allow-Origin");
                HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", origin);
            }

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                Response.Headers.Remove("Access-Control-Allow-Origin");
                Response.AddHeader("Access-Control-Allow-Origin", HttpContext.Current.Request.Headers.Get("Origin"));
                //Response.AddHeader("Access-Control-Allow-Origin", Request.UrlReferrer.GetLeftPart(UriPartial.Authority));

                Response.Headers.Remove("Access-Control-Allow-Methods");
                Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");

                Response.Headers.Remove("Access-Control-Allow-Headers");
                Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Content-Length, Connection, Authorization, Accept, Accept-Encoding, Accept-Language, Host, User-Agent, Access-Control-Request-Method, Origin,  X-CSRF-Token, X-Requested-With");

                Response.StatusCode = 200;
                Response.End();
            }
        }
    }
}
