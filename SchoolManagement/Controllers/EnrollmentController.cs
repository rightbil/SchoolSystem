using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web.Mvc;
using SchoolSystem.ViewModels;
using SchoolSystem.DbContext;
using SchoolSystem.DbModels.Model;
using SchoolSystem.MVC.Models;
using PagedList;
namespace SchoolSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly SchoolDbContext db = new SchoolDbContext();
        public ActionResult Index(string searchBy, string search, int? page)
        {
            var result = new List<VMStudentEnrolledCourses>().ToPagedList(page ?? 1, 4);
            switch (searchBy.ToLower())
            {
                case "dateofbirth":
                    result = SearchStudentByDateOfBirth(search).ToPagedList(page ?? 1, 4);
                    break;
                case "student":
                    result = SearchStudentByName(search).ToPagedList(page ?? 1, 4);
                    break;
                default:
                    result = SearchCourseByTitle(search).ToPagedList(page ?? 1, 4);
                    break;
            }
            return View(result);
        }
        [HttpGet]
        public ActionResult Enrollment(int id /*department id*/)
        {
            var listOfStudents = new List<SelectListItem>();
            foreach (var s in db.students)
            {
                listOfStudents.Add(
                 new SelectListItem
                 {   
                     Text = string.Join(" ", new { s.StudentId, s.LastName }),
                     Value = s.StudentId.ToString()
                 }
                 );
            }
            ViewBag.students = listOfStudents;
            ViewBag.courses = new SelectList(db.courses.OrderBy(x => x.Title),"CourseId", "Title");
            // use stuent id and get his department and courses belong to the department
            


            return View();
        }
        [HttpPost]
        public ActionResult Enrollment(string studentId, string courseId)
        {
            var enrollment = new Enrollment()
            {
                StudentID = int.Parse(studentId),
                CourseID = int.Parse(courseId)
            };
            db.enrollments.Add(enrollment);
            db.SaveChanges();
            return RedirectToAction("Enrollment");
        }
        public List<VMStudentEnrolledCourses> SearchStudentByName(string studentName)
        {
            var listOfStudents = new List<VMStudentEnrolledCourses>();

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
            var listOfStudents = new List<VMStudentEnrolledCourses>();
            if (ConvertStringToDate(studentDateOfBirth))
            {
                foreach (var e in db.enrollments.Include(x => x.Student).Include(x => x.Course).Where(x => DateTime.Compare(x.Student.DateOfBirth, localDateTime) == 1).ToList())
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
