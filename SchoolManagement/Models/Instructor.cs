using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SchoolManagement.Customize;
using SchoolSystem.Customize;

namespace SchoolSystem.MVC.Models
{
    public class Instructor
    {
        [Key] public int InstructorId { get; set; }

       // [DisplayName("Last Name"), Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DisplayName("First Name"), Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(NullDisplayText = "Hire date not specified", DataFormatString = "{0:d}",ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime),"01/01/2020","31/12/2020")]

        //[ValidDateRange("01/01/2020")]
        //[SsCurrentDate] // only to check the date is at most today
        [DisplayName("Hire Date"),Required(ErrorMessage = "Hire date should not be in the future")]
        public DateTime HireDate { get; set; }

        public string Gender { get; set; }
        
        //[ScaffoldColumn(false)]
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int CourseId { get; set; }
        public string Course { get; set; }
    }
}



