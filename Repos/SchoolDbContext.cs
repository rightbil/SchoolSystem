
using SchoolSystem.DbModels.Model;
using System.Data.Entity;
using  System.Data.Entity.ModelConfiguration;


namespace SchoolSystem.DbContext
    {
    public class SchoolDbContext : System.Data.Entity.DbContext
    {

        /*
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }
        */


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelbuilder.Entity<Student>().HasMany(p => p.Department).WithMany()

            /*modelBuilder.Entity<Course>()
                .HasMany(c => ).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("StudentId")
                    .ToTable("Enrollment"));*/
        }
        /*modelbuilder.Entity(typeof(ChangeOrder))
                .HasOne(typeof(User), "AssignedTo")
                .WithMany()
                .HasForeignKey("AssignedToID")
                .OnDelete(DeleteBehavior.Restrict); // no ON DELETE
            modelbuilder.Entity(typeof(ChangeOrder))

                .HasOne(typeof(User), "CreatedBy")
                .WithMany()
                .HasForeignKey("CreatedByID")
            
                .OnDelete(DeleteBehavior.Cascade); // set ON DELETE CASCADE
            #1#
        }
        */


        /*
        modelBuilder.Entity("myNamespace.Models.ChangeOrder", b =>
        {
            b.HasOne("myNamespace.Models.User")
                .WithMany()
                .HasForeignKey("CreatedByID")
                .OnDelete(DeleteBehavior.Cascade);
        });
        */

        public  SchoolDbContext():base("SchoolSystemConn") {}
        public DbSet<Student> students { get; set; }
        public DbSet<Enrollment> enrollments { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Department> departments { get; set; }
    }
}
