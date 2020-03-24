namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn : DbMigration
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
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.tblDepartment",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 10),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.tblTeacher",
                c => new
                    {
                        TeacherID = c.Int(name: "Teacher ID", nullable: false, identity: true),
                        LastName = c.String(name: "Last Name"),
                        FirstName = c.String(name: "First Name"),
                        HireDate = c.DateTime(name: "Hire Date", nullable: false),
                        Major = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherID)
                .ForeignKey("dbo.tblDepartment", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblCourseEnrollement", "StudentID", "dbo.tblStudent");
            DropForeignKey("dbo.tblCourseEnrollement", "CourseID", "dbo.tblCourse");
            DropForeignKey("dbo.tblTeacher", "DepartmentId", "dbo.tblDepartment");
            DropIndex("dbo.tblCourseEnrollement", new[] { "StudentID" });
            DropIndex("dbo.tblCourseEnrollement", new[] { "CourseID" });
            DropIndex("dbo.tblTeacher", new[] { "DepartmentId" });
            DropTable("dbo.tblStudent");
            DropTable("dbo.tblCourseEnrollement");
            DropTable("dbo.tblTeacher");
            DropTable("dbo.tblDepartment");
            DropTable("dbo.tblCourse");
        }
    }
}
