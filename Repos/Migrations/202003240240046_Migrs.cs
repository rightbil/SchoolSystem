namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblTeacher", "Course_CourseId", c => c.Int());
            CreateIndex("dbo.tblTeacher", "Course_CourseId");
            AddForeignKey("dbo.tblTeacher", "Course_CourseId", "dbo.tblCourse", "CourseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblTeacher", "Course_CourseId", "dbo.tblCourse");
            DropIndex("dbo.tblTeacher", new[] { "Course_CourseId" });
            DropColumn("dbo.tblTeacher", "Course_CourseId");
        }
    }
}
