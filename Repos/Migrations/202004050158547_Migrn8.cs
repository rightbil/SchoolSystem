namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn8 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tblStudent", name: "DoB", newName: "Date of Birth");
            AlterColumn("dbo.tblStudent", "Date of Birth", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblStudent", "Date of Birth", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            RenameColumn(table: "dbo.tblStudent", name: "Date of Birth", newName: "DoB");
        }
    }
}
