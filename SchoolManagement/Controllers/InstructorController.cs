using System;
using SchoolSystem.DbContext;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using PagedList;
using SchoolManagement.Controllers;

namespace SchoolSystem.MVC.Models.Controllers
{
    public class InstructorController : BaseController
    {
        private SchoolDbContext db;
        public InstructorController()
        {
            db = new SchoolDbContext();
        }
        //[Authorize] will through 404 - do not have permission.
        public ActionResult Index(int? page)
        {
            return View(SelectAllInstructors().ToPagedList(page ?? 1, 7));
        }
        public ActionResult Details(int id)
        {
            var instructor = SelectAnInstructor(id);
            if (instructor == null){
                return HttpNotFound();
            }
            return View(instructor);
        }
        public ActionResult Create()
        {
            var Gender = from Gender g in Enum.GetValues(typeof(Gender))
                select new
                {
                    ID= g.ToString(),
                    Name = g.ToString(),
                };

            ViewData["VDGender"] = new SelectList(Gender, "ID", "Name");
            ViewData["AllDepartment"] = new SelectList(db.departments, "DepartmentId", "DepartmentName");

            ViewData["AllCourse"] = new SelectList(db.courses, "CourseId", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstructorId,LastName,FirstName,Gender,Department,Course")] Instructor postedInstructor)
        {
            if (ModelState.IsValid)
            {
                var t = new DbModels.Model.Instructor
                {
                    FirstName = postedInstructor.FirstName,
                    LastName = postedInstructor.LastName,
                    Gender = postedInstructor.Gender,
                    DepartmentId = int.Parse(postedInstructor.Department),
                    InstructorId = postedInstructor.InstructorId,
                    CourseId = int.Parse(postedInstructor.Course)
                };
                db.instructors.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postedInstructor);
        }
        public ActionResult Edit(int id)
        {
            var record = db.instructors.Include(x => x.Department).Include(x => x.Course).FirstOrDefault(x => x.InstructorId == id);
            var teacher = new Instructor
            {
                InstructorId = record.InstructorId,
                FirstName = record.FirstName,
                LastName = record.LastName,
                Gender = record.Gender,
                //HireDate will not be edited
                DepartmentId = record.Department.DepartmentId,
                CourseId = record.Course.CourseId
            };

            if (teacher == null)
            {
                return HttpNotFound();
            }
            
            var deptChoise = new List<SelectListItem>();
            foreach (var dept in db.departments)
            {
                deptChoise.Add(new SelectListItem()
                {
                    Text = dept.DepartmentName,
                    Value = dept.DepartmentId.ToString(),
                    Selected = dept.DepartmentId == record.Department.DepartmentId ? true : false
                });
            }
            ViewData["AllDepartment"] = deptChoise;
            var courseChoise = new List<SelectListItem>();
            foreach (var course in db.courses)
            {
                courseChoise.Add(new SelectListItem()
                {
                    Text = course.Title,
                    Value = course.CourseId.ToString(),
                    Selected = course.CourseId == record.Course.CourseId ? true : false
                });
            }
            ViewData["AllCourse"] = courseChoise;
        return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstructorId,LastName,FirstName,Gender,Department,Course")] Instructor instructor)
        {
            
        var instructorToEdit = new SchoolSystem.DbModels.Model.Instructor
            {
                InstructorId = instructor.InstructorId,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Gender = instructor.Gender,
                //HireDate = instructor.HireDate,
                DepartmentId = int.Parse(instructor.Department),
                CourseId = int.Parse(instructor.Course)

            };

            if (ModelState.IsValid)
            {
                db.Entry(instructorToEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instructor);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var teacher = db.instructors.Include(x => x.Department).Include(x => x.Course).FirstOrDefault(x => x.InstructorId == id);
            var teacherToDelete = new Instructor
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                /*HireDate = teacher.HireDate,
                Gender = teacher.Gender.ToString(),*/
                DepartmentId = teacher.Department.DepartmentId,
                CourseId = teacher.Course.CourseId,
                Department = teacher.Department.DepartmentName,
                Course = teacher.Course.Title
            };
            if (teacherToDelete == null)
            {
                return HttpNotFound();
            }

            return View(teacherToDelete);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var teacherConfirmDelete = db.instructors.Include(y => y.Department).FirstOrDefault(x => x.InstructorId == id);

            db.instructors.Remove(teacherConfirmDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public List<Instructor> SelectAllInstructors()
        {
            var record = db.instructors.Include(x => x.Department).Include(x => x.Course).ToList();
            List<Instructor> listOfTeachers = new List<Instructor>();
            foreach (var instructor in record)
            {
                listOfTeachers.Add(new Instructor()
                {
                    InstructorId = instructor.InstructorId,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    Gender = instructor.Gender,
                    HireDate = (DateTime)instructor.HireDate,
                    Department = instructor.Department.DepartmentName,
                    DepartmentId = instructor.Department.DepartmentId,
                    Course = instructor.Course.Title,
                    CourseId = instructor.Course.CourseId
                });
            }
            return listOfTeachers.OrderBy(x => x.Gender).ThenBy(x => x.LastName).ToList();
        }
        public Instructor SelectAnInstructor(int id)
        {
            var record = db.instructors.Include(x => x.Department).Include(x => x.Course).FirstOrDefault(x => x.InstructorId == id);
            return new Instructor()
            {
                InstructorId = record.InstructorId,
                FirstName = record.FirstName,
                LastName = record.LastName,
                Gender = record.Gender.ToString(),
                HireDate = (DateTime)record.HireDate,
                Department = record.Department.DepartmentName,
                DepartmentId = record.DepartmentId,
                Course = record.Course.Title,
                CourseId = record.CourseId
            };
        }
        public string findByLastName(string searchLastName)
        { 
            var teachers = db.instructors.Include(x => x.Department).Include(x => x.Course).Where(x => x.FirstName.ToUpper().Contains(searchLastName.ToUpper())).ToList();
           var listOfTeachers = new List<Instructor>();
            foreach (var teacher in teachers)
            {
                listOfTeachers.Add(new Instructor()
                {
                    InstructorId = teacher.InstructorId,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Gender = teacher.Gender,
                    HireDate = (DateTime)teacher.HireDate,
                    Department = teacher.Department.DepartmentName,
                    DepartmentId = teacher.Department.DepartmentId,
                    Course = teacher.Course.Title,
                    CourseId = teacher.Course.CourseId
                });
            }
            return PartialView("PartialList").RenderToString(HttpContext, listOfTeachers);
        }
    }
    /// <summary>
    /// Extension method to be seen again
    /// </summary>
    public static class ViewExtensions
    {
        public static string RenderToString(this PartialViewResult partialView, HttpContextBase httpContext, object model)
        {
            if (httpContext == null)
            {
                throw new NotSupportedException("An HTTP context is required to render the partial view to a string");
            }
            partialView.ViewData.Model = model;
            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);
            var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);
            var view = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName).View;
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                using (var tw = new HtmlTextWriter(sw))
                {
                    view.Render(new ViewContext(controllerContext, view, partialView.ViewData, partialView.TempData, tw), tw);
                }
            }
            return sb.ToString();
        }
    }
}
