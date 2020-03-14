using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> students { get; set; }
        public DbSet<Enrollment> enrollments { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Teacher> teachers { get; set; }
    }
}
