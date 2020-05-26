using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using PagedList;
using SchoolSystem.MVC.Models;
using SchoolSystem.DbContext;
using SchoolSystem.Utility;
namespace SchoolSystem.Controllers
{
    public class StudentController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        private EmailSender sender = new EmailSender();

        public JsonResult TestJson()
        {
            var st = new Student();
            st.StudentId =1;
            st.FirstName = "Test";
            st.LastName = "Test";
            st.EmailAddress = "Test";
            st.Password = "Test";
            st.PhoneNumber = "Test";
            st.DateOfBirth = Convert.ToDateTime("2020-02-02");
            st.Postalcode = "20901";
            st.Comment = "IComment";
            st.Url = "";
            st.DepartmentId = 1;
            st.Department = "Geography";

            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(st);
            return Json("",JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(int? page)
        {
            Session["students"] = SelectAllStudents().ToPagedList(page ?? 1, 8);
            return View(Session["students"]);
        }
        public ActionResult Details(int? id)
        {
            var list = Session["Student"] as List<Student>;
            var student = list.Find(x => x.StudentId == id);
         
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        public ActionResult Create()
        {
            var Zipcode = from Zipcode z in Enum.GetValues(typeof(Zipcode))
                          select new
                          {
                              ID = (int)z,
                              Name = z.ToString(),
                          };

            ViewData["Zipcode"] = new SelectList(Zipcode, "ID", "Name");
            ViewData["Departments"] = new SelectList(db.departments, "DepartmentId", "DepartmentName");
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,LastName,FirstName,EmailAddress,ConfirmEmailAddress,Password,ConfirmPassword,PhoneNumber,DateOfBirth,PostalCode,Comment,Url,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                var studentToSave = new SchoolSystem.DbModels.Model.Student()
                {
                    StudentId = student.StudentId,
                    LastName = student.LastName,
                    FirstName = student.FirstName,
                    EmailAddress = student.EmailAddress,
                    Password = student.Password,
                    PhoneNumber = student.PhoneNumber,
                    DateOfBirth = student.DateOfBirth,
                    Postalcode = student.Postalcode,
                    Comment = student.Comment,
                    Url = student.Url,
                    DepartmentId = student.DepartmentId
                };
                db.students.Add(studentToSave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);

        }
        public ActionResult Edit(int id)
        {
            var record = db.students.Include(x => x.Department).FirstOrDefault(x => x.StudentId == id);
            var student = new Student
            {
                StudentId = record.StudentId,
                FirstName = record.FirstName,
                LastName = record.LastName,
                EmailAddress = record.EmailAddress,
                Password = record.Password,
                PhoneNumber = record.PhoneNumber,
                DateOfBirth = record.DateOfBirth,
                Postalcode = record.Postalcode,
                Comment = record.Comment,
                Url = record.Url,
                DepartmentId = record.Department.DepartmentId
            };
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
            ViewData["Departments"] = deptChoise;
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                var studentToEdit = new DbModels.Model.Student
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    EmailAddress = student.EmailAddress,
                    Password = student.Password,
                    PhoneNumber = student.PhoneNumber,
                    DateOfBirth = student.DateOfBirth,
                    Postalcode = student.Postalcode,
                    Comment = student.Comment,
                    Url = student.Url,
                    DepartmentId = int.Parse(student.Department)
                };

                db.Entry(studentToEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }
        public ActionResult Delete(int id)
        {
            var list = Session["Student"] as List<Student>;
            var student = list.Find(x => x.StudentId == id);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = db.students.Find(id);
            db.students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public List<Student> SelectAllStudents()
        {
            var listOfStudents = new List<Student>();
            foreach (var student in db.students.Include(x => x.Department))
            {
                listOfStudents.Add(new Student()
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    EmailAddress = student.EmailAddress,
                    Password = student.Password,
                    PhoneNumber = student.PhoneNumber,
                    DateOfBirth = student.DateOfBirth,
                    Postalcode = student.Postalcode,
                    Comment = student.Comment,
                    Url = student.Url,
                    DepartmentId = student.DepartmentId,
                    Department = student.Department.DepartmentName
                }
                );
            }
            // Has used the Compare implemented 
            listOfStudents.Sort();
            return listOfStudents;
        }
        public Student SelectAStudent(int? id)
        {
            /*var record = db.students.Include(x => x.Department).FirstOrDefault(x => x.StudentId == id);
            return new Student()
            {
                StudentId = record.StudentId,
                FirstName = record.FirstName,
                LastName = record.LastName,
                EmailAddress = record.EmailAddress,
                Password = record.Password,
                PhoneNumber = record.PhoneNumber,
                DateOfBirth = record.DateOfBirth,
                Postalcode = record.Postalcode,
                Comment = record.Comment,
                Url = record.Url,
                // RegisteredOn = stdntFromDb.RegisteredOn,
                DepartmentId = record.Department.DepartmentId,
                Department = record.Department.DepartmentName
            };*/
            var list = Session["Student"] as List<Student>;
         return list.Find(x => x.StudentId == id);
        }
        public ActionResult ListOfStudents(int id)
        {
            //var studentList = db.students.SqlQuery("Select * from tblStudent").ToList();
            /*var student = db.students
                              .SqlQuery("SELECT [First Name],[Last Name], [Department Name] FROM tblStudent INNER JOIN tblDepartment ON[DtudentId=@id", new SqlParameter("@id", 1))
                              .FirstOrDefault();*/
            // db.Database.ExecuteSqlCommand()
            //Get student name of string type
            //string firstName = db.Database.SqlQuery<string>("Select [First Name] from tblStudent where StudentId=1").FirstOrDefault();
            //string lastName = db.Database.SqlQuery<string>("Select [Last Name] from tblStudent where StudentId=@id", new SqlParameter("@id", 1)).FirstOrDefault();
            return View();
        }
        public ActionResult StudentCountByDepartment()
        {
            var studentCount = db.students.GroupBy(x => x.Department.DepartmentName)
                   .Select(y => new StudentByDepartment
                   {
                       Department = y.Key,
                       Total = y.Count()

                   }).ToList().OrderByDescending(z => z.Total);

            return View(studentCount);
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
