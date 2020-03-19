namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mrgn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCourse",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Credit = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Student_StudentId = c.Int(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.tblStudent", t => t.Student_StudentId)
                .Index(t => t.Student_StudentId);
            
            CreateTable(
                "dbo.tblCourseEnrollement",
                c => new
                    {
                        CourseEnrollementId = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.CourseEnrollementId)
                .ForeignKey("dbo.tblCourse", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.tblStudent", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.tblStudent",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        LastName = c.String(name: "Last Name"),
                        FirstName = c.String(name: "First Name"),
                        EmailAddress = c.String(name: "Email Address"),
                        Password = c.String(),
                        PhoneNumber = c.String(name: "Phone Number"),
                        PostalCode = c.String(name: "Postal Code"),
                        DoB = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Comment = c.String(),
                        ImageUrl = c.String(),
                        RegisteredOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.tblTeacher",
                c => new
                    {
                        TeacherID = c.Int(name: "Teacher ID", nullable: false, identity: true),
                        LastName = c.String(name: "Last Name"),
                        FirstName = c.String(name: "First Name"),
                        HireDate = c.DateTime(name: "Hire Date", nullable: false),
                        Major = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherID);
            
            CreateTable(
                "dbo.tblDepartment",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DeptName = c.String(nullable: false, maxLength: 10),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        Teacher_TeacherId = c.Int(nullable: false),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_TeacherId, t.Course_CourseId })
                .ForeignKey("dbo.tblTeacher", t => t.Teacher_TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.tblCourse", t => t.Course_CourseId, cascadeDelete: true)
                .Index(t => t.Teacher_TeacherId)
                .Index(t => t.Course_CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherCourses", "Course_CourseId", "dbo.tblCourse");
            DropForeignKey("dbo.TeacherCourses", "Teacher_TeacherId", "dbo.tblTeacher");
            DropForeignKey("dbo.tblCourseEnrollement", "StudentID", "dbo.tblStudent");
            DropForeignKey("dbo.tblCourse", "Student_StudentId", "dbo.tblStudent");
            DropForeignKey("dbo.tblCourseEnrollement", "CourseID", "dbo.tblCourse");
            DropIndex("dbo.TeacherCourses", new[] { "Course_CourseId" });
            DropIndex("dbo.TeacherCourses", new[] { "Teacher_TeacherId" });
            DropIndex("dbo.tblCourseEnrollement", new[] { "StudentID" });
            DropIndex("dbo.tblCourseEnrollement", new[] { "CourseID" });
            DropIndex("dbo.tblCourse", new[] { "Student_StudentId" });
            DropTable("dbo.TeacherCourses");
            DropTable("dbo.tblDepartment");
            DropTable("dbo.tblTeacher");
            DropTable("dbo.tblStudent");
            DropTable("dbo.tblCourseEnrollement");
            DropTable("dbo.tblCourse");
        }
    }
}
