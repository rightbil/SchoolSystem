using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using SchoolSystem.MVC.Models;
using SchoolSystem.DbContext;


namespace SchoolSystem.Controllers
{
    public class CourseController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        public List<Course> SelectAllCourses()
        {
            var listOfStudents = new List<Course>();
            foreach (var c in db.courses.ToList())
            {
                listOfStudents.Add(new Course()
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Credit = c.Credit,
                    Price = c.Price

                }
                );

            }
            return listOfStudents;
        }
        public Course SelectACourse(int? id)
        {
            var c = db.courses.FirstOrDefault(x => x.CourseId == id);

            return new Course()
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Credit = c.Credit,
                Price = c.Price


            };

        }
        public ActionResult Index()
        {
           return View(SelectAllCourses());
         }
  
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = SelectACourse(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
 
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Price,Credit")] Course course)
        {

            SchoolSystem.DbModels.Model.Course courseToEdit = new SchoolSystem.DbModels.Model.Course
            {
                CourseId = course.CourseId,
                Title=course.Title,
                Credit = course.Credit,
                Price = course.Price

            };

            if (ModelState.IsValid)
            {
                db.courses.Add(courseToEdit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseToEdit);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = SelectACourse(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
      [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,Title,Credit,Price")] Course course)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = SelectACourse(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolSystem.DbModels.Model.Course  course= db.courses.FirstOrDefault(x=>x.CourseId==id);
            db.courses.Remove(course);
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
    }
}
