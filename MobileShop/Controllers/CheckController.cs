using MobileShop.Common;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MobileShop.Controllers
{
    public class CheckController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = (User)Session[Constant.USER_SESSION];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Auth", action = "Login", Area = "" }));

            }
            base.OnActionExecuting(filterContext);
        }
    }
}