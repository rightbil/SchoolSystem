using System.Collections.Generic;
using System.Linq;
using SchoolSystem.DbContext;

namespace SchoolSystem.MVC.ViewModels
{
    public class VMRadioButtonDepartmet
    {
        public string  SelectedDepartment { get; set; }
        public List<DbModels.Model.Department> Departments
        {
            get
            {
            var db = new SchoolDbContext();

            return db.departments.ToList();

            }

        }
    }
}