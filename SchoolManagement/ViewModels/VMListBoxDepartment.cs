using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolSystem.DbModels.Model;

namespace SchoolSystem.MVC.ViewModels
{
    public class VMListBoxDepartment
    {
        public IEnumerable<SelectListItem> Departments { get; set; }
        public IEnumerable<string> selectedDepartments  { get; set; }
    }
}