using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using PagedList;
using SchoolManagement.ViewModels;
using SchoolSystem.MVC.Models;
using SchoolSystem.DbContext;
using SchoolSystem.Exceptions;
using SchoolSystem.Customize;
using SchoolSystem.MVC.Models.Controllers;

namespace SchoolSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly SchoolDbContext db = new SchoolDbContext();
        //TODO: it is refreshing whole page
        /// <summary>
        /// uses ajax to update the page for all , top3 and bottom3
        /// 
        /// </summary>
        public ActionResult Summary()
        {
            return View();
        }
        /// <summary>
        /// the following methods are used in the Summary action above
        /// </summary>
        /// <returns></returns>
        public PartialViewResult All()
        {
            var allCourses = SelectAllCourses();
            return PartialView("PartialViewCourse", allCourses);
        }
        public PartialViewResult TopThreeCourses()
        {
            var topCourses = SelectAllCourses().OrderByDescending(x => x.Price).Take(3).ToList();
            return PartialView("PartialViewCourse", topCourses);
        }
        public PartialViewResult BottomThreeCourses()
        {
            var bottomCourses = SelectAllCourses().OrderBy(x => x.Price).Take(3).ToList();

            return PartialView("PartialViewCourse", bottomCourses);
        }
        //TODO:Multiple Delete needs fix
        /// <summary>
        /// sort by title or price - multiple select and delete
        /// </summary>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string sortBy, int? page)
        {
            // return View(SelectAllCourses().ToList());
            /*var listOfCourse = db.courses.Join(db.departments, c => c.CourseId, d => d.DepartmentId,(c, d) => new { Department = d.DepartmentName,Title = c.Title,Credit = c.Credit,Price = c.Price});*/
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
            return View(listOfCourses.ToPagedList(page ?? 1, 10));
        }
        //TODO: individual and all select shall work properly
        /// <summary>
        /// multiple delete - individual and all select shall work properly
        /// </summary>
        /// <param name="courseIdToDelete"></param>
        /// <returns></returns>
        public ActionResult MultipleDelete(IEnumerable<int> courseIdToDelete)
        {
            db.courses.RemoveRange(db.courses.Where(x => courseIdToDelete.Contains(x.CourseId)).ToList());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //TODO:DONE
        public ActionResult Details(int id)
        {
            Course course = SelectACourse(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        //TODO:DONE
        [ActionName("AddCourse")]
        public ActionResult Create()
        {
            return View("Create");
        }
        //TODO:DONE
        [ActionName("AddCourse")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Price,Credit")] Course course)
        {
            // #90 Server Side Validation 
            // the below code was disabled after the RemoteClient attribute was added.
            /*if (db.courses.Any(x => x.Title == course.Title)){ ModelState.AddModelError("Title","Title is already in use");}*/
            var courseToEdit = new DbModels.Model.Course
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
            return View("Create", courseToEdit);
        }
        //TODO:DONE
        public ActionResult Edit(int id)
        {
            Course course = SelectACourse(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(/*Exclude = "Title",*/ Include = "CourseId,Title,Credit,Price")] SchoolSystem.DbModels.Model.Course course)
        {

            //TODO: bug code was added to handle title while editing
            /*string currentTitle=String.Empty;
           foreach (var v in db.courses.Where(x => x.CourseId == course.CourseId)){currentTitle = v.Title;}
           var c= new Course(){
               CourseId = course.CourseId,
               Title= currentTitle,
               Credit = course.Credit,
               Price = course.Price
           };*/
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please check fields for errors");
            return View(course);
        }
        //TODO:DONE
        public ActionResult Delete(int id)
        {
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
            var course = db.courses.FirstOrDefault(x => x.CourseId == id);
            db.courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //TODO:DONE
        /// <summary>
        /// List of all courses from the database
        /// </summary>
        /// <returns>List<Course>CourseId,Title,Credit , Price </Course></returns>
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
        //TODO:DONE
        /// <summary>
        /// Return a Course Object matching the id passed
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A course Object</returns>
        public Course SelectACourse(int id)
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
        //TODO:DONE
        /// <summary>
        /// the ff method will work as far as the js is enabled at the client.validated by sending anonymous AJAX call to the server. 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public JsonResult IsTitleExist(string title)
        {
            return Json(!db.courses.Any(x => x.Title == title), JsonRequestBehavior.AllowGet);
            // (!db.courses.Any(x => title.Equals(x.Title, StringComparison.OrdinalIgnoreCase))
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
