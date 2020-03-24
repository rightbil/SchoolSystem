namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrs1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblTeacher", "Course_CourseId", "dbo.tblCourse");
            DropIndex("dbo.tblTeacher", new[] { "Course_CourseId" });
            RenameColumn(table: "dbo.tblTeacher", name: "Course_CourseId", newName: "CourseId");
            AlterColumn("dbo.tblTeacher", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblTeacher", "CourseId");
            AddForeignKey("dbo.tblTeacher", "CourseId", "dbo.tblCourse", "CourseId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblTeacher", "CourseId", "dbo.tblCourse");
            DropIndex("dbo.tblTeacher", new[] { "CourseId" });
            AlterColumn("dbo.tblTeacher", "CourseId", c => c.Int());
            RenameColumn(table: "dbo.tblTeacher", name: "CourseId", newName: "Course_CourseId");
            CreateIndex("dbo.tblTeacher", "Course_CourseId");
            AddForeignKey("dbo.tblTeacher", "Course_CourseId", "dbo.tblCourse", "CourseId");
        }
    }
}
