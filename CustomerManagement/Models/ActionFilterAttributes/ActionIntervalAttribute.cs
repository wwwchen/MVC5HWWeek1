using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.Models.ActionFilterAttributes
{
    public class ActionIntervalAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _sw = new Stopwatch();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _sw.Start();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _sw.Stop();
            Debug.WriteLine($"Action : {filterContext.RouteData.Values["controller"]}/{filterContext.RouteData.Values["action"]} : {_sw.ElapsedMilliseconds}");
            base.OnActionExecuted(filterContext);
        }
    }
}