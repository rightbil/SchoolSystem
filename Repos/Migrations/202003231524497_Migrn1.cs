namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tblDepartment", name: "DepartmentId", newName: "Department ID");
            RenameColumn(table: "dbo.tblDepartment", name: "DepartmentName", newName: "Department Name");
            AlterColumn("dbo.tblDepartment", "Department Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.tblTeacher", "Major", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblTeacher", "Major", c => c.String(nullable: false));
            AlterColumn("dbo.tblDepartment", "Department Name", c => c.String(nullable: false, maxLength: 10));
            RenameColumn(table: "dbo.tblDepartment", name: "Department Name", newName: "DepartmentName");
            RenameColumn(table: "dbo.tblDepartment", name: "Department ID", newName: "DepartmentId");
        }
    }
}
