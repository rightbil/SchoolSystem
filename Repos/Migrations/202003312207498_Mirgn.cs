namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mirgn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblStudent", "RegisteredOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblStudent", "RegisteredOn", c => c.DateTime(nullable: false));
        }
    }
}
