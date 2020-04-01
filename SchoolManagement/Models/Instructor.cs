using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SchoolSystem.MVC.Models
{
   public class Instructor
    {
        [Key]
        public int TeacherId { get; set; }

        [DisplayName("Last Name"), Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DisplayName("First Name"), Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Major is required")]
        public string Major { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss tt}")]
        [DisplayFormat(NullDisplayText = "Hire date not specified",DataFormatString = "{0:d}")]
        public DateTime ? HireDate { get; set; }

        [ScaffoldColumn(false)]
        public int DepartmentId { get; set; }

        public string Department { get; set; }

        /*public byte ? Age { get; set; }*/

        //public char Gender { get; set; }

        public int CourseId { get; set; }

        public string Course { get; set; }









    }
}