using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{

    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public String Password { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Postalcode { get; set; }
        public String photo { get; set; }
        public String Comment { get; set; }
        public String ImageUrl { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}