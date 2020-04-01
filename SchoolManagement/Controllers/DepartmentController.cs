using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SchoolSystem.DbContext;
using SchoolSystem.MVC.Models;
namespace SchoolSystem.MVC.Models.Controllers
{
    public class DepartmentController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        public ActionResult Index()
        {
            var fromDb = db.departments.OrderBy(x => x.DepartmentName).ToList();
            List<Department> listOfdepts = new List<Department>();
            foreach (var d in fromDb)
            {
                listOfdepts.Add(

                    new Department()
                    {
                         DepartmentId = d.DepartmentId,
                        DepartmentName = d.DepartmentName,
                        Capacity = d.Capacity

                    }
                    );
            }

            return View(listOfdepts);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fromDb = db.departments.FirstOrDefault(x => x.DepartmentId == id);

            Department department = new Department()
            {
                DepartmentId = fromDb.DepartmentId,
                DepartmentName = fromDb.DepartmentName,
                Capacity = fromDb.Capacity

            };
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,DepartmentName,Capacity")] Department department)
        {
            SchoolSystem.DbModels.Model.Department dept = new SchoolSystem.DbModels.Model.Department()
            {
                DepartmentName = department.DepartmentName,
                Capacity = department.Capacity
            };

            if (ModelState.IsValid)
            {
                db.departments.Add(dept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fromDb = db.departments.FirstOrDefault(x => x.DepartmentId == id);

            Department dept = new Department()
            {
                DepartmentId = fromDb.DepartmentId,
                DepartmentName = fromDb.DepartmentName,
                Capacity = fromDb.Capacity
            };

            if (dept == null)
            {
                return HttpNotFound();
            }
            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,DepartmentName,Capacity")] Department department)



        {
            SchoolSystem.DbModels.Model.Department dept = new SchoolSystem.DbModels.Model.Department()
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                Capacity = department.Capacity

            };
            
            if (ModelState.IsValid)
            {
                db.Entry(dept).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fromDb = db.departments.Find(id);
           Department department = new Department()
            {
                DepartmentId =  fromDb.DepartmentId,
                DepartmentName = fromDb.DepartmentName,
                Capacity = fromDb.Capacity
            };
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var fromDb = db.departments.FirstOrDefault(x=>x.DepartmentId==id);
            db.departments.Remove(fromDb);
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

        public ActionResult ListAllDepartments()
        {
            var depts = db.departments.OrderByDescending(x=>x.Capacity).ToList();

            return View(depts);
        }
    }
}
