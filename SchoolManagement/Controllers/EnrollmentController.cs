using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SchoolSystem.DbContext;



namespace SchoolSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        public ActionResult Enrollment()
        {
           // var students = db.students.ToList().DistinctBy(x => x.StudentId).ToList();
           // var courses = db.courses.DistinctBy(x => x.CourseId).ToList();

           List<SelectListItem> listOfStudents = new List<SelectListItem>();
            foreach (var s in db.students)
            {

                Response.Write(s.StudentId.ToString());
                listOfStudents.Add(
                new SelectListItem
                {
                    Text = string.Join(" ",new {s.StudentId,s.FirstName, s.LastName}),
                    Value = s.StudentId.ToString()
                    //,Selected = db.departments.FirstOrDefault().DepartmentName)?null : true :false;
                }
                );
            }
            ViewBag.students = listOfStudents;
            ViewBag.courses =  new SelectList(db.courses.OrderBy(x=>x.Title), "CourseId", "Title");
          //ViewBag.students = new SelectList(db.students,"StudentId", "LastName" + " FirstName");
            return View();
        }
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.courses, "CourseID", "Name");
            ViewBag.StudentID = new SelectList(db.students, "ID", "LastName");
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Enrollment");
            }

            ViewBag.CourseID = new SelectList(db.courses, "CourseID", "Name", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.students, "ID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.courses, "CourseID", "Name", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.students, "ID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.courses, "CourseID", "Name", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.students, "ID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }
        [HttpPost, ActionName("Delete"),ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.enrollments.Find(id);
            db.enrollments.Remove(enrollment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/
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
