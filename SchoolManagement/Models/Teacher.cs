using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SchoolSystem.MVC.Models
{
    public class Teacher
    {

        [Key]
        public int TeacherId { get; set; }

        [DisplayName("Last Name"), Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DisplayName("First Name"),Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Major is required")]
        public string Major { get; set; }
        
        public DateTime HireDate { get; set; }
    
        public int DepartmentId { get; set; }

        public string Department { get; set; }

        /*public byte ? Age { get; set; }*/

        public int CourseId { get; set; }

        public string Course { get; set; }









    }
}