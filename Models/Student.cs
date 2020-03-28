using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSystem.DbModels.Model
{
    [Table("tblStudent")]
    public class Student
    {
        [Column(Order = 0),Key]
        public int StudentId { get; set; }
        [Column( "Last Name",Order = 1)]
        public string LastName { get; set; }
        [Column("First Name", Order = 2)]
        public string FirstName { get; set; }
        [Column("Email Address", Order = 3)]
        public string EmailAddress { get; set; }
        [Column(Order = 4)]
        public string Password { get; set; }
        [Column("Phone Number", Order = 5)]
        public string PhoneNumber { get; set; }
        [Column("DoB",TypeName = "DateTime2")]
        public DateTime DateOfBirth { get; set; }
        [Column("Postal Code", Order =6)]
        public string Postalcode { get; set; }
        //public byte[] Photo { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }

        [Column("Registered On")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        
        public DateTime RegisteredOn
        {
            get { return DateTime.Now; }
            set { value = DateTime.Now; }
            
        }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

         public virtual ICollection<Enrollment> Enrollments { get; set; }
        // public int DepartmentId { get; set; }
        // public Department Department { get; set; }

    }
}