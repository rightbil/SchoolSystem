using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.MVC.Models
{
    public class Teacher
    {

        [Display(Name = "Teacher ID"), Key]
        public int TeacherId { get; set; }

        [DisplayName("Last Name"), Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DisplayName("First Name"),Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        /*[Display(Name="Hire Date")]
        public DateTime HireDate { get; private set; } = DateTime.Now;*/

        [Required(ErrorMessage = "Major is required")]
        public string Majors { get; set; }

       

    }
}