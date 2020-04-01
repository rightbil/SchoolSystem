namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mirgrn1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblStudent", "Full Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblStudent", "Full Name", c => c.String(nullable: false, maxLength: 40));
        }
    }
}
