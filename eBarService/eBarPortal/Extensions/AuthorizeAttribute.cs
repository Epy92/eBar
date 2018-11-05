using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace eBarPortal.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class eBarAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            HttpContextAccessor contxt = new HttpContextAccessor();
            var loggedUser = contxt.HttpContext.Session.GetUserObject<UserModel>("LoggedUser");


            if (loggedUser == null)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                return;
            }

            var isAuthorized = loggedUser != null;
            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }
    }
}
