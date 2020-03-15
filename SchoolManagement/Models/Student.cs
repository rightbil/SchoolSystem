using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using SchoolSystem.Utility;

namespace SchoolSystem.Models
{
    [CustomAttributeAll("Bililign Work", "testing Scenario")]
    [CustomAttributeAuthor("Bililign Gebru", 1.0)]

    public class Student
    {
        [Display(Name = "Student ID")] public int ID { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is Required")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(25, MinimumLength = 10, ErrorMessage = "You need to give a long enough first name")]
        public string FirstName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Email")]
        [Compare("EmailAddress", ErrorMessage = "Confirm email do not match")]
        public string ConfirmEmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password length must be b/n 8 and 20")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password do not match with password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Telephone Number")]
        [Required(ErrorMessage = "Telephone Number is required")]
        [DataType(DataType.PhoneNumber)]

        public string PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]

        public string DateOfBirth { get; set; }



        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zipcode is required")]
        [DataType(DataType.PostalCode)]

        public string Postalcode { get; set; }


        [Display(Name = "Photo")]
        [Required(ErrorMessage = "Photo need to be uploaded")]
     
        [DataType(DataType.Upload)]
        public String photo { get; set; }



        [Required(ErrorMessage = "Comment")]
        [DataType(DataType.MultilineText)]
        public String Comment { get; set; }

        [Required(ErrorMessage = "Image Url")]
        [DataType(DataType.ImageUrl)]
        public String ImageUrl { get; set; }


        //[DataType(DataType.Duration)]
        //[DataType(DataType.Custom)]
        //[DataType(DataType.Currency)]

        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}
