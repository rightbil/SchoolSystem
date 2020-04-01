using System.Text;
using System.Web.Mvc;
using SchoolSystem.DbContext;
using System.Collections.Generic;
using System.Linq;
using SchoolSystem.DbModels.Model;
using SchoolSystem.MVC.ViewModels;
namespace SchoolManagement.Controllers
{
    public class TestController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        [HttpGet]
        
        //TODO: RadioButton
        public ActionResult Index()
        {
            return View("RadioButton", new VMRadioButtonDepartmet());
        }
        [HttpPost]
        public string Index(VMRadioButtonDepartmet c)
        {
            if (string.IsNullOrEmpty(c.SelectedDepartment))
                return "you did not select a department";
            else
                return "you have selected : " + c.SelectedDepartment;
        }
       
        //TODO: ListBox
        [HttpGet]
        public ActionResult ListBox()
        {
            var listOfDepartments = new List<SelectListItem>();
            foreach (Department dept in db.departments)
            {
                listOfDepartments.Add(new SelectListItem
                {
                    Text = dept.DepartmentName,
                    Value = dept.DepartmentId.ToString(),
                    Selected = true
                }
                );
            }
            VMListBoxDepartment vMListBoxDepartment = new VMListBoxDepartment();
            vMListBoxDepartment.Departments = listOfDepartments;
            return View(vMListBoxDepartment);
        }
        [HttpPost]
        public string ListBox(IEnumerable<string> selectedDepartments)
        {
            if (selectedDepartments == null)
            {
                return "You did not select any department";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You Selected - " + string.Join(",", selectedDepartments));
                return sb.ToString();
            }
        }

        //TODO: MetadataType

        public ActionResult DisplayUrl()
        {
         return View(db.students.ToList());
        }



    }
}