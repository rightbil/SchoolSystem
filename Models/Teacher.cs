using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSystem.DbModels.Model
{
    [Table("tblTeacher")]
    public class Teacher
    {
        /*private DateTime hireDate=DateTime.Now;*/
        /*[HiddenInput(DisplayValue=false)]*/
        [Key, Column("Teacher ID")]
        public int TeacherId { get; set; }
        [Column("Last Name")]
        public string LastName { get; set; }
        [Column("First Name")]
        public string FirstName { get; set; }

        [Column("Hire Date")] 
        public DateTime HireDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Major is required")]
        public string Major { get; set; }
        public ICollection<Course> AssignedCourses { get; set; }

        //public ICollection<Department> Departments { get; set; }
        /*public virtual ISet<Course> xcourses { get; set; }
        public virtual IList<Course> ycourses { get; set; }*/
        
       

    }
}
