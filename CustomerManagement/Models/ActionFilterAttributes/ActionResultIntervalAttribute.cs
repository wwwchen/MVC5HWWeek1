using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.Models.ActionFilterAttributes
{
    public class ActionResultIntervalAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _sw = new Stopwatch();

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _sw.Start();
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _sw.Stop();
            Debug.WriteLine($"ActionResult : {filterContext.RouteData.Values["controller"]}/{filterContext.RouteData.Values["action"]} : {_sw.ElapsedMilliseconds}");
            base.OnResultExecuted(filterContext);
        }
    }
}