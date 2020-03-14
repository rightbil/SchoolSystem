using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> students { get; set; }
        public DbSet<Enrollment> enrollments { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Teacher> teachers { get; set; }
    }
}
