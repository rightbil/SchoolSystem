using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSystem.DbModels.Model
{

    [Table("tblDepartment")]
    public class Department
    {
        [Key, Column("Department ID")]
        public int DepartmentId { get; set; }

        [Column("Department Name"),Required,StringLength(20)]
        public string DepartmentName { get; set; }

        public int Capacity { get; set; }

        public virtual ICollection<Instructor> Teachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        /*public virtual ICollection<Course> Courses { get; set; }*/
       }
}

