using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;
using IActionFilter = System.Web.Http.Filters.IActionFilter;
using IExceptionFilter = System.Web.Http.Filters.IExceptionFilter;

namespace SchoolManagement.Customize
{
    public class SSLogExecution:  ActionFilterAttribute// /*, //IActionFilter,//IResultFilter,*/, IExceptionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var message = "\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                             " ->" + filterContext.ActionDescriptor.ActionName +
                             " ->OnActionExecuting \t-" + DateTime.Now.ToString() + "\n";

            LogExecutionTime(message);
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var message = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                          " ->" + filterContext.ActionDescriptor.ActionName +
                          " ->OnActionExecuted \t-" + DateTime.Now.ToString() + "\n";

            LogExecutionTime(message);
        }

        public  void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var message = "\n" + filterContext.RouteData.Values["controller"] +
                          " ->" + filterContext.RouteData.Values["action"] +
                          " ->OnResultExecuting \t-" + DateTime.Now.ToString() + "\n";

            LogExecutionTime(message);
        }
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var message = "\n" + filterContext.RouteData.Values["controller"] +
                          " ->" + filterContext.RouteData.Values["action"] +
                          " ->OnActionExecuted \t-" + DateTime.Now.ToString() + "\n";

            LogExecutionTime(message);
        }

        public void OnException(ExceptionContext filterContext)
        {
            var message = "\n" + filterContext.RouteData.Values["controller"] +
                          " ->" + filterContext.RouteData.Values["action"] +
                          " ->" + filterContext.Exception.Message +"\t-" + DateTime.Now.ToString() + "\n";

            LogExecutionTime(message);
        }
    private void LogExecutionTime(string data)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/Logs/InstructorLogfile.txt"),data);
            
        }

    }
}