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
        [StringLength(20),Required]
        public string Title { get; set; }
        public int Credit { get; set; }
        public double Price { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        
    }
}
