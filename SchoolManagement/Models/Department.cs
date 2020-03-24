
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SchoolSystem.MVC.Models
{
    
    public class Department
    {
        public int  DepartmentId { get; set; }
        [DisplayName("Department Name"), Required, StringLength(20)]
        public string DepartmentName { get; set; }
        public int Capacity { get; set; }
    }
}