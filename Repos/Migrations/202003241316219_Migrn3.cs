namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStudent", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblStudent", "DepartmentId");
            AddForeignKey("dbo.tblStudent", "DepartmentId", "dbo.tblDepartment", "Department ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblStudent", "DepartmentId", "dbo.tblDepartment");
            DropIndex("dbo.tblStudent", new[] { "DepartmentId" });
            DropColumn("dbo.tblStudent", "DepartmentId");
        }
    }
}
