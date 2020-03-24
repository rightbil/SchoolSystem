using System;
using SchoolSystem.DbContext;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SchoolSystem.MVC.Models;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using Microsoft.Ajax.Utilities;

namespace SchoolManagement.Controllers
{
    public class TeachersController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        public ActionResult Index()
        {
       var teachers = db.teachers.Include(x => x.Department).Include(x=>x.Course).ToList();
            List<Teacher> listOfTeachers = new List<Teacher>();
            foreach (var teacher in teachers)
            {
                listOfTeachers.Add(new Teacher()
                {
                    TeacherId = teacher.TeacherId,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Major = teacher.Major,
                    HireDate = teacher.HireDate,
                    Department = teacher.Department.DepartmentName,
                    DepartmentId = teacher.Department.DepartmentId,
                    Course= teacher.Course.Title,
                    CourseId = teacher.Course.CourseId
                });
            }
            return View(listOfTeachers);
        }
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var searchedObject = db.teachers.Include(x => x.Department).Include(x=>x.Course).FirstOrDefault(x => x.TeacherId == id);

            Teacher teacher = new Teacher
            {
                TeacherId = searchedObject.TeacherId,
                FirstName = searchedObject.FirstName,
                LastName = searchedObject.LastName,
                Major = searchedObject.Major,
                HireDate = searchedObject.HireDate,
                Department= searchedObject.Department.DepartmentName,
                DepartmentId = searchedObject.DepartmentId, // searchedObject.Department.DepartmentId
                Course= searchedObject.Course.Title,
                CourseId = searchedObject.CourseId
            };
            if (teacher == null)
            {
                return HttpNotFound();
            }

            return View(teacher);
        }
        public ActionResult Create()
        {
            ViewData["AllDepartment"] = new SelectList(db.departments, "DepartmentId", "DepartmentName");
            ViewData["AllCourse"] = new SelectList(db.courses, "CourseId", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,LastName,FirstName,Major,Department, Course")] Teacher postTeacher)
        {
          
           if (ModelState.IsValid)
            {
               SchoolSystem.DbModels.Model.Teacher t = new SchoolSystem.DbModels.Model.Teacher
                {   
                    FirstName = postTeacher.FirstName,
                    LastName = postTeacher.LastName,
                    HireDate = DateTime.Now,
                    Major = postTeacher.Major,
                    DepartmentId =int.Parse(postTeacher.Department),
                    TeacherId= postTeacher.TeacherId,
                    CourseId=int.Parse(postTeacher.Course)
              };
                db.teachers.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postTeacher);
        }
    
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teacherToEdit = db.teachers.Include(x => x.Department).Include(x=>x.Course).FirstOrDefault(x => x.TeacherId == id);

            var teacher = new Teacher
            {
                TeacherId = teacherToEdit.TeacherId,
                FirstName = teacherToEdit.FirstName,
                LastName = teacherToEdit.LastName,
                Major = teacherToEdit.Major,
                HireDate = teacherToEdit.HireDate,
                DepartmentId = teacherToEdit.Department.DepartmentId
                ,
                CourseId=teacherToEdit.Course.CourseId
            };
            
            var deptList = db.departments;
            var deptChoise = new List<SelectListItem>();
            foreach (var dept in deptList)
            {
                deptChoise.Add(new SelectListItem()
                {
                    Text = dept.DepartmentName,
                    Value = dept.DepartmentId.ToString(),
                    Selected = dept.DepartmentId == teacherToEdit.Department.DepartmentId ? true : false
                });
            }
            ViewData["AllDepartment"] = deptChoise;

            var courseList = db.courses;
            var courseChoise = new List<SelectListItem>();
            foreach (var course in courseList)
            {
                courseChoise.Add(new SelectListItem()
                {
                    Text = course.Title,
                    Value = course.CourseId.ToString(),
                    Selected = course.CourseId == teacherToEdit.Course.CourseId ? true : false
                });
            }
            ViewData["AllCourse"] = courseChoise;
            //ViewData["AllDepartment"] = new SelectList(db.departments, "DepartmentId", "DeptName", 2);//teacherToEdit.Department.DepartmentId);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,LastName,FirstName,HireDate,Major,Department,Course")] Teacher teacher)
        {
           SchoolSystem.DbModels.Model.Teacher teacherToEdit = new SchoolSystem.DbModels.Model.Teacher
            {   TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Major = teacher.Major,
                HireDate = teacher.HireDate,
                DepartmentId = int.Parse(teacher.Department),
                CourseId = int.Parse(teacher.Course)
              
            };
           
            if (ModelState.IsValid)
            {
                db.Entry(teacherToEdit).State = EntityState.Modified;
           db.SaveChanges();
           return RedirectToAction("Index");
            }
            return View(teacher);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var teacher = db.teachers.Include(x => x.Department).Include(x=>x.Course).FirstOrDefault(x => x.TeacherId == id);
            var  teacherToDelete = new Teacher
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Major = teacher.Major,
                HireDate = teacher.HireDate,
                DepartmentId = teacher.Department.DepartmentId
                ,
                CourseId = teacher.Course.CourseId
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
            var teacherConfirmDelete = db.teachers.Include(y=>y.Department).FirstOrDefault(x => x.TeacherId == id);

            db.teachers.Remove(teacherConfirmDelete);
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


        public string findByLastName( string searchLastName)
        {
            
           var teachers = db.teachers.Include(x=>x.Department).Include(x=>x.Course).Where(x=>x.FirstName.Contains(searchLastName)).ToList();
            List<Teacher> listOfTeachers = new List<Teacher>();
            foreach (var teacher in teachers)
            {
                listOfTeachers.Add(new Teacher()
                {
                    TeacherId = teacher.TeacherId,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Major = teacher.Major,
                    HireDate = teacher.HireDate,
                    Department = teacher.Department.DepartmentName,
                    DepartmentId = teacher.Department.DepartmentId,
                    Course=teacher.Course.Title,
                    CourseId = teacher.Course.CourseId
                });
            }

            return PartialView("PartialList").RenderToString(HttpContext,listOfTeachers);
        }
      
    }
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
