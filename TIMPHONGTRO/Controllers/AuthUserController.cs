using MODEL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TIMPHONGTRO.Common;

namespace TIMPHONGTRO.Controllers
{
    public class AuthUserController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var session = (AccountDTO)Session[Constants.USER_SESSION];
            if (session == null || session.RoleId == 1)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "Index", area = "" }));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}