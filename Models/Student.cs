using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolSystem.DbModels.Model
{
    [Table("tblStudent")]
    public partial class Student
    {
        [Column(Order = 0), Key] public int StudentId { get; set; }
        [Column("Last Name", Order = 1), StringLength(20), Required]
        public string LastName { get; set; }
        [Column("First Name", Order = 2), StringLength(20), Required]
        public string FirstName { get; set; }
        [Column("Email Address", Order = 3), StringLength(20), Required]
        public string EmailAddress { get; set; }
        [Column(Order = 4), StringLength(20), Required]
        public string Password { get; set; }
        [Column("Phone Number", Order = 5), StringLength(10), Required]
        public string PhoneNumber { get; set; }
        [Column("Date of Birth", TypeName = "Date", Order = 6), Required]
        public DateTime DateOfBirth { get; set; }
        [Column("Postal Code", Order = 7), StringLength(5), Required]
        public string Postalcode { get; set; }

      // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "Date"), Required]
        public DateTime? RegisteredOn { get; set; } = DateTime.Today;
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

       
    }


    //[MetadataType(typeof(Student))]
    public partial class Student
    {
        //public byte[] Photo { get; set; 
        [StringLength(1024), Required]
        public string Comment { get; set; }
        public string Url { get; set; }
    }


}