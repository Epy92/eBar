using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace eBarWS.Utils
{
    public class EBarAuth : AuthorizationFilterAttribute
    {
        public override bool AllowMultiple
        {
            get { return false; }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var headers = HttpContext.Current.Request.Headers;
            if (headers.HasKeys())
            {
                var sessionKey = headers["Session_ID"];
                if (string.IsNullOrEmpty(sessionKey))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                base.OnAuthorization(actionContext);
            }
        }
    }
}