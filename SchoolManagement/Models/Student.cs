using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SchoolSystem.MVC.Models
{
    public class Student : IComparable<Student>
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Student ID"), Key]
        public int StudentId { get; set; }
  //      [RegularExpression(@"^(([A-Za-z]+[\s] {1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage= "Last name should not contain either special character or numbers")

        [Display(Name = "Last Name"), Required(ErrorMessage = "Last name is Required"),
        StringLength(20, MinimumLength = 1, ErrorMessage = "You need to give a long enough first name")]

        public string LastName { get; set; }

        [Display(Name = "First Name"), Required(ErrorMessage = "First name is required"),
         StringLength(20, MinimumLength = 1, ErrorMessage = "You need to give a long enough last name")]
        public string FirstName { get; set; }

        [DisplayName("Full Name")] 
        public string FullName => LastName + " " + FirstName;


        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$")]
        //[ReadOnly(true)]
        [DataType(DataType.EmailAddress), Display(Name = "Email Address"),
         Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [DataType(DataType.EmailAddress), Display(Name = "Confirm Email"),
         System.ComponentModel.DataAnnotations.Compare("EmailAddress", ErrorMessage = "Confirm email do not match")]
        public string ConfirmEmailAddress { get; set; }

        [Display(Name = "Password"), Required(ErrorMessage = "Password is required"),
         StringLength(10, MinimumLength = 4, ErrorMessage = "Password length must be b/n 4 and 10"),
         DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Confirm Password"), Required(ErrorMessage = "Password is required"),
         DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password",
            ErrorMessage = "Confirm password do not match with password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Telephone Number"), Required(ErrorMessage = "Telephone Number is required"),
         DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth"), Required(ErrorMessage = "Date of Birth is required"),
         DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Zip Code"), Required(ErrorMessage = "Zipcode is required"), DataType(DataType.PostalCode)]
        public string Postalcode { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        //This is to open sites in a new window.
        [UIHint("WindowForUrl")]
        [Required, DataType(DataType.Url)]
        public string Url { get; set; }
        [HiddenInput(DisplayValue = false)] public DateTime RegisteredOn { get; set; }
        public int Age()
            => DateTime.Now.DayOfYear < RegisteredOn.DayOfYear
                ? DateTime.Now.Year - RegisteredOn.Year
                : DateTime.Now.Year - RegisteredOn.Year - 1;
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int CompareTo(Student other)
        {
           return this.FullName.CompareTo(other.FullName);
        }
    }
    public enum Gender
    {
        Male,
        Female,
        Undefined
    }
}
