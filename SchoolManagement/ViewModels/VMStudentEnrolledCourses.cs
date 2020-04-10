using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.ViewModels
{
    public class VMStudentEnrolledCourses
    {
        public int StudentId { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        public string Courses { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:d}")] //, ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth"), Required(ErrorMessage = "Date of Birth is required"),
         DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        /*[DisplayName("Total Credit")]
        public double totalCredit { get; set; }*/
    }
}