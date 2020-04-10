using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using SchoolSystem.ViewModels;
using SchoolSystem.DbContext;
using SchoolSystem.DbModels.Model;
using PagedList;
using PagedList.Mvc;

namespace SchoolSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        public List<VMStudentEnrolledCourses> SearchStudentByName(string studentName)
        {
            List<VMStudentEnrolledCourses> listOfStudents = new List<VMStudentEnrolledCourses>();

            foreach (var e in db.enrollments.Include(x => x.Student).Include(x => x.Course).Where(x => x.Student.FirstName.StartsWith(studentName) || x.Student.LastName.StartsWith(studentName)).ToList())
            {

                listOfStudents.Add(

                    new VMStudentEnrolledCourses()
                    {
                        StudentId = e.Student.StudentId,
                        FullName = e.Student.FirstName + " " + e.Student.LastName,
                        DateOfBirth = e.Student.DateOfBirth,
                        Courses = e.Course.Title

                    });
            }

            return listOfStudents;

        }
        public List<VMStudentEnrolledCourses> SearchStudentByDateOfBirth(string studentDateOfBirth)
        {
            DateTime localDateTime = DateTime.Parse(studentDateOfBirth);
            List<VMStudentEnrolledCourses> listOfStudents = new List<VMStudentEnrolledCourses>();
            //Mule
            if (ConvertStringToDate(studentDateOfBirth))
            {
                foreach (var e in db.enrollments.Include(x => x.Student).Include(x => x.Course).Where(x=> DateTime.Compare(x.Student.DateOfBirth, localDateTime)==1).ToList())
                {
                    listOfStudents.Add(
                        new VMStudentEnrolledCourses()
                        {
                            StudentId = e.Student.StudentId,
                            FullName = e.Student.FirstName + " " + e.Student.LastName,
                            DateOfBirth = e.Student.DateOfBirth,
                            Courses = e.Course.Title
                        });
                }
                return listOfStudents;
            }
            else
            {
                return listOfStudents;
            }
        }
        public bool ConvertStringToDate(string stringToDate)
        {
            DateTime localStudentDataOfBirth = DateTime.Parse(stringToDate);
            string[] validFormats = { "MM/dd/yyyy", "yyyy/MM/dd", "MM/dd/yyyy hh:mm tt" };
            CultureInfo provider = new CultureInfo("en-US");
            try
            {
                DateTime date = DateTime.ParseExact(stringToDate, "yyyy/MM/dd", provider);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<VMStudentEnrolledCourses> SearchCourseByTitle(string courseTitle)
        {
            var listOfCourses = new List<VMStudentEnrolledCourses>();

            foreach (var e in db.enrollments.Include(x => x.Student).Include(x => x.Course)
                .Where(x => x.Course.Title.Contains(courseTitle)).ToList())
            {

                listOfCourses.Add(

                    new VMStudentEnrolledCourses()
                    {
                        StudentId = e.Student.StudentId,
                        FullName = e.Student.FirstName + " " + e.Student.LastName,
                        DateOfBirth = e.Student.DateOfBirth,
                        Courses = e.Course.Title

                    });
            }

            return listOfCourses;
        }
        public ActionResult Index(string searchBy, string search, int? page)
        {
            var listOfCoursesByStudent = new List<VMStudentEnrolledCourses>();
            if (searchBy == "dateofbirth")
            {
                return View(SearchStudentByDateOfBirth(search).ToPagedList(page?? 1, 2));
            }
            else if (searchBy == "student")
            {
                return View(SearchStudentByName(search).ToPagedList(page ?? 1, 2));
            }
            else
            {
                return View(SearchCourseByTitle(search).ToPagedList(page ?? 1, 2));
            }
        }
        [HttpGet]
        public ActionResult Enrollment()
        {
            List<SelectListItem> listOfStudents = new List<SelectListItem>();
            foreach (var s in db.students)
            {
                listOfStudents.Add(
                 new SelectListItem
                 {
                     Text = string.Join(" ", new { s.StudentId, s.FirstName, s.LastName }),
                     Value = s.StudentId.ToString()
                 }
                 );
            }
            ViewBag.students = listOfStudents;
            ViewBag.courses = new SelectList(db.courses.OrderBy(x => x.Title), "CourseId", "Title");
            return View();
        }
        [HttpPost]
        public ActionResult Enrollment(string studentId, string courseId)
        {
            //try
            //{
            Enrollment enrollment = new Enrollment()
            {
                StudentID = int.Parse(studentId),
                CourseID = int.Parse(courseId)
            };
            db.enrollments.Add(enrollment);
            db.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ToString();
            //}

            return RedirectToAction("Enrollment");
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
