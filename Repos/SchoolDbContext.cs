﻿using SchoolSystem.DbModels.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SchoolSystem.DbContext
{
    public class SchoolDbContext : System.Data.Entity.DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            /*modelBuilder.Entity<Course>()>.OneToManyCascadeDeleteConvention(true)
                .HasOne(typeof(User), "AssignedTo")
                .WithMany()
                .HasForeignKey("AssignedToID")
                .OnDelete(DeleteBehavior.Restrict); // no ON DELETE
            /*
         base.OnModelCreating(modelBuilder);
         foreach (var relationship in modelBuilder.Entity<T>())
          {
             relationship.DeleteBehavior = DeleteBehavior.Restrict;
         }
                   modelBuilder.Entity<Student>().HasMany(p => p.Department)
                          .WithMany()
                           modelBuilder
                      .Entity<Student>()
                      .HasMany(c =>)
                      .WithMany(i => i.Courses)
                      .Map(t => t.MapLeftKey("CourseID")
                      .MapRightKey("StudentId")
                      .ToTable("Enrollment"));#1#





      modelBuilder.Entity(typeof(ChangeOrder))
              .HasOne(typeof(User), "AssignedTo")
              .WithMany()
              .HasForeignKey("AssignedToID")
              .OnDelete(DeleteBehavior.Restrict); // no ON DELETE

      modelBuilder.Entity(typeof(ChangeOrder))
              .HasOne(typeof(User), "CreatedBy")
              .WithMany()
              .HasForeignKey("CreatedByID")
              .OnDelete(DeleteBehavior.Cascade); // set ON DELETE CASCADE

      modelBuilder.Entity("myNamespace.Models.ChangeOrder", b =>
      {
            b.HasOne("myNamespace.Models.User")
              .WithMany()
              .HasForeignKey("CreatedByID")
              .OnDelete(DeleteBehavior.Cascade);
      });
      */
        }
        public SchoolDbContext() : base("SchoolSystemConn") { }
        public DbSet<Student> students { get; set; }
        public DbSet<Enrollment> enrollments { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Department> departments { get; set; }
    }
}
