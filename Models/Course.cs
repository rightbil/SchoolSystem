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
        public int Credit { get; set; }
        public double Price { get; set; }
        public virtual ICollection<CourseEnrollement> Enrollments { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }

    }
}
