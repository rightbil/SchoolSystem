namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tblStudent", name: "Registered On", newName: "RegisteredOn");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.tblStudent", name: "RegisteredOn", newName: "Registered On");
        }
    }
}
