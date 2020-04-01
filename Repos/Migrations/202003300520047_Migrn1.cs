namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tblTeacher", newName: "tblInstructor");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tblInstructor", newName: "tblTeacher");
        }
    }
}
