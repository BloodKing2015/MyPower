using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyPower.Common
{
    public class Authorized : AuthorizeAttribute
    {
        private const string UserSessionKey = "usr";

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            base.OnAuthorization(filterContext);

            if (filterContext.HttpContext.Session["UserSessionKey"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                     {
                         {"controller", "Login"},
                         {"action", "Index"},
                         {"returnUrl", filterContext.HttpContext.Request.RawUrl}
                     });
                return;
            }
            return;
        }
    }
}