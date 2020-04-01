namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mirgrn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStudent", "Full Name", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblStudent", "Full Name");
        }
    }
}
