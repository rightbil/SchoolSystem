using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using SchoolSystem.MVC.Models;
using SchoolSystem.DbContext;
using SchoolSystem.Exceptions;
using SchoolSystem.Customize;
namespace SchoolSystem.Controllers
{
    public class CourseController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        public ActionResult SummaryList()
        {

            return View();
        }

        public PartialViewResult All()
        {
            var allCourses = SelectAllCourses();
            return PartialView("PartialViewCourse", allCourses);
        }
        public PartialViewResult TopThreeCourses()
        {
            var topCourses = SelectAllCourses().OrderByDescending(x=>x.Price).Take(3).ToList();
            return PartialView("PartialViewCourse", topCourses);
        }
        public PartialViewResult BottomThreeCourses()
        {
            var bottomCourses = SelectAllCourses().OrderBy(x => x.Price).Take(3).ToList();

            return PartialView("PartialViewCourse", bottomCourses);
        }
        //[OutputCache(Duration = 20)] [ChildActionOnly] // this works as expected
        //[OutputCache(CacheProfile = "1MinuteCache")] [ChildActionOnly] // assume "1MinuteCache" is added in web config this don't work so it needs customization
        // why attribute is not there I think it is stadared Mule
        [SsPartialCache("1MinuteCache")] // this is the custom cache
        public ActionResult Index(string sortBy)
        {
            var listOfCourses = SelectAllCourses().AsQueryable();
            ViewBag.SortByTitle = string.IsNullOrEmpty(sortBy) ? "Title desc" : "";
            ViewBag.SortByCredit = sortBy == "Credit" ? "Credit Desc" : "Credit";
            switch (sortBy)
            {
                case "Title desc":
                    listOfCourses = listOfCourses.OrderByDescending(x => x.Title);
                    break;
                case "Credit Desc":
                    listOfCourses = listOfCourses.OrderByDescending(x => x.Credit);
                    break;
                case "Credit":
                    listOfCourses = listOfCourses.OrderBy(b => b.Credit);
                    break;
                default:
                    listOfCourses = listOfCourses.OrderBy(a => a.Title);
                    break;
            }
            return View(listOfCourses);
        }
        [HttpGet]
        public ActionResult Index2()
        {
            return View(SelectAllCourses().ToList());
        }
        //TODO: Mule
        public ActionResult Delete(IEnumerable<int> courseIdToDelete)
        {
            int localCourseIdToDlete = 1;
            
            db.courses.RemoveRange(db.courses.Where(x => courseIdToDelete.Contains(x.CourseId)).ToList());
            db.SaveChanges();
            return RedirectToAction("Index");
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
        // the ff method will work as far as the js is enabled at the client.
        // it validated by sending anonymous AJAX call to the server. 
        public JsonResult IsTitleExist(string title)
        {
            return Json(!db.courses.Any(x => x.Title == title), JsonRequestBehavior.AllowGet);
            // (!db.courses.Any(x => title.Equals(x.Title, StringComparison.OrdinalIgnoreCase))
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Price,Credit")] Course course)
        {
            // #90 Server Side Validation 
            // the below code was disabled after the RemoteClient attribute was added.
            /*if (db.courses.Any(x => x.Title == course.Title))
            {
                ModelState.AddModelError("Title","Title is already in use");
            }*/
            try
            {
                ValidateCourseTitle(course.Title);
            }
            catch (InvalidNameException ex)
            {
                Response.Write(ex.Message);
                throw;
            }

            DbModels.Model.Course courseToEdit = new DbModels.Model.Course
            {
                CourseId = course.CourseId,
                Title = course.Title,
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
        [HttpPost, ValidateAntiForgeryToken]
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
        [HttpPost]
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
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DbModels.Model.Course course = db.courses.FirstOrDefault(x => x.CourseId == id);
            db.courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //TODO: Mule
        public static void ValidateCourseTitle(string name)
        {
            Regex regex = new Regex("^[a-zA-Z]+ $");
            if (regex.IsMatch(name))
                throw new InvalidNameException(name);
        }
        public List<Course> SelectAllCourses()
        {
            var listOfCourses = new List<Course>();
            foreach (var course in db.courses.ToList())
            {
                listOfCourses.Add(new Course()
                    {
                        CourseId = course.CourseId,
                        Title = course.Title,
                        Credit = course.Credit,
                        Price = course.Price
                    }
                );
            }
            return listOfCourses;
        }
        public Course SelectACourse(int? id)
        {
            var course = db.courses.FirstOrDefault(x => x.CourseId == id);
            return new Course()
            {
                CourseId = course.CourseId,
                Title = course.Title,
                Credit = course.Credit,
                Price = course.Price
            };
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
