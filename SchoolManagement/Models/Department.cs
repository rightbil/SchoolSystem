
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SchoolSystem.MVC.Models
{
    
    public class Department
    {
        public int  DepartmentId { get; set; }
        [DisplayName("Department Name"), Required, StringLength(10)]
        public string DeptName { get; set; }
        
        public int Capacity { get; set; }
    }
}