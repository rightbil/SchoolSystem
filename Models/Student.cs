using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{

    public class Student
    {
        public DateTime registrationDate;
        public int StudentId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Postalcode { get; set; }
        public string Photo { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
        public DateTime RegisteredOn
        {
            get { return DateTime.Now; }
            private set { value = DateTime.Now; }
            
        }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}