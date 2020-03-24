using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSystem.DbModels.Model
{
    [Table("tblTeacher")]
    public class Teacher
    {
       [Key, Column("Teacher ID")]
       public int TeacherId { get; set; }
       [Column("Last Name")]
       public string LastName { get; set; }
       [Column("First Name")]
       public string FirstName { get; set; }

       [Column("Hire Date"), Required]
       public DateTime HireDate { get ; set ;}
       public string Major { get; set; }
        
       //Foreign Key
       public int  DepartmentId { get; set; }
       public Department Department { get; set; }


       public int  CourseId { get; set; }
       public Course Course { get; set; }
        // Mule public int Department_DepartmentId { get; set; }

        // Mule [ForeignKey("Department_DepartmentId")]
        // Bililign repalce the above with

        // default Foreign key DepartmentId must be the same 

        /*public int CourseId { get; set; }
        public Course Course { get; set; }*/


        /*
        // Foreign key keword will overwrite the default in 3 ways
        // case 1.
        [ForeignKey("Department")]
        public int DepartmentRefId { get; set; } // will be foreingkey
        public Department Department { get; set; }

         //case 2. prevent the DepartmentId foreign key creation
         public int DepartmentRefId { get; set; } // will be foreingkey

         [ForeignKey("DepartmentRefId")]
         public Department Department { get; set; }

         // Case 3.


         public int DepartmentRefId { get; set; } // will be foreingkey

         [ForeignKey("DepartmentRefId")] // add this one on th navigation key in Department table
         public Department Department { get; set; }
         */










    }
}
