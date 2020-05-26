using System.Web.Mvc;
using SchoolSystem.DbContext;
using Utility;

namespace SchoolManagement.Controllers
{
    public class BaseController : Controller
    {
        private ILog _Ilog;
        private SchoolDbContext db;

        public BaseController()
        {
            _Ilog = Log.GetInstance;
         //   db = new SchoolDbContext();
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            _Ilog.SchoolSystemExceptions(filterContext.Exception.ToString());
            filterContext.ExceptionHandled = true;
            this.View("Error").ExecuteResult(this.ControllerContext);

        }
    }
}