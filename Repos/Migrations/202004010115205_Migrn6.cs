namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStudent", "RegisteredOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblStudent", "RegisteredOn");
        }
    }
}
