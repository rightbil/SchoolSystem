using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.ViewModels
{
    public class CourseByDepartment
    {
        public string DepartmentName { get; set; }
        public string Title { get; set; }
        public int Credit { get; set; }
        public double Price { get; set; }
    }
}