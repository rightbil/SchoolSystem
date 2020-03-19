
using SchoolSystem.DbModels.Model;
using System.Data.Entity;

namespace SchoolSystem.DbContext

{
    public class SchoolDbContext : System.Data.Entity.DbContext
    {
        public DbSet<Student> students { get; set; }
        public DbSet<CourseEnrollement> enrollments { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Teacher> teachers { get; set; }

        public System.Data.Entity.DbSet<SchoolSystem.DbModels.Model.Department> Departments { get; set; }
    }
}
