using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolSystem.DbModels.Model
{
    [Table("tblCourse")]
    public class Course
    {   
        [Column(Order = 0), Key]
        public int CourseId { get; set; }
        [Column(Order = 1)]
        public string Title { get; set; }
        [Column(Order = 2)]
        public int Credit { get; set; }
        [Column(Order = 3)]
        public double Price { get; set; }
        /*public int DepartmentId { get; set; }
        public Department Department { get; set; }*/
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
       
    }
}
