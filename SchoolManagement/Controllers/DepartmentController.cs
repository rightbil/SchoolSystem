using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SchoolSystem.DbContext;
using SchoolSystem.MVC.Models;
namespace SchoolSystem.MVC.Models.Controllers
{
    public class DepartmentController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        public ActionResult Index(int ? page)
        {
           return View(SelectAllDepartments()
                 .ToPagedList(page??1,8));
        }
        public ActionResult Details(int id)
        {
            return View(SelectADepartment(id));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,DepartmentName,Capacity")] Department department)
        {
            var dept = new DbModels.Model.Department()
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
        public ActionResult Edit(int id)
        {
           return View(SelectADepartment(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,DepartmentName,Capacity")] Department department)
        {
            var dept = new DbModels.Model.Department()
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
        public ActionResult Delete(int id)
        {
           return View(SelectADepartment(id));
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.departments.Remove(db.departments.FirstOrDefault(x => x.DepartmentId == id));
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
        public List<Department> SelectAllDepartments()
        {
            var listOfdepts = new List<Department>();
            foreach (var d in db.departments)
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

            return listOfdepts;
        }
        public Department SelectADepartment(int id)
        {
            var depts =  db.departments.FirstOrDefault(d=>d.DepartmentId==id);

            return new Department()
            {
                DepartmentId = depts.DepartmentId,
                DepartmentName = depts.DepartmentName,
                Capacity=depts.Capacity,

            };

            
        }
    }
}
