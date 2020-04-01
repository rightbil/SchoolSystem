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

namespace SchoolSystem.MVC.Models.Controllers
{
    public class InstructorController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        public int PageSize = 4;
        public List<Instructor> SelectAllTeachers()
        {
            var teachers = db.teachers.Include(x => x.Department).Include(x => x.Course).ToList();
            List<Instructor> listOfTeachers = new List<Instructor>();
            foreach (var teacher in teachers)
            {
                listOfTeachers.Add(new Instructor()
                {
                    TeacherId = teacher.InstructorId,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Major = teacher.Major,
                    HireDate = teacher.HireDate,
                    Department = teacher.Department.DepartmentName,
                    DepartmentId = teacher.Department.DepartmentId,
                    Course = teacher.Course.Title,
                    CourseId = teacher.Course.CourseId
                });
            }

            return listOfTeachers;
        }
        public Instructor SelectATeacher(int id)
        {
            var searchedObject = db.teachers.Include(x => x.Department).Include(x => x.Course).FirstOrDefault(x => x.InstructorId == id);
            return new Instructor()
            {

                TeacherId = searchedObject.InstructorId,
                FirstName = searchedObject.FirstName,
                LastName = searchedObject.LastName,
                Major = searchedObject.Major,
                HireDate = searchedObject.HireDate,
                Department = searchedObject.Department.DepartmentName,
                DepartmentId = searchedObject.DepartmentId,
                Course = searchedObject.Course.Title,
                CourseId = searchedObject.CourseId


            };
        }
        public ActionResult Index(int page= 1)
        {
            return View(SelectAllTeachers()
                                .OrderBy(p=>p.TeacherId)
                                .Skip((page-1) * PageSize).Take(PageSize));
        }
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = SelectATeacher(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }

            return View(instructor);
        }
        public ActionResult Create()
        {
            ViewData["AllDepartment"] = new SelectList(db.departments, "DepartmentId", "DepartmentName");
            ViewData["AllCourse"] = new SelectList(db.courses, "CourseId", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,LastName,FirstName,Major,Department, Course")] Instructor postInstructor)
        {
            if (ModelState.IsValid)
            {
                SchoolSystem.DbModels.Model.Instructor t = new SchoolSystem.DbModels.Model.Instructor
                {
                    FirstName = postInstructor.FirstName,
                    LastName = postInstructor.LastName,
                    HireDate = DateTime.Now,
                    Major = postInstructor.Major,
                    DepartmentId = int.Parse(postInstructor.Department),
                    InstructorId = postInstructor.TeacherId,
                    CourseId = int.Parse(postInstructor.Course)
                };
                db.teachers.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postInstructor);
        }
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teacherToEdit = db.teachers.Include(x => x.Department).Include(x => x.Course).FirstOrDefault(x => x.InstructorId == id);

            var teacher = new Instructor
            {
                TeacherId = teacherToEdit.InstructorId,
                FirstName = teacherToEdit.FirstName,
                LastName = teacherToEdit.LastName,
                Major = teacherToEdit.Major,
                HireDate = teacherToEdit.HireDate,
                DepartmentId = teacherToEdit.Department.DepartmentId,
                CourseId = teacherToEdit.Course.CourseId
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
        public ActionResult Edit([Bind(Include = "TeacherId,LastName,FirstName,HireDate,Major,Department,Course")] Instructor instructor)
        {
            SchoolSystem.DbModels.Model.Instructor instructorToEdit = new SchoolSystem.DbModels.Model.Instructor
            {
                InstructorId = instructor.TeacherId,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Major = instructor.Major,
                HireDate = instructor.HireDate,
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
            var teacher = db.teachers.Include(x => x.Department).Include(x => x.Course).FirstOrDefault(x => x.InstructorId == id);
            var teacherToDelete = new Instructor
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Major = teacher.Major,
                HireDate = teacher.HireDate,
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
            var teacherConfirmDelete = db.teachers.Include(y => y.Department).FirstOrDefault(x => x.InstructorId == id);

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


        public string findByLastName(string searchLastName)
        {

            var teachers = db.teachers.Include(x => x.Department).Include(x => x.Course).Where(x => x.FirstName.ToUpper().Contains(searchLastName.ToUpper())).ToList();
            List<Instructor> listOfTeachers = new List<Instructor>();
            foreach (var teacher in teachers)
            {
                listOfTeachers.Add(new Instructor()
                {
                    TeacherId = teacher.InstructorId,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Major = teacher.Major,
                    HireDate = teacher.HireDate,
                    Department = teacher.Department.DepartmentName,
                    DepartmentId = teacher.Department.DepartmentId,
                    Course = teacher.Course.Title,
                    CourseId = teacher.Course.CourseId
                });
            }

            return PartialView("PartialList").RenderToString(HttpContext, listOfTeachers);
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
