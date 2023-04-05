using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ProyectowebB.App_Start
{
    public class SessionFilter : ActionFilterAttribute
    {
        private const string ControllerName = "Home";
        private const string ActionName = "Index";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;

            if (session == null || session.Count == 0)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                { "controller", ControllerName },
                { "action", ActionName }
            });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}