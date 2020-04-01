using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Mvc;
using SchoolSystem.MVC.Models;
using SchoolSystem.DbContext;
using SchoolSystem.Utility;
namespace SchoolSystem.Controllers
{
    public class StudentController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        private EmailSender sender = new EmailSender();
        public List<Student> SelectAllStudents()
        {
            var listOfStudents = new List<Student>();
            foreach (var student in db.students.Include(x => x.Department).ToList())
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
                    //RegisteredOn = student.RegisteredOn,
                    DepartmentId = student.DepartmentId,
                    Department = student.Department.DepartmentName
                }
                );
            }
            return listOfStudents;

        }

        public Student SelectAStudent(int? id)
        {
            var stdntFromDb = db.students.Include(x => x.Department).FirstOrDefault(x => x.StudentId == id);

            return new Student()
            {
                StudentId = stdntFromDb.StudentId,
                FirstName = stdntFromDb.FirstName,
                LastName = stdntFromDb.LastName,
                EmailAddress = stdntFromDb.EmailAddress,
                Password = stdntFromDb.Password,
                PhoneNumber = stdntFromDb.PhoneNumber,
                DateOfBirth = stdntFromDb.DateOfBirth,
                Postalcode = stdntFromDb.Postalcode,
                Comment = stdntFromDb.Comment,
                Url = stdntFromDb.Url,
               // RegisteredOn = stdntFromDb.RegisteredOn,
                DepartmentId = stdntFromDb.Department.DepartmentId,
                Department = stdntFromDb.Department.DepartmentName
            };
        }
        public ActionResult Index()
        {
            return View(SelectAllStudents().OrderBy(x => x.LastName).ThenBy(x=>x.LastName));
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = SelectAStudent(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult Create()
        {
            ViewData["Departments"] = new SelectList(db.departments, "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        /*
        public ActionResult Create(FormCollection frmCollection)
        {
            foreach (var key in frmCollection.AllKeys)
            {
                Response.Write( key + " " + key[]);
            }
            */

        public ActionResult Create([Bind(Include = "StudentId, LastName,FirstName,EmailAddress,ConfirmEmailAddress, Password, ConfirmPassword, PhoneNumber,DateOfBirth, PostalCode,Comment, Url,DepartmentId")] Student student) //, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                SchoolSystem.DbModels.Model.Student studentToSave = new SchoolSystem.DbModels.Model.Student()
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
                /*

            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                    Server.MapPath("~/images/profile"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }*/

                db.students.Add(studentToSave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);

        }

        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stdntFromDb = db.students.Include(x => x.Department).FirstOrDefault(x => x.StudentId == id);
            var student = new Student
            {
                StudentId = stdntFromDb.StudentId,
                FirstName = stdntFromDb.FirstName,
                LastName = stdntFromDb.LastName,
                EmailAddress = stdntFromDb.EmailAddress,
                Password = stdntFromDb.Password,
                PhoneNumber = stdntFromDb.PhoneNumber,
                DateOfBirth = stdntFromDb.DateOfBirth,
                Postalcode = stdntFromDb.Postalcode,
                Comment = stdntFromDb.Comment,
                Url = stdntFromDb.Url,
                DepartmentId = stdntFromDb.Department.DepartmentId,
            };

            var deptChoise = new List<SelectListItem>();
            foreach (var dept in db.departments)
            {
                deptChoise.Add(new SelectListItem()
                {
                    Text = dept.DepartmentName,
                    Value = dept.DepartmentId.ToString(),
                    Selected = dept.DepartmentId == stdntFromDb.Department.DepartmentId ? true : false
                });
            }
            //Console.WriteLine("testing values");
            ViewData["Departments"] = deptChoise;

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,LastName,FirstName,EmailAddress,ConfirmEmailAddress,Password ,ConfirmPassword, PhoneNumber,DateOfBirth,Postalcode,Comment,ImageUrl,RegisteredOn,Department")] Student student)
        {
            SchoolSystem.DbModels.Model.Student studentToEdit = new SchoolSystem.DbModels.Model.Student
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                EmailAddress = student.EmailAddress,
                Password = student.Password,
                PhoneNumber = student.PhoneNumber,
                DateOfBirth =student.DateOfBirth,
                Postalcode = student.Postalcode,
                Comment = student.Comment,
                Url = student.Url,
                RegisteredOn = student.RegisteredOn,
                DepartmentId = int.Parse(student.Department)
            };

            if (ModelState.IsValid)
            {
                db.Entry(studentToEdit).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = SelectAStudent(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolSystem.DbModels.Model.Student student = db.students.Find(id);
            db.students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
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
                .Select( y=> new StudentByDepartment
            {
                Department = y.Key,
                Total = y.Count()

            }).ToList().OrderByDescending(z=>z.Total);

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
