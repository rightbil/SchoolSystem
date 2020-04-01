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
                        Title = c.String(nullable: false, maxLength: 20),
                        Credit = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.tblEnrollment",
                c => new
                    {
                        EnrollmentId = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.EnrollmentId)
                .ForeignKey("dbo.tblCourse", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.tblStudent", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.tblStudent",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        LastName = c.String(name: "Last Name", nullable: false, maxLength: 20),
                        FirstName = c.String(name: "First Name", nullable: false, maxLength: 20),
                        EmailAddress = c.String(name: "Email Address", nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        PhoneNumber = c.String(name: "Phone Number", nullable: false, maxLength: 10),
                        PostalCode = c.String(name: "Postal Code", nullable: false, maxLength: 5),
                        DoB = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Comment = c.String(nullable: false, maxLength: 1024),
                        Url = c.String(),
                        RegisteredOn = c.DateTime(name: "Registered On", nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.tblDepartment", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.tblDepartment",
                c => new
                    {
                        DepartmentID = c.Int(name: "Department ID", nullable: false, identity: true),
                        DepartmentName = c.String(name: "Department Name", nullable: false, maxLength: 20),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.tblTeacher",
                c => new
                    {
                        InstructorID = c.Int(name: "Instructor ID", nullable: false, identity: true),
                        LastName = c.String(name: "Last Name", nullable: false, maxLength: 20),
                        FirstName = c.String(name: "First Name", nullable: false, maxLength: 20),
                        HireDate = c.DateTime(name: "Hire Date", nullable: false),
                        Major = c.String(nullable: false, maxLength: 20),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InstructorID)
                .ForeignKey("dbo.tblCourse", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.tblDepartment", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblEnrollment", "StudentID", "dbo.tblStudent");
            DropForeignKey("dbo.tblTeacher", "DepartmentId", "dbo.tblDepartment");
            DropForeignKey("dbo.tblTeacher", "CourseId", "dbo.tblCourse");
            DropForeignKey("dbo.tblStudent", "DepartmentId", "dbo.tblDepartment");
            DropForeignKey("dbo.tblEnrollment", "CourseID", "dbo.tblCourse");
            DropIndex("dbo.tblTeacher", new[] { "CourseId" });
            DropIndex("dbo.tblTeacher", new[] { "DepartmentId" });
            DropIndex("dbo.tblStudent", new[] { "DepartmentId" });
            DropIndex("dbo.tblEnrollment", new[] { "StudentID" });
            DropIndex("dbo.tblEnrollment", new[] { "CourseID" });
            DropTable("dbo.tblTeacher");
            DropTable("dbo.tblDepartment");
            DropTable("dbo.tblStudent");
            DropTable("dbo.tblEnrollment");
            DropTable("dbo.tblCourse");
        }
    }
}
