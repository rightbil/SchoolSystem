using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class Teacher
    {
        /*[HiddenInput(DisplayValue=false)]*/
        public int ID { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; private set; } = DateTime.Now;
        [Required(ErrorMessage = "Major is required")]
        public String Major { get; set; }
        public Course assignedCourses { get; set; }
        
        /*public virtual ISet<Course> xcourses { get; set; }
        public virtual IList<Course> ycourses { get; set; }*/
        
        // public Teacher()
        // {
        //     RegistrationDate = DateTime.Now;
        // }
        // public Teacher(string firstName, string lastName, String major, Course assignedCourses)
        // {
        //     this.FirstName = firstName;
        //     this.LastName = lastName;
        //     this.Major = major;
        //     this.assignedCourses = assignedCourses;
        //     this.RegistrationDate = DateTime.Now;
        //     
        // }

    }
}
