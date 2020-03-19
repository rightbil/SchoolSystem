using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Remoting;

namespace SchoolSystem.DbModels.Model
{
    [Table("tblStudent")]
    public class Student
    {
       // public DateTime registeredOn;
        
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
      //  public byte[] Photo { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
        public DateTime RegisteredOn
        {
            get { return DateTime.Now; }
            private set { value = DateTime.Now; }
            
        }
        public virtual ICollection<Course> Courses { get; set; }
    }
}