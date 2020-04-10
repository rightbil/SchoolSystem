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

        [DisplayName("Last Name"), Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DisplayName("First Name"), Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss tt}")]
        [DisplayFormat(NullDisplayText = "Hire date not specified", DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime),"01/01/2020","31/12/2020")]

        [SsDateRange("01/01/2010")]
        [SsCurrentDate] // only to check the date is at most today
        public DateTime? HireDate { get; set; }

        public Gender Gender { get; set; }

        [ScaffoldColumn(false)] public int DepartmentId { get; set; }

        public string Department { get; set; }

        /*public byte ? Age { get; set; }*/


        public int CourseId { get; set; }

        public string Course { get; set; }
    }

    /*
    public enum Gender
        {
            Male = 0,
            Female = 1,
            Unknown = -1
        };
        */

}



